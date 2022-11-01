using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography;

namespace Application_QS_CheckList.Helper
{
    public static class GeneralHelper
    {
        private static string ClassCode="H01";

        private static string _APP_START_PATH = string.Empty;

        private static bool bInvalidEmail = false;

        public static string APP_START_PATH
        {
            get 
            { 
                _APP_START_PATH = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                return _APP_START_PATH;
            }
        }

        public static string ByteToGB(string HddSpaceHere)
        {
            long hddspaceint = Convert.ToInt64(HddSpaceHere) / 1073741824;

            return hddspaceint.ToString();

        }

        public static string CheckRamCapacity(int percentage)
        {
            string StatusText = string.Empty;
            if (percentage > 50)
            {
                StatusText = "OKAY";
            }
            else if (percentage < 50)
            {
                StatusText = "NOT OKAY";
            }
            else
            {
                StatusText = "UNKNOWN";
            }

            return StatusText;

        }

        public static int ConvertToPercent(string FreeSpace, string TotalSpace)
        {
            int Percentage = Convert.ToInt16((Convert.ToDouble(FreeSpace) / Convert.ToDouble(TotalSpace)) * 100);
            return Percentage;
        }

    }
}
