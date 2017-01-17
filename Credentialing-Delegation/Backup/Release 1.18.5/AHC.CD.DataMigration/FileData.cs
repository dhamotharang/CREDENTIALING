using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AHC.CD.DataMigration
{
    class FileData
    {
        public string XPath { get; set; }
        public XmlNode XmlNode { get; set; }
        public string FullFileName { get; set; }
    }
}
