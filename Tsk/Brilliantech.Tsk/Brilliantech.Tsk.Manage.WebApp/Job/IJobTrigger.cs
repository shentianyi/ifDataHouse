using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;

namespace Brilliantech.Tsk.Manage.WebApp.Job
{
    public interface IJobTrigger
    {
        string Name
        {
            get;
        }
        //IJobDetail Job
        //{
        //    get;
        //}
        //ICronTrigger Trigger
        //{
        //    get;
        //}

        void Run();
    }
}