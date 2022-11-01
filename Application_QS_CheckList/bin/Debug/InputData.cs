using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;

namespace Application_QS_CheckList
{
    public partial class InputData : Form
    {
        string ServerModel = Class.Data.Constant.NotAvailable;
        string Processor = Class.Data.Constant.NotAvailable;
        string SerialNumber = Class.Data.Constant.NotAvailable;
        string ServerName = Class.Data.Constant.NotAvailable;
        string OperatingSystemnSP = Class.Data.Constant.NotAvailable;
        string HDDnFreeSpace = Class.Data.Constant.NotAvailable;
        string RAMCapacity = Class.Data.Constant.NotAvailable;
        string CPUUtils = Class.Data.Constant.NotAvailable;
        string CheckAntivirus = Class.Data.Constant.NotAvailable;
        string IPaddress = Class.Data.Constant.NotAvailable;
        string SQLVer = Class.Data.Constant.NotAvailable;
        string TeamViewerData = Class.Data.Constant.NotAvailable;
        string MSOffice = Class.Data.Constant.NotAvailable;

        public InputData()
        {
            InitializeComponent();
        }
     

        private void GenerateBtn_Click_1(object sender, EventArgs e)
        {
            PrintData();
          
        }

        private void InputData_Load(object sender, EventArgs e)
        {
            GetData();
            PrintData();
                       
        }

        public void GetData()
        {
            ServerModel = Helper.GetDataHelper.GetWMIData("Model", "Win32_ComputerSystem");
            Processor = Helper.GetDataHelper.GetWMIData("Name", "Win32_Processor");
            SerialNumber = Helper.GetDataHelper.GetWMIData("SerialNumber", "Win32_OperatingSystem");
            ServerName = Helper.GetDataHelper.GetWMIData("Name", "Win32_ComputerSystem");
            OperatingSystemnSP = Helper.GetDataHelper.GetWMIData("Caption", "Win32_OperatingSystem");
            HDDnFreeSpace = Helper.GetDataHelper.GetWMIData("FreePhysicalMemory", "Win32_OperatingSystem");
            RAMCapacity = Helper.GetDataHelper.GetWMIData("TotalVisibleMemorySize", "Win32_OperatingSystem");
            CPUUtils = Helper.GetDataHelper.GetWMIData("LoadPercentage", "Win32_Processor");
            CheckAntivirus = Helper.GetDataHelper.GetInstalledAntivirus();
            IPaddress = Helper.GetIpAddressHelper.GetIpAddress();
            SQLVer = Class.MSSQLTool.GetSQLVersion(Helper.ConfigHelper.CONFIG.DATABASE.MssqlConnStr);
            TeamViewerData = Class.RegistryTool.CollectTeamviewerId();
            MSOffice = Class.RegistryTool.GetOfficeVersion();

        }

        public void PrintData()
        {
            ServerModelnProc.Text = ServerModel + Processor;
            WindowServerKey.Text = SerialNumber;

            RamCapa.Text = Helper.GeneralHelper.ConvertToPercent(HDDnFreeSpace, RAMCapacity).ToString() + "% - Free Space";
            label1.Text = Helper.GeneralHelper.CheckRamCapacity(Helper.GeneralHelper.ConvertToPercent(HDDnFreeSpace, RAMCapacity));
   
            ServerNameDesc.Text = ServerName;
            OperatingSystem.Text = OperatingSystemnSP + " " + Environment.OSVersion.ServicePack;
            HardDiskCapa.Text = Helper.GetDataHelper.GetHardDisk().Replace("\\","");
            CpuUtil.Text = CPUUtils + "%";
            NC.Text = IPaddress;
            AS.Text = CheckAntivirus;
            SQL.Text = SQLVer;
            MS.Text = MSOffice;
            TeamViewer.Text = "ID: " + TeamViewerData + " " + "PW: " + string.Empty; ;
            Others.Text = "NA";


        }

        private void Pingbtn_Click(object sender, EventArgs e)
        {
            PingStatus.Text = Helper.GetIpAddressHelper.GetPingResult(InputPing.Text);
         
        }

        
    }
}
