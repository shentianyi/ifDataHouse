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
            scanTimer.Enabled = false;
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
                scanTimer.Interval = Conf.ScanInt;
                scanTimer.Enabled = true;
                scanTimer.Start();
                writeLog("Brillantech.Datahouse.Testbench.Service 【已启动】");
                LogUtil.Logger.Info("Brillantech.Datahouse.Testbench.Service 【已启动】");
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            LogUtil.Logger.Warn("Brillantech.Datahouse.Testbench.Service 【已停止】.");
        }
        private void Process(string fullPath)
        {
            try
            {
                bool ok = false;
                using (FileStream fs = File.Open(fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string s = reader.ReadLine();
                        if (s == null || s.Length == 0 || s.Split(Conf.DataSpliter).Length != Conf.TestParamCount)
                        {
                            ok = true;
                            LogUtil.Logger.Error("【数据错误】【文件】" + Path.GetFileName(fullPath));
                            //File.Delete(fullPath);                      
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
                            ok = new Servicer().AddProductInspect(dataMap);
                        }
                    }
                }
                if(ok)
                File.Delete(fullPath);
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
            }
        }

        private void writeLog(string log)
        {
            //datahouseTestEventLog.WriteEntry(new DateTime().ToString("yyyy-MM-dd hh:MM:sss"));
            //datahouseTestEventLog.WriteEntry(log);
        }

        private void scanTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            scanTimer.Stop();
            List<string> files = FileUtil.GetAllFilesFromDirectory(Conf.TestFilePath);
            foreach (string file in files) {
                Process(file);
            }
            scanTimer.Enabled = true;
            scanTimer.Start();
        }

        private void datahouseTestEventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

    }
}
