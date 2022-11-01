using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Application_QS_CheckList.Helper
{
    public static class LogHelper
    {
        private static string ClassCode = "H03";

        public enum LOG_TYPE
        {
            NORMAL,
            ERROR,
            RAW
        }

        public static void WriteLogFile(LOG_TYPE type, DateTime LogTime, string Msg)
        {
            //Create File's Path
            string Path = CreateFilePath(type, LogTime);
            Class.FileIOTool.WrtieOnFile(Path, LogTime.ToString("yyyy-MM-dd HH:mm:ss =>") + "\t" + Msg);
        }

        private static string CreateFilePath(LOG_TYPE type, DateTime LogTime)
        {
            if (type == LOG_TYPE.NORMAL)
            {
                return System.IO.Path.Combine(Helper.GeneralHelper.APP_START_PATH, System.IO.Path.Combine("Normal", "Normal" + "_" + LogTime.ToString("yyyyMMdd") + ".log"));
            }
            else if (type == LOG_TYPE.ERROR)
            {
                return System.IO.Path.Combine(Helper.GeneralHelper.APP_START_PATH, System.IO.Path.Combine("Error", "Error" + "_" + LogTime.ToString("yyyyMMdd") + ".log"));
            }
            else if (type == LOG_TYPE.RAW)
            {
                return System.IO.Path.Combine(Helper.GeneralHelper.APP_START_PATH, System.IO.Path.Combine("Raw", "Raw" + "_" + LogTime.ToString("yyyyMMdd") + ".log"));
            }
            else
            {
                return System.IO.Path.Combine(Helper.GeneralHelper.APP_START_PATH, LogTime.ToString("yyyyMMdd") + ".log");
            }

        }

    }
}
