using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using System.Configuration;
using Brilliantech.Framwork.Utils.LogUtil;

namespace Brilliantech.Tsk.Manage.WebApp.Job
{
    public class TskDataEmailCronTrigger:IJobTrigger
    {
        private IJobDetail job;
        private ICronTrigger trigger;
    
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public TskDataEmailCronTrigger()
        {
            this.job = JobBuilder.Create<TskDataEmailCronJob>()
                  .WithIdentity("job1", "group1")
                  .Build();
            this.trigger = (ICronTrigger)TriggerBuilder.Create()
                                                      .WithIdentity("trigger1", "group1")
                                                      .WithCronSchedule(ConfigurationManager.AppSettings["TskDataEmailCron"].ToString())
                                                      .Build();
        }

        public void Run()
        { 
            DateTimeOffset ft = MvcApplication.Scheduler.ScheduleJob(job, trigger);

            LogUtil.Logger.Info("测试数据文件邮件任务启动成功");

        }
    }
}