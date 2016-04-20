using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MondayDriver
{
    class FileHandler
    {
        public static DriveInfo[] GetAllDrives()
        {
            DriveInfo[] allDrives;
            try
            {
                allDrives = DriveInfo.GetDrives();
            }
            catch (Exception)
            {
                allDrives = null;
            }
            return allDrives;
        }

        public static FileInfo[] GetAllFiles(string dirPath)
        {
            DirectoryInfo dirInfo = null;
            FileInfo fileInfo = null;
            FileInfo[] files = null;
            try
            {
                dirInfo = new DirectoryInfo(dirPath);
                fileInfo = new FileInfo(dirPath);
                files = dirInfo.GetFiles();
            }
            catch (Exception)

            { files = null; }

            return files;
        }

        public static DirectoryInfo[] GetAllDirectories(string dirPath)
        {
            DirectoryInfo dirinfo = null;
            DirectoryInfo[] dirs = null;
            try
            {
                dirinfo = new DirectoryInfo(dirPath);
                dirs = dirinfo.GetDirectories();
            }
            catch (Exception)
            { dirs = null; }

            return dirs;
        }

        public static bool DeleteFile(string filePath)
        {
            bool isSuccessfull;
            try
            {
                File.Delete(filePath);
                isSuccessfull = true;
            }
            catch (Exception)
            {
                isSuccessfull = false;
            }
            return isSuccessfull;
        }
    }
}
