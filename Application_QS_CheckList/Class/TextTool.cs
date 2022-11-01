using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Application_QS_CheckList.Class
{
    public class TextTool
    {
        private string _Filename = string.Empty;

        private List<string> _TextList;

        public string Filename
        {
            get { return _Filename; }
        }
        public List<string> TextList
        {
            get { return _TextList; }
        }

        public TextTool(string filename)
        {
            try
            {
                _Filename = filename;
                _TextList = new List<string>();

                string line;

                StreamReader file = new StreamReader(filename);

                while ((line = file.ReadLine()) != null)
                {
                    _TextList.Add(line);
                }

                file.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TextTool(string Data, bool flag)
        {
            try
            {
                _TextList = new List<string>();

                Stream Stm = new MemoryStream(ASCIIEncoding.Unicode.GetBytes(Data));

                string line;

                StreamReader file = new StreamReader(Stm, Encoding.Unicode);

                while ((line = file.ReadLine()) != null)
                {
                    _TextList.Add(line);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
