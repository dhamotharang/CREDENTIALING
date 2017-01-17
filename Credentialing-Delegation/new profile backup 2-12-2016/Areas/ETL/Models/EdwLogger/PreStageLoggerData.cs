using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.ETL.Models.EdwLogger
{
    public class PreStageLoggerData
    {
        public string DataSource { get; set; }
        public string FileDate { get; set; }
        public string FileLocation { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
        public string LoggerId { get; set; }
        public string MasterEDWLoggerId { get; set; }
        public string moment { get; set; }
        public string plan { get; set; }
        public string serial { get; set; }
    }
}