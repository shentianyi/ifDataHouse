using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Brilliantech.BaseClassLib.Util;
using System.IO;
using Brilliantech.DatahouseService.ServiceProvider;

namespace Brilliantech.DatahouseService.PackSys
{
    public partial class DatahousePackSysService : ServiceBase
    {
        private static Object fileLocker = new Object();
        private static Object dirLocker = new Object();
        public DatahousePackSysService()
        {
            InitializeComponent();
            scanTimer.Enabled = false;
        }

        protected override void OnStart(string[] args)
        {
            writeLog("Brillantech.Datahouse.Pack.Service 【启动中】");
           // LogUtil.Logger.Info("Brillantech.Datahouse.Pack.Service 【启动中】");
            try
            {
                if (!Directory.Exists(Conf.PackFilePath))
                {
                   // LogUtil.Logger.Error("【文件目录不存在】" + Conf.PackFilePath);
                    this.Stop();
                }
                //List<string> files =FileUtil.GetAllFilesFromDirectory(Conf.PackFilePath);
                //if (files != null)
                //{
                //    foreach (string file in files)
                //    {
                //        //LogUtil.Logger.Info("【处理残留文件】" + Path.GetFileName(file));
                //        Process(file);
                //    }
                //}
                scanTimer.Interval = Conf.ScanInt;
                scanTimer.Enabled = true;
                scanTimer.Start();
                writeLog("Brillantech.Datahouse.Pack.Service 【已启动】");
                LogUtil.Logger.Info("Brillantech.Datahouse.Pack.Service 【已启动】");
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
            LogUtil.Logger.Warn("Brillantech.Datahouse.Pack.Service 【已停止】.");
        }
        private void Process(string fullPath)
        { 
            try
            {
              if (FileUtil.IsFileClosed(fullPath))
                using (FileStream fs = File.Open(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        while (reader.Peek()>=0)
                        {
                            string s = reader.ReadLine();
                            if (s == null || s.Length == 0 || s.Split(Conf.DataSpliter).Length != Conf.PackParamCount)
                            {
                                LogUtil.Logger.Error("【数据错误】【文件】" + Path.GetFileName(fullPath));
                                // LogUtil.Logger.Error(s.Split(Conf.DataSpliter));
                                //    LogUtil.Logger.Error(Conf.DataSpliter);
                                // LogUtil.Logger.Error(s.Split(Conf.DataSpliter).Length);
                            }
                            else
                            {
                                string[] data = s.Split(Conf.DataSpliter);
                                Dictionary<string, string> dataMap = new Dictionary<string, string>();
                                dataMap.Add("partId", data[0]);
                                dataMap.Add("productNr", data[1]);
                                dataMap.Add("entityId", data[2]);
                                dataMap.Add("packTime", data[3]);
                                new Servicer().AddProductPack(dataMap);
                            }
                        }
                    }
                }
                File.Delete(fullPath); 
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
               
            }
        }
        private void writeLog(string log)
        {
            datahousePackEventLog.WriteEntry(new DateTime().ToString("yyyy-MM-dd hh:MM:sss"));
            datahousePackEventLog.WriteEntry(log);
        }

        private void scanTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            scanTimer.Stop();
            List<string> files = FileUtil.GetAllFilesFromDirectory(Conf.PackFilePath);
            if (files != null)
            {
                foreach (string file in files)
                {
                    Process(file);
                }
            }
            scanTimer.Enabled = true;
            scanTimer.Start();
        }

    }
}
