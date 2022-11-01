using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Application_QS_CheckList
{
    static class Program
    {
        static string ClassCode = "P00";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string FunctionCode = "F00";
            string ThisParentCode = string.Format("[{0}:{1}]", ClassCode, FunctionCode);
            try
            {

                if (!Helper.ConfigHelper.Construct(ThisParentCode))
                {
                    MessageBox.Show("Failed to construct application configurations.\nPlease contact your administrator.");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new InputData());

                Helper.LogHelper.WriteLogFile(Helper.LogHelper.LOG_TYPE.NORMAL, DateTime.Now, string.Format("[{0}:{1}:{2}]NORMAL MESSAGE: Application Configuration Success\n", ThisParentCode, ClassCode, FunctionCode));

            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteLogFile(Helper.LogHelper.LOG_TYPE.ERROR, DateTime.Now, string.Format("[{0}]ERROR MESSAGE:{1}\nSTACK TRACE:{2}\n", ThisParentCode, ex.Message, ex.StackTrace));

            }
            
        }
    }
}
