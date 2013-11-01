using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.BaseClassLib.Util; 


namespace Brilliantech.DatahouseService.Testbench
{
    public class Conf
    {
        private static string testFilePath;
        private static string testErrorFilePath;
        private static string testMovedFilePath;
        private static int scanInt = 2;
        private static int testParamCount = 0;
        private static char dataSpliter = ';';

     
        static Conf()
        {
            ConfigUtil conf = new ConfigUtil("FileScanConfig", "conf.ini");
            testFilePath = conf.Get("TestFilePath");
            testErrorFilePath = conf.Get("TestErrorFilePath");
            testMovedFilePath = conf.Get("TestMovedFilePath");
            int.TryParse(conf.Get("ScanInt"), out scanInt);
            int.TryParse(conf.Get("TestParamCount"), out testParamCount);
            int c = 59;
            int.TryParse(conf.Get("FileDataSpliter").Trim(), out c);
            dataSpliter = (char)c;
        }
        public static string TestFilePath
        {
            get { return Conf.testFilePath; }
        }
        public static string TestErrorFilePath
        {
            get { return Conf.testErrorFilePath; }
        }
        public static string TestMovedFilePath
        {
            get { return Conf.testMovedFilePath; }
        }
        public static int ScanInt
        {
            get { return Conf.scanInt; }
        }
        public static int TestParamCount
        {
            get { return Conf.testParamCount; }
        }
        public static char DataSpliter
        {
            get { return Conf.dataSpliter; }
        }

    }
}
