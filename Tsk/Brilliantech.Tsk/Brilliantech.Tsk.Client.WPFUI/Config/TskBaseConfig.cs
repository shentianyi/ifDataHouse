using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.Framwork.Utils.ConfigUtil;

namespace Brilliantech.Tsk.Client.WPFUI.Config
{
    public class TskBaseConfig
    {
        private static ConfigUtil config;
        private static string remoteServerIP;
        private static string remoteServerPort;
        private static string inspectServiceAddress;
        private static string dataFilePath;
        private static string errorFilePath;
        private static string movedFilePath;
        private static bool deleteFileAfterRead = false;
        private static int scanInterval = 2;

        static TskBaseConfig()
        {
            try
            {
                config = new ConfigUtil("BaseConfig", @"Ini\TskClientConfig.ini");
                remoteServerIP =config.Get("RemoteServerIP") ;
                remoteServerPort = config.Get("RemoteServerPort");
                dataFilePath = config.Get("DataFilePath");
                errorFilePath = config.Get("ErrorFilePath");
                movedFilePath = config.Get("MovedFilePath");

                if (!bool.TryParse(config.Get("DeleteFileAfterRead"), out deleteFileAfterRead))
                {
                    deleteFileAfterRead = false;
                }
                if (!int.TryParse(config.Get("ScanInterval"), out scanInterval))
                {
                    scanInterval = 2;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static string RemoteServerIP
        {
            get { return TskBaseConfig.remoteServerIP; }
            set { TskBaseConfig.remoteServerIP = value; }
        }


        public static string RemoteServerPort
        {
            get { return TskBaseConfig.remoteServerPort; }
            set { TskBaseConfig.remoteServerPort = value; }
        }

        public static string InspectServiceAddress
        {
            get
            {
                return string.Format("http://{0}:{1}/Brilliantech.Tsk.Service.Wcf/InspectService/",
                  remoteServerIP,
                  remoteServerPort);
            }
        }

        public static string DataFilePath
        {
            get { return TskBaseConfig.dataFilePath; }
            set { TskBaseConfig.dataFilePath = value; }
        }

        public static string ErrorFilePath
        {
            get { return TskBaseConfig.errorFilePath; }
            set { TskBaseConfig.errorFilePath = value; }
        }

        public static string MovedFilePath
        {
            get { return TskBaseConfig.movedFilePath; }
            set { TskBaseConfig.movedFilePath = value; }
        }


        public static bool DeleteFileAfterRead
        {
            get { return TskBaseConfig.deleteFileAfterRead; }
            set { TskBaseConfig.deleteFileAfterRead = value; }
        }

        public static int ScanInterval
        {
            get { return TskBaseConfig.scanInterval; }
            set { TskBaseConfig.scanInterval = value; }
        }

        public static void Save()
        {
            try
            {
                config.Set("RemoteServerIP", remoteServerIP);
                config.Set("RemoteServerPort", remoteServerPort);
                config.Set("DataFilePath", dataFilePath);
                config.Set("ErrorFilePath", errorFilePath);
                config.Set("MovedFilePath", movedFilePath);
                config.Set("DeleteFileAfterRead", deleteFileAfterRead);
                config.Set("ScanInterval", scanInterval);
                config.Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
