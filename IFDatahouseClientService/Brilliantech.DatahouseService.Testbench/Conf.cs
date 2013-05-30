using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliantech.DatahouseService.Util;


namespace Brilliantech.DatahouseService.Testbench
{
    public class Conf
    {
        private static string testFilePath;
        private static string testErrorFilePath;
        private static string testMovedFilePath;
        private static int testScanInt = 2;
        private static int testParamCount = 0;
        private static char testDataSpliter = ';';

     
        static Conf()
        {
            ConfigUtil conf = new ConfigUtil("TestbechConfig", "conf.ini");
            testFilePath = conf.Get("FilePath");
            testErrorFilePath = conf.Get("ErrorFilePath");
            testMovedFilePath = conf.Get("MovedFilePath");
            int.TryParse(conf.Get("ScanInt"), out testScanInt);
            int.TryParse(conf.Get("ParamCount"), out testParamCount);
            int c = 59;
            int.TryParse(conf.Get("FileDataSpliter").Trim(), out c);
            testDataSpliter = (char)c;
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
        public static int TestScanInt
        {
            get { return Conf.testScanInt; }
        }
        public static int TestParamCount
        {
            get { return Conf.testParamCount; }
        }
        public static char TestDataSpliter
        {
            get { return Conf.testDataSpliter; }
        }

    }
}
