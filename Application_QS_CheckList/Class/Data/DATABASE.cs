using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Application_QS_CheckList.Class.Data
{
    public class DATABASE
    {
        [XmlAttribute("MSSQLCONNSTR")]
        public string MssqlConnStr { get; set; }

        public DATABASE() { }

        public DATABASE DeepClone()
        {
            using (MemoryStream memoryStream = new MemoryStream(10))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(memoryStream) as DATABASE;
            }
        }
    }
}
