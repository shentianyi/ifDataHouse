using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Framwork.Utils.ConfigUtil;

namespace Brilliantech.Tsk.Service.Wcf.Config
{
    public class MSSqlConfig
    {
        private static ConfigUtil config;
        private static string connectionString;

        static MSSqlConfig()
        {
            try
            {
                config = new ConfigUtil("Connection", "Ini/SqlConfig.ini");
                connectionString = config.Get("ConnectionString");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
                config.Set("ConnectionString", value);
                config.Save();
            }

        }
    }
}
