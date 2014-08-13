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


        //private static void MoveFile(string sourcePath, string targetPath, bool autoRename = true)
        //{
        //    lock (fileLocker)
        //    {
        //        if (File.Exists(sourcePath))
        //        {
        //            if (autoRename)
        //            {
        //                targetPath = Path.Combine(Path.GetDirectoryName(targetPath), Guid.NewGuid().ToString() + Path.GetExtension(targetPath));
        //            }
        //        }
        //        File.Move(sourcePath, targetPath);
        //        LogUtil.Logger.Info("【文件移动】【自】" + sourcePath + "【至】" + targetPath);
        //    }
        //}
        //private static bool CheckDirectory(string dirName, bool autoCreate = true)
        //{
        //    bool result = false;
        //    lock (dirLocker)
        //    {
        //        if (Directory.Exists(dirName))
        //        {
        //            result = true;
        //        }
        //        else
        //        {
        //            if (autoCreate)
        //            {
        //                Directory.CreateDirectory(dirName);
        //                result = true;
        //            }
        //        }
        //    }
        //    return result;
        //}

    }
}
