using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Framwork.Utils.ConfigUtil;

namespace Brilliantech.Tsk.Service.Wcf.Config
{
    public class TskConfig
    {
        private static ConfigUtil config;
        private static int dataCount = 0;
        private static char dataSpliter = ';';
        static TskConfig()
        {
            try
            {
                config = new ConfigUtil(@"Ini\TskConfig.ini");
                int.TryParse(config.Get("DataCount", "DataFormat"), out dataCount);
                dataSpliter = config.Get("DataSpliter", "DataFormat").ToCharArray().First();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int DataCount
        {
            get { return TskConfig.dataCount; }
            set
            {
                TskConfig.dataCount = value;
                config.Set("DataCount", value, "DataFormat");
                config.Save();
            }
        }


        public static char DataSpliter
        {
            get { return TskConfig.dataSpliter; }
            set
            {
                TskConfig.dataSpliter = value;
                config.Set("DataSpliter", value, "DataFormat");
                config.Save();
            }
        }
    }
}
