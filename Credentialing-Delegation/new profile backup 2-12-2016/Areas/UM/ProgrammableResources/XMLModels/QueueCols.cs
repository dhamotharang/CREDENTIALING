using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PortalTemplate.Areas.UM.ProgrammableResources.XMLModels
{
    [Serializable]
    [XmlRoot("QueueCols"), XmlType("QueueCols")]  
    public class QueueCols
    {
        public string Label { get; set; }

        public string Entity { get; set; }

        public string Class { get; set; }
    }
}