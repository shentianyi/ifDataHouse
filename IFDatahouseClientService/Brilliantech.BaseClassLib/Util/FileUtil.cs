using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Brilliantech.BaseClassLib.Util
{
    public class FileUtil
    {
        public static List<string> GetAllFilesFromDirectory(string directory)
        {
            try
            {
                return Directory.GetFiles(directory.Trim()).ToList();
            }
            catch (Exception e)
            {
                LogUtil.Logger.Error(e.Message);
                return null;
            }
        }
        public static bool IsFileClosed(string fileName)
        {
            try
            {
                using (File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                LogUtil.Logger.Warn(fileName + "not closed." + e.Message);
                return false;
            }
        }
    }
}
