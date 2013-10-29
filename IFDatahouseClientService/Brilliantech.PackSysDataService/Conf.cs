using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.IO;
using Brilliantech.BaseClassLib.Util;

namespace Brilliantech.PackSysDataService
{
    public class Conf
    {
        private static ConfigUtil conf = new ConfigUtil("PackDBDataConf", "conf.ini");
        private static string packDataPath = "C:\\PackData";
        private static int readDbInterval = 5000;
        // db conf
        private static string host;
        private static string db;
        private static string user;
        private static string pass;

        private static string connstr;

        // read start time
        private static DateTime dataReadStartTime = DateTime.Now;
        private static char dataSpliter = ';';
        static Conf()
        {
            packDataPath = conf.Get("FilePath");
            if (!Directory.Exists(packDataPath))
            {
                Directory.CreateDirectory(packDataPath);
            } 
            int.TryParse(conf.Get("ReadInterval"), out readDbInterval);
            // db conf
             host = conf.Get("Host");
             db = conf.Get("DB");
             user = conf.Get("User");
             pass = conf.Get("Pass");

             connstr = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",host,db,user, pass);
            // read start time 
             if (conf.Get("DataReadStartTime") != null && conf.Get("DataReadStartTime").Length > 0)
                 DateTime.TryParse(conf.Get("DataReadStartTime"), out dataReadStartTime);
             else
                 DataReadStartTime = DateTime.Now;

             int c = 59;
             int.TryParse(conf.Get("FileDataSpliter").Trim(), out c);
             dataSpliter = (char)c;
        }

        public static string PackDataPath
        {
            get { return Conf.packDataPath; }
        }

        public static int ReadDbInterval
        {
            get { return Conf.readDbInterval; }
        }

        public static string Connstr
        {
            get { return Conf.connstr; }
        }


        public static DateTime DataReadStartTime
        {
            get { return Conf.dataReadStartTime; }
            set { Conf.dataReadStartTime = value;
            conf.Set("DataReadStartTime",value);
            conf.Save();
            }
        }

    }
}
