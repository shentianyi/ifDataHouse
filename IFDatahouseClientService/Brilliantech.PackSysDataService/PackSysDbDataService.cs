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
            readDataTimer.Enabled = false;
        }

        protected override void OnStart(string[] args)
        {
            writeLog("Brillantech.PackSysDbData.Service 【启动中】");
            LogUtil.Logger.Info("---[ START ]----");
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
            bool success = false;
            string dataReadEndTime = string.Empty;
            try
            {
                PackDbDataHandler packDataHandler = new PackDbDataHandler();
              //  DateTime dataReadEndTime = Conf.DataReadStartTime.AddMilliseconds(10000);
                dataReadEndTime = TimeUtil.GetDateTimeInMil();
                string fileName = Guid.NewGuid().ToString() + ".txt";
                string file = Path.Combine(Conf.PackDataPath, fileName);
                packDataHandler.WritePackItemViewToFileBy(Conf.DataReadStartTime, dataReadEndTime, file);
                success = true;
            }
            catch (Exception ex) {
                success = false;
                LogUtil.Logger.Error(ex.Message);
               // this.Stop();
            }
            if (success) {
                Conf.DataReadStartTime = dataReadEndTime;
            }
        }

        private void writeLog(string log)
        {
            packDataSysEventLog.WriteEntry(new DateTime().ToString("yyyy-MM-dd hh:MM:sss"));
            packDataSysEventLog.WriteEntry(log);
        }
    }
}
