using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Application_QS_CheckList.Helper
{
    class GetDataHelper
    {
        string ClassCode = "H02";

        public static string GetWMIData(string Caption, string Source)
        {
            string ObjectString = "NA";
            try
            {
                

                ManagementObjectSearcher WmiObject = new ManagementObjectSearcher("Select * From " + Source);

                foreach (ManagementObject Object in WmiObject.Get())
                {
                    ObjectString = Object.GetPropertyValue(Caption).ToString();
                }

                
            }
            catch(Exception ex) 
            {
                
            }
            return ObjectString;
        }

        public static string GetWMIDataGet(string Caption, string Source, string Here)
        {
            string ObjectString = "NA";

            ManagementObjectSearcher WmiObject = new ManagementObjectSearcher("Select * From " + Source+ "where Column ="+Here);

            foreach (ManagementObject Object in WmiObject.Get())
            {
                ObjectString = Object.GetPropertyValue(Caption).ToString();
            }

            return ObjectString;
        }

        public static string GetHardDisk()
        {
            string HDDinfo = "NA";

            StringBuilder texthdd = new StringBuilder();
            //stringbuilder
            foreach (System.IO.DriveInfo label in System.IO.DriveInfo.GetDrives())
            {
                if (label.IsReady)
                {
                    HDDinfo = label.Name + Helper.GeneralHelper.ByteToGB(label.TotalSize.ToString()) +"/"+ Helper.GeneralHelper.ByteToGB(label.AvailableFreeSpace.ToString())+" ";

                    texthdd.Append(HDDinfo);
                }
            }

            HDDinfo = texthdd.ToString();

            return HDDinfo;
        }

        public static string GetInstalledAntivirus()
        {
            string wmipathstr = @"\\" + Environment.MachineName + @"\root\SecurityCenter2";
            string AvString = "NA";
            
            try
            {
                ManagementObjectSearcher AVObject = new ManagementObjectSearcher(wmipathstr, "Select * from AntiVirusProduct");
                foreach (ManagementObject Object in AVObject.Get())
                {
                    AvString = Object["Displayname"].ToString() +" : "+ Object["ProductState"].ToString();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return AvString;
        }
       
    }
}
