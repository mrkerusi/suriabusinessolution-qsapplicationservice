using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Application_QS_CheckList.Helper
{
    public static class ConfigHelper
    {
        public static string ClassCode = "H00";
        private static Class.Data.CONFIG _CONFIG = null;
        private static string _CONFIG_FILE_PATH = string.Empty;
        private static string _MSSQL_CONN_STR = string.Empty;
        private static XmlSerializer ConfigurationSerializer = null;
        private static XmlSerializer DataXmlSerializer = null;

        public static Class.Data.CONFIG CONFIG
        {
            get { return _CONFIG; }
            set { _CONFIG = value; }
        }

        public static string MSSQL_CONN_STR
        {
            get { return _MSSQL_CONN_STR; }
            set { _MSSQL_CONN_STR = value; }
        }

        public static bool Construct(string ParentCode)
        {
            string FunctionCode = "F00";
            bool Result = false;

            try
            {
                ConfigurationSerializer = new XmlSerializer(typeof(Class.Data.CONFIG));

                XmlDocument XmlDocument = new System.Xml.XmlDocument();
                XmlDocument.Load(Path.Combine(Helper.GeneralHelper.APP_START_PATH, "QS_App.config"));
                string ConfigurationXML = XmlDocument.InnerXml;

                TextReader TR = new StringReader(ConfigurationXML);
                Helper.ConfigHelper.ReadResponseStr("CONFIG", TR, ParentCode);
               
                //if (!Directory.Exists(Helper.ConfigHelper.CONFIG.FILEPATH.EXPORT_FILE_PATH))
                //{
                //    Directory.CreateDirectory(Helper.ConfigHelper.CONFIG.FILEPATH.EXPORT_FILE_PATH);
                //}
                //if (!Directory.Exists(Helper.ConfigHelper.CONFIG.FILEPATH.BACKUP_FILE_PATH)) 
                //{
                //    Directory.CreateDirectory(Helper.ConfigHelper.CONFIG.FILEPATH.BACKUP_FILE_PATH);
                //}

                Helper.LogHelper.WriteLogFile(Helper.LogHelper.LOG_TYPE.NORMAL, DateTime.Now, string.Format("[{0}:{1}:{2}]NORMAL MESSAGE:LoadInterfaceResult:{3}", ParentCode, ClassCode, FunctionCode, string.Format("Interface Version {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version)));

                Result = true;
            }
            catch (Exception ex)
            {
                Helper.LogHelper.WriteLogFile(Helper.LogHelper.LOG_TYPE.ERROR, DateTime.Now, string.Format("[{0}:{1}:{2}]ERROR MESSAGE:{3}\nSTACK TRACE:{4}\n", ParentCode, ClassCode, FunctionCode, ex.Message, ex.StackTrace));
            }

            return Result;
        }

        public static void ReadResponseStr(string type, TextReader XmlResponse, string Parentcode)
        {
            string FunctionCode = "F01";
            try
            {
                switch (type)
                {
                    case "CONFIG":
                        {
                            _CONFIG = null;
                            _CONFIG = (Class.Data.CONFIG)ConfigurationSerializer.Deserialize(XmlResponse);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex) 
            {
                Helper.LogHelper.WriteLogFile(Helper.LogHelper.LOG_TYPE.ERROR, DateTime.Now, string.Format("[{0}:{1}:{2}]ERROR MESSAGE:{3}\nSTACK TRACE:{4}\n", Parentcode, ClassCode, FunctionCode, ex.Message, ex.StackTrace));
            }
        }

    }
}
