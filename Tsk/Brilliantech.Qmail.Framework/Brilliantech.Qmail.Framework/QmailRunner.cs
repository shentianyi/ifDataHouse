using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Framwork.Utils.LogUtil;
using Quartz;
using Quartz.Impl;
using Brilliantech.Qmail.Framework.Job;

namespace Brilliantech.Qmail.Framework
{
    class QmailRunner
    {
        public static IScheduler Scheduler;
        static void Main(string[] args)
        {
            ISchedulerFactory sf = new StdSchedulerFactory();
            Scheduler = sf.GetScheduler();
            new TskInspectDetailJobTrigger();
            Scheduler.Start();
            Console.Read();
        }
    }
}
