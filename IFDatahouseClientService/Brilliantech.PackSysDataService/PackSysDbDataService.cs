using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.IO;
using Brilliantech.BaseClassLib.Util;

namespace Brilliantech.PackSysDataService
{
    partial class PackSysDbDataService : ServiceBase
    { 
        public PackSysDbDataService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            writeLog("Brillantech.PackSysDbData.Service 【启动中】");
            LogUtil.Logger.Info("START");
            readDataTimer.Interval = Conf.ReadDbInterval;
            readDataTimer.Enabled = true;
            readDataTimer.Start();
        }

        protected override void OnStop()
        {
            readDataTimer.Stop();
        } 

        private void readDataTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                PackDbDataHandler packDataHandler = new PackDbDataHandler();
                DateTime dataReadEndTime = DateTime.Now;
                string fileName = Guid.NewGuid().ToString() + ".txt";
                string file = Path.Combine(Conf.PackDataPath, fileName);
                packDataHandler.WritePackItemViewToFileBy(Conf.DataReadStartTime, dataReadEndTime, file);
                Conf.DataReadStartTime = dataReadEndTime;
            }
            catch (Exception ex) {
                LogUtil.Logger.Error(ex.Message);
            }
        }

        private void writeLog(string log)
        {
            packDataSysEventLog.WriteEntry(new DateTime().ToString("yyyy-MM-dd hh:MM:sss"));
            packDataSysEventLog.WriteEntry(log);
        }
    }
}
