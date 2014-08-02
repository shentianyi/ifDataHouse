using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;

namespace Brilliantech.Tsk.QuartzTask.WS.Job
{
    public interface IJobTrigger
    {
        string Name
        {
            get;
        } 
        void Run();
    }
}