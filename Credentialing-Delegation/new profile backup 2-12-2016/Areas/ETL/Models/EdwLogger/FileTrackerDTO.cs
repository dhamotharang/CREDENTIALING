using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.ETL.Models.EdwLogger
{
    public class FileTrackerDTO
    {
        public int Sno { get; set; }
        public string FileLocation { get; set; }
        public string Plan { get; set; }
        public string DataSource { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public string FileDate { get; set; }
        public string Remarks { get; set; }
    }
}