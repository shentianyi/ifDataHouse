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
            try
            {
                Console.Title = "QMail";
                Console.ForegroundColor = ConsoleColor.White;
                ISchedulerFactory sf = new StdSchedulerFactory();
                Scheduler = sf.GetScheduler();
                new TskInspectDetailJobTrigger();
                Scheduler.Start();

                for (var i = 0; i < 10; i++) {
                    Console.WriteLine();
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
              
                Console.WriteLine("       ---------------------------------------------------");
                Console.WriteLine("       ...................QMail已启动......................");
                Console.WriteLine("       ...................请不要关闭此窗口.................");
                Console.WriteLine("       ---------------------------------------------------");
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine();
                }
                Console.ReadLine();
            }
            catch (Exception e) {
                LogUtil.Logger.Error(e.Message);
            }
        }
    }
}
