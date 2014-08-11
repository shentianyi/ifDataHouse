using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brilliantech.Framwork.Utils.LogUtil;
using System.IO;
using Brilliantech.Tsk.Client.WPFUI.Config;
using System.Timers;
using log4net;
using Brilliantech.Tsk.Client.WPFUI.TskInspectService;
using Brilliantech.Framwork.Message;


namespace Brilliantech.Tsk.Client.WPFUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer scanTimer;

        private static Object fileLocker = new Object();
        private static Object dirLocker = new Object();
         

        public MainWindow()
        {
            InitializeComponent();
            InitTimer();
            StartService();
            CleanupLog();
        }

        /// <summary>
        /// init timer
        /// </summary>
        private void InitTimer()
        { 
            this.scanTimer = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.scanTimer)).BeginInit();
            this.scanTimer.Enabled = true;
            this.scanTimer.Interval = TskBaseConfig.ScanInterval;
            this.scanTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.scanTimer_Elapsed);
            ((System.ComponentModel.ISupportInitialize)(this.scanTimer)).EndInit();
        }

        /// <summary>
        /// start service
        /// </summary>
        private void StartService()
        { 
            try
            { 
                if (!Directory.Exists(TskBaseConfig.DataFilePath))
                {
                    LogUtil.Logger.Error("【数据文件目录不存在】" + TskBaseConfig.DataFilePath);
                    MessageBox.Show("【数据文件目录不存在】" + TskBaseConfig.DataFilePath, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                List<string> files = GetAllFilesFromDirectory(TskBaseConfig.DataFilePath);
                if (files != null)
                {
                    foreach (string file in files)
                    {
                        LogUtil.Logger.Info("【处理残留文件】" + System.IO.Path.GetFileName(file));
                        Process(file);
                    }
                }
                scanTimer.Enabled = true;
                scanTimer.Start();
                LogUtil.Logger.Info("【TSK数据服务已启动】");
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                // MessageBox.Show(e.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Cleanup Log
        /// </summary>
        private void CleanupLog()
        {
            try
            {
                LogCleanupTask logCleanupTask = new LogCleanupTask();
                logCleanupTask.Clean(DateTime.Now);
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error("【清理日志错误】" + e.Message);
            }
        }
        
        /// <summary>
        /// Process File
        /// </summary>
        /// <param name="fullPath"></param>
        private void Process(string fullPath)
        {
            bool canAccessRemoteService = true;

            string nextDir = System.IO.Path.Combine(TskBaseConfig.ErrorFilePath, DateTime.Today.ToString("yyyy-MM-dd"));
            try
            {
                if (IsFileClosed(fullPath))
                {
                    using (FileStream fs = File.Open(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            string s = reader.ReadLine();
                            InspectServiceClient client = new InspectServiceClient("BasicHttpBinding_IInspectService",
                                TskBaseConfig.InspectServiceAddress);
                            ProcessMessage msg = client.CreateInspect(s);
                            if (msg.Result)
                            {
                                nextDir = System.IO.Path.Combine(TskBaseConfig.MovedFilePath, DateTime.Today.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                LogUtil.Logger.Error(msg.GetMessageContent());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.GetType());
                if (e is System.ServiceModel.EndpointNotFoundException) {
                    canAccessRemoteService = false;
                }
                LogUtil.Logger.Error(e.Message);
            }
            // 是否可以访问服务
            if (canAccessRemoteService)
            {
                // 是否删除文件
                if (TskBaseConfig.DeleteFileAfterRead)
                {
                    // 删除文件
                    if (IsFileClosed(fullPath))
                    {
                        File.Delete(fullPath);
                        LogUtil.Logger.Warn("【删除读完的数据文件】" + fullPath);
                    }
                }
                else
                {
                    // 移动文件
                    CheckDirectory(nextDir);
                    MoveFile(fullPath, System.IO.Path.Combine(nextDir, System.IO.Path.GetFileName(fullPath)));
                }
            }
        }

        /// <summary>
        /// Get all files in directory
        /// </summary>
        /// <param name="direcctory"></param>
        /// <returns></returns>
        private List<string> GetAllFilesFromDirectory(string directory)
        {
            try
            {
                return Directory.GetFiles(directory.Trim()).ToList();
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error("【获取所有文件出现异常】" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Move file
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="autoRename"></param>
        private static void MoveFile(string sourceFileName, string destFileName, bool autoRename = true)
        {
            try
            {
                lock (fileLocker)
                {
                    if (File.Exists(sourceFileName))
                    {
                        if (autoRename)
                        {
                            destFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(destFileName),
                               System.IO.Path.GetFileNameWithoutExtension(sourceFileName) + "_" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(sourceFileName));
                        }
                        else {
                            throw new IOException("目标文件已经存在");
                        }
                    }
                    File.Move(sourceFileName, destFileName);
                    LogUtil.Logger.Info("【文件移动】【自】" + sourceFileName + "【至】" + destFileName);
                }
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error("【文件移动】【自】" + sourceFileName + "【至】" + destFileName+"【错误】" + e.Message);
            }
        }

        /// <summary>
        /// Check directory
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="autoCreate"></param>
        /// <returns></returns>
        private static bool CheckDirectory(string dirName, bool autoCreate = true)
        {
            bool result = false;
            lock (dirLocker)
            {
                if (Directory.Exists(dirName))
                {
                    result = true;
                }
                else
                {
                    if (autoCreate)
                    {
                        Directory.CreateDirectory(dirName);
                        result = true;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Check file is closed 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsFileClosed(string fileName)
        {
            try
            {
                using (File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                LogUtil.Logger.Warn(fileName + "文件未关闭." + e.Message);
                return false;
            }
        }
        /// <summary>
        /// Scan File Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            scanTimer.Stop();
            List<string> files = GetAllFilesFromDirectory(TskBaseConfig.DataFilePath);
            foreach (string file in files)
            {
                if (IsFileClosed(file))
                {
                    Process(file);
                }
            }
            scanTimer.Enabled = true;
            scanTimer.Start();
        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {
            new Setting().ShowDialog();
        }

        private void CleanLogBtn_Click(object sender, RoutedEventArgs e)
        {
            CleanupLog();
            MessageBox.Show("日志清理成功");
        }
    }
}
