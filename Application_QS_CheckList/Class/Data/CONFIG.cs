using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Application_QS_CheckList.Class.Data
{
    [Serializable]
    [XmlRoot("CONFIGURATION")]
    public class CONFIG
    {
        [XmlElement("DATABASE")]
        public DATABASE DATABASE { get; set; }

        public CONFIG() { }

        public CONFIG DeepClone()
        {
            using (MemoryStream memoryStream = new MemoryStream(10))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(memoryStream) as CONFIG;
            }
        }
    }
}
