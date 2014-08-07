using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Timers; 
using Brilliantech.DatahouseService.ServiceProvider;
using Brilliantech.BaseClassLib.Util;

namespace Brilliantech.DatahouseService.Testbench
{
    public partial class DatahouseTestbenchService : ServiceBase
    { 
        private static Object fileLocker = new Object();
        private static Object dirLocker = new Object();

        public DatahouseTestbenchService()
        {
            InitializeComponent(); 
        }

        protected override void OnStart(string[] args)
        {
            writeLog("Brillantech.Datahouse.Testbench.Service 【启动中】");
            LogUtil.Logger.Info("Brillantech.Datahouse.Testbench.Service 【启动中】");
            try
            {
                if (!Directory.Exists(Conf.TestFilePath))
                {
                    LogUtil.Logger.Error("【文件目录不存在】"+Conf.TestFilePath);
                    this.Stop();
                }
                List<string> files = GetAllFilesFromDirectory(Conf.TestFilePath);
                if (files != null) {
                    foreach (string file in files) {
                        LogUtil.Logger.Info("【处理残留文件】"+Path.GetFileName(file));
                        Process(file);
                    }
                }
                scanTimer.Interval = Conf.ScanInt;
                scanTimer.Enabled = true;
                scanTimer.Start();
                writeLog("Brillantech.Datahouse.Testbench.Service 【已启动】");
                LogUtil.Logger.Info("Brillantech.Datahouse.Testbench.Service 【已启动】");
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                writeLog(e.Message);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            LogUtil.Logger.Warn("Brillantech.Datahouse.Testbench.Service 【已停止】.");
        }
        private void Process(string fullPath) {

            string nextDir;
            try
            {
                using (FileStream fs = File.Open(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string s = reader.ReadLine();
                        if (s == null || s.Length == 0 || s.Split(Conf.DataSpliter).Length != Conf.TestParamCount)
                        {
                            LogUtil.Logger.Error("【数据错误】【文件】" + Path.GetFileName(fullPath));
                            LogUtil.Logger.Error(s.Split(Conf.DataSpliter));
                            LogUtil.Logger.Error(Conf.DataSpliter);
                            LogUtil.Logger.Error(s.Split(Conf.DataSpliter).Length);
                            nextDir = Path.Combine(Conf.TestErrorFilePath, DateTime.Today.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            string[] data = s.Split(Conf.DataSpliter);
                            Dictionary<string, string> dataMap = new Dictionary<string, string>();
                            dataMap.Add("entityId", data[0]);
                            DateTime date = DateTime.Now;
                            DateTime.TryParse(data[1], out date);
                            dataMap.Add("inspectTime", TimeUtil.GetMilliseconds(date).ToString());
                            dataMap.Add("partNr", data[2]);
                            dataMap.Add("productNr", data[3]);
                            dataMap.Add("type", data[4]);
                            new Servicer().AddProductInspect(dataMap);
                            nextDir = Path.Combine(Conf.TestMovedFilePath , DateTime.Today.ToString("yyyy-MM-dd"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                nextDir = Path.Combine(Conf.TestErrorFilePath, DateTime.Today.ToString("yyyy-MM-dd"));
            }

            CheckDirectory(nextDir);
            MoveFile(fullPath, Path.Combine(nextDir, Path.GetFileName(fullPath)));
        }
        private List<string> GetAllFilesFromDirectory(string directory)
        {
            try
            {
                return Directory.GetFiles(directory.Trim()).ToList();
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                return null;
            }
        }

        private static void MoveFile(string sourcePath, string targetPath, bool autoRename = true)
        {
            lock (fileLocker)
            {
                if (File.Exists(sourcePath))
                {
                    if (autoRename)
                    {
                        targetPath = Path.Combine(Path.GetDirectoryName(targetPath), Guid.NewGuid().ToString() + Path.GetExtension(targetPath));
                    }
                }
                File.Move(sourcePath, targetPath);
                LogUtil.Logger.Info("【文件移动】【自】" + sourcePath + "【至】" + targetPath);
            }
        }
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
                LogUtil.Logger.Warn(fileName + "not closed." + e.Message);
                return false;
            }
        }
        private void writeLog(string log)
        {
            datahouseTestEventLog.WriteEntry(new DateTime().ToString("yyyy-MM-dd hh:MM:sss"));
            datahouseTestEventLog.WriteEntry(log);
        }

        private void scanTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            scanTimer.Stop();
            List<string> files = GetAllFilesFromDirectory(Conf.TestFilePath);
            foreach (string file in files) {
                Process(file);
            }
            scanTimer.Enabled = true;
            scanTimer.Start();
        }

    }
}
