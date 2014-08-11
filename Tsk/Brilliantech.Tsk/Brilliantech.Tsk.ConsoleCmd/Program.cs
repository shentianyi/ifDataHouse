using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using Brilliantech.Tsk.ConsoleCmd.InspectService;

namespace Brilliantech.Tsk.ConsoleCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "TSK056;91G40538;23431905;23431905;2014-04-01 12:01:15;2014-04-01 12:01:15;20T1906234141213;2014-04-01 14:23:13;15;1";
           // InspectService client = new InspectService();
           // client.CreateInspect(result);
          //  InspectServiceClient client = new InspectServiceClient();
           // client.CreateInspect(result);
            int a = 12;
            int.TryParse("33", out a);
            Console.WriteLine(a);
           Console.WriteLine( int.TryParse("3daff3", out a));
            Console.WriteLine(a);
            Console.Read();
        }
    }
}
