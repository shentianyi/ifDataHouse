using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.BaseClassLib.Util;


namespace Brilliantech.DatahouseService.PackSys
{
    public class Conf
    {
        private static string packFilePath; 
        private static int scanInt = 10000; 
        private static char dataSpliter = ';';
        private static int packParamCount = 0;
     
        static Conf()
        {
            ConfigUtil conf = new ConfigUtil("FileScanConfig", "conf.ini");
            packFilePath = conf.Get("PackFilePath");           
            int.TryParse(conf.Get("ScanInt"), out scanInt); 
            int c = 59;
            int.TryParse(conf.Get("FileDataSpliter").Trim(), out c);
            dataSpliter = (char)c;
            int.TryParse(conf.Get("PackParamCount"), out packParamCount);
        }
        public static string PackFilePath
        {
            get { return Conf.packFilePath; }
        }
         
        public static int ScanInt
        {
            get { return Conf.scanInt; }
        } 
        public static char DataSpliter
        {
            get { return Conf.dataSpliter; }
        }
        public static int PackParamCount
        {
            get { return Conf.packParamCount; }
        }
    }
}
