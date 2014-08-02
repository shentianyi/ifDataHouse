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
    public class TskDataEmailCronTriggerItem
    {
        private string triggerId;
        private string triggerGroupId;
        private string cronSchedule;
        private int startDayOff;
        private int startHourOff;
        private int startMinOff;

        private int endDayOff;
        private int endHourOff;
        private int endMinOff;

        public static List<TskDataEmailCronTriggerItem> GetList(string groupId)
        {
            List<TskDataEmailCronTriggerItem> items = new List<TskDataEmailCronTriggerItem>();
            var appSettings = ConfigurationManager.AppSettings;
            string[] tskDataEmailQueryDates = appSettings["TskDataEmailQueryDates"].Split(';');
            string[] tskDataEmailCrons = appSettings["TskDataEmailCrons"].Split(';');
            for (var i = 0; i < tskDataEmailCrons.Length; i++)
            {
                TskDataEmailCronTriggerItem item = new TskDataEmailCronTriggerItem()
                {
                    triggerGroupId = groupId,
                    triggerId = "trigger" + i.ToString(),
                    cronSchedule = tskDataEmailCrons[i]
                };
                //-1:7:0~0:7:0
                string[] dateOffs = tskDataEmailQueryDates[i].Split('~');
                string[] startOffs = dateOffs[0].Split(':');
                item.startDayOff = int.Parse(startOffs[0]);
                item.startHourOff = int.Parse(startOffs[1]);
                item.startMinOff = int.Parse(startOffs[2]);
                string[] endOffs = dateOffs[1].Split(':');
                item.endDayOff = int.Parse(endOffs[0]);
                item.endHourOff = int.Parse(endOffs[1]);
                item.endMinOff = int.Parse(endOffs[2]);
                items.Add(item);
            }
            return items;
        }
        public string TriggerId
        {
            get { return triggerId; }
            set { triggerId = value; }
        }
        public string TriggerGroupId
        {
            get { return triggerGroupId; }
            set { triggerGroupId = value; }
        }

        public string CronSchedule
        {
            get { return cronSchedule; }
            set { cronSchedule = value; }
        }

        public int StartDayOff
        {
            get { return startDayOff; }
            set { startDayOff = value; }
        }

        public int StartHourOff
        {
            get { return startHourOff; }
            set { startHourOff = value; }
        }

        public int StartMinOff
        {
            get { return startMinOff; }
            set { startMinOff = value; }
        }
        public int EndDayOff
        {
            get { return endDayOff; }
            set { endDayOff = value; }
        }

        public DateTime StartDate
        {
            get
            {
                return DateTime.Today.AddDays(this.startDayOff).AddHours(this.startHourOff).AddMinutes(this.startMinOff);
            }
        }

        public int EndHourOff
        {
            get { return endHourOff; }
            set { endHourOff = value; }
        }

        public int EndMinOff
        {
            get { return endMinOff; }
            set { endMinOff = value; }
        }

        public DateTime EndDate
        {
            get
            {
                return DateTime.Today.AddDays(this.endDayOff).AddHours(this.endHourOff).AddMinutes(this.endMinOff);
            }
        }
    }
    public class TskDataEmailCronTrigger
    {
        private static string groupId = "TskDataEmailCronGroup";
        private static List<TskDataEmailCronTriggerItem> triggerItems = TskDataEmailCronTriggerItem.GetList(groupId);
      
       // private List<ICronTrigger> triggers;
 
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public TskDataEmailCronTrigger()
        {
            var i = 0;
            foreach (var item in triggerItems)
            {
                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                          .WithIdentity(item.TriggerId, item.TriggerGroupId)
                                                          .WithCronSchedule(item.CronSchedule)
                                                          .Build();

                IJobDetail job = JobBuilder.Create<TskDataEmailCronJob>()
                 .WithIdentity("job"+i.ToString(), groupId)
                 .Build();

                job.JobDataMap.Add("StartDate",item.StartDate);
                job.JobDataMap.Add("EndDate", item.EndDate);

               
                MvcApplication.Scheduler.ScheduleJob(job, trigger);
                i += 1;
                LogUtil.Logger.Info("InspectEmailQueryDate:" + item.StartDate.ToString()+"~"+ item.EndDate.ToString());
                LogUtil.Logger.Info("添加触发器:" + trigger.Key);
                //this.triggers.Add(trigger);
            }
            LogUtil.Logger.Info("测试数据文件邮件任务启动成功");
        }

        //public void Run()
        //{
        //    foreach (var trigger in triggers)
        //    {
        //        LogUtil.Logger.Info("添加触发器:" + trigger.Key);
        //        MvcApplication.Scheduler.ScheduleJob(job, trigger);
        //    }
        //    LogUtil.Logger.Info("测试数据文件邮件任务启动成功");
        //}
    }
}