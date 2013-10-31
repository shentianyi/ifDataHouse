using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Brilliantech.DatahouseService.ServiceProvider;
using Brilliantech.BaseClassLib.Util;
using Brilliantech.PackSysDataService;

namespace Test.DatahouseService.ConsoleTip
{
    class Program
    {

        static void Main(string[] args)
        {
            // PackDbDataHandler h = new PackDbDataHandler();
            // h.WritePackItemViewToFileBy(DateTime.Now.AddDays(-20), DateTime.Now, "c:\\packdata\\bba.txt");
            //Console.WriteLine(string.Format("{0}{4}{1}{4}{2}{4}{3}","item.partNr","item.packageID","item.projectID","item.packagingTime","-"));
            // DateTime date = DateTime.Parse("2013-10-30 16:07:15");
            DateTime dt = DateTime.Now;
                string ds=dt.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime dta = DateTime.Parse(ds);

            Console.WriteLine(TimeUtil.GetMilliseconds(dt));
            Console.WriteLine(TimeUtil.GetMilliseconds(dt.ToUniversalTime()));
            Console.WriteLine(ds);
            Console.WriteLine(TimeUtil.GetMilliseconds(dta));
            Console.WriteLine(TimeUtil.GetMilliseconds(dta.ToUniversalTime()));
            Console.Read();
        }
    }
    class Tester{  public void packTest(object o)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("{0}--{1}",o,i);
                try
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data.Add("entityId", "");
                    data.Add("packTime", TimeUtil.GetMilliseconds(DateTime.Now).ToString());
                    data.Add("productNr", Guid.NewGuid().ToString());
                    data.Add("partId", "91G104803");
                    LogUtil.Logger.Error(data);
                    Servicer service = new Servicer();
                    service.AddProductPack(data);
                }
                catch (Exception e)
                {
                   LogUtil.Logger.Error(e.Message);
                    //Brilliantech.Packaging.EpmIntegration.Util.LogUtil.Logger.Error(e.Message);
                }
            }
        }
    }
}
