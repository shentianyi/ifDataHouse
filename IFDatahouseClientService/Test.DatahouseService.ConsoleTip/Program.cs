using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Brilliantech.DatahouseService.ServiceProvider;

namespace Test.DatahouseService.ConsoleTip
{
    class Program
    {

        static void Main(string[] args)
        {
            for (int i = 0; i < 200; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(new Tester().packTest),i);
            }
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
                    data.Add("packTime", Brilliantech.DatahouseService.Util.TimeUtil.GetMilliseconds(DateTime.Now).ToString());
                    data.Add("productNr", Guid.NewGuid().ToString());
                    data.Add("partId", "91G104803");
                    Brilliantech.DatahouseService.Util.LogUtil.Logger.Error(data);
                    Servicer service = new Servicer();
                    service.AddProductPack(data);
                }
                catch (Exception e)
                {
                    Brilliantech.DatahouseService.Util.LogUtil.Logger.Error(e.Message);
                    //Brilliantech.Packaging.EpmIntegration.Util.LogUtil.Logger.Error(e.Message);
                }
            }
        }
    }
}
