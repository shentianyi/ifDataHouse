using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Quartz;
using Brilliantech.Framwork.Utils.LogUtil;
using Quartz.Impl;
using Brilliantech.Tsk.QuartzTask.WS.Job;

namespace Brilliantech.Tsk.QuartzTask.WS
{
    public partial class TskQuartzTaskService : ServiceBase
    {
        public static IScheduler Scheduler;

        public TskQuartzTaskService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ISchedulerFactory sf = new StdSchedulerFactory();
                Scheduler = sf.GetScheduler();

                new TskDataEmailCronTrigger();
                Scheduler.Start();
                EventLog.WriteEntry("服务启动成功");
                LogUtil.Logger.Info("服务启动成功");
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("服务启动失败:" + e.Message);
                LogUtil.Logger.Error("服务启动失败:" + e.Message);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            if (Scheduler != null) {
                Scheduler.Shutdown();
                LogUtil.Logger.Info("定时任务停止");
            } 
            EventLog.WriteEntry("服务已停止");
            LogUtil.Logger.Info("服务停止");
        }
    }
}
