using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Brilliantech.Tsk.Service.Wcf;
using Brilliantech.Framwork.Utils.LogUtil;

namespace Brilliantech.Tsk.Host.ConsoleCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            LogUtil.Logger.Info("Service Starting");
            ServiceHost host = new ServiceHost(typeof(InspectService));
            host.Close();
            Console.Read();
        }
    }
}
