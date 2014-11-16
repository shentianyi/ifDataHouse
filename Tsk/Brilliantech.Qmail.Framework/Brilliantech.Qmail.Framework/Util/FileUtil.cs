using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Brilliantech.Qmail.Framework.Util
{
    public class FileUtil
    {
        public static string GetExcelPath(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExcelTmp");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return Path.Combine(filePath,fileName);
        }
    }
}
