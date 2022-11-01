using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Data;

namespace Application_QS_CheckList.Class
{
    public static class FileIOTool
    {
        private static string _UnSaveErrorMsg = string.Empty;
        private static string _Source = "CSV_BluStar_Server";
        private static string _Log = "Application";
        private static string _Event = "";

        public static void WrtieOnFile(string PathStr, string Text)
        {
            int RetryCounter = 0;

            while (RetryCounter < 3)
            {
                try
                {
                    if (File.Exists(PathStr))
                    {
                        StreamWriter SW = File.AppendText(PathStr);
                        SW.WriteLine(Text);
                        SW.Close();
                        SW.Dispose();
                    }
                    else
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(PathStr)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(PathStr));
                        }

                        StreamWriter SW = File.AppendText(PathStr);
                        SW.WriteLine(Text);
                        SW.Close();
                        SW.Dispose();
                    }

                    break;
                }
                catch (Exception ex)
                {
                    RetryCounter++;

                    if (RetryCounter >= 30)
                    {
                        _UnSaveErrorMsg = string.Format("{0:yyyy-MM-dd HH:mm:ss} => Message:{1} \nStackTrace:{2}", DateTime.Now, ex.Message, ex.StackTrace);

                        if (!EventLog.SourceExists(_Source))
                            EventLog.CreateEventSource(_Source, _Log);

                        _Event = _UnSaveErrorMsg;

                        EventLog.WriteEntry(_Source, _Event,
                            EventLogEntryType.Warning, 234);

                    }
                    else
                    {
                        System.Threading.Thread.Sleep(RetryCounter * 100);
                    }
                }
            }
        }

        public static void FileAppendText(string PathStr, string Text)
        {
            try
            {
                if (File.Exists(PathStr))
                {
                    StreamWriter SW = File.AppendText(PathStr);
                    SW.WriteLine(Text);
                    SW.Close();
                    SW.Dispose();
                }
                else
                {
                    if (!Directory.Exists(Path.GetDirectoryName(PathStr)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(PathStr));
                    }

                    StreamWriter SW = File.AppendText(PathStr);
                    SW.WriteLine(Text);
                    SW.Close();
                    SW.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FileCopy(string PathStr, string NewPathStr, bool Overwrite)
        {
            try
            {
                File.Copy(PathStr, NewPathStr, Overwrite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FileCreate(string PathStr, string Text, bool Overwrite)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(PathStr)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(PathStr));
                }
                else
                {
                    if (File.Exists(PathStr))
                    {
                        File.Delete(PathStr);
                    }
                }

                StreamWriter SW = File.AppendText(PathStr);
                SW.WriteLine(Text);
                SW.Close();
                SW.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FileDelete(string PathStr)
        {
            try
            {
                File.Delete(PathStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void FileMove(string PathStr, string NewPathStr, bool Overwrite)
        {
            try
            {
                if (Overwrite)
                {
                    if (File.Exists(NewPathStr))
                    {
                        File.Delete(NewPathStr);
                    }
                }
                File.Move(PathStr, NewPathStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool FileExist(string PathStr) 
        {
            try
            {
                bool Result = false;

                if (Directory.GetFiles(PathStr, "*.CSV", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    Result = true;
                }
                return Result;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            
        }

        public static void ForceCreateNewFile(string PathStr, string Text)
        {
            int RetryCounter = 0;

            while (RetryCounter < 3)
            {
                try
                {

                    if (!Directory.Exists(Path.GetDirectoryName(PathStr)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(PathStr));
                    }

                    StreamWriter SW = File.AppendText(PathStr);
                    SW.WriteLine(Text);
                    SW.Close();
                    SW.Dispose();

                    break;
                }
                catch (Exception ex)
                {
                    RetryCounter++;

                    if (RetryCounter >= 30)
                    {
                        _UnSaveErrorMsg = string.Format("{0:yyyy-MM-dd HH:mm:ss} => Message:{1} \nStackTrace:{2}", DateTime.Now, ex.Message, ex.StackTrace);

                        if (!EventLog.SourceExists(_Source))
                            EventLog.CreateEventSource(_Source, _Log);

                        _Event = _UnSaveErrorMsg;

                        EventLog.WriteEntry(_Source, _Event,
                            EventLogEntryType.Warning, 234);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(RetryCounter * 100);
                    }
                }
            }
        }

        /// <summary>
        /// Export DataTable Columns , Rows to file
        /// </summary>
        /// <param name="datatable">The datatable to exported from.</param>
        /// <param name="delimited">string for delimited exported row items</param>
        /// <param name="exportcolumnsheader">Including columns header with exporting</param>
        /// <param name="file">The file path to export to.</param>
        public static void ExportDataTabletoFile(DataTable datatable, string delimited, bool exportcolumnsheader, string file)
        {
            StreamWriter strFile = new StreamWriter(file, false, System.Text.Encoding.Default);

            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (i == datatable.Rows.Count - 1)
                {
                    SB.Append(datatable.Rows[i]["LineData"]);
                }
                else
                {
                    SB.Append(datatable.Rows[i]["LineData"] + Environment.NewLine);
                }

            }

            strFile.Write(SB.ToString());

            strFile.Flush();
            strFile.Close();
        }
    }
}
