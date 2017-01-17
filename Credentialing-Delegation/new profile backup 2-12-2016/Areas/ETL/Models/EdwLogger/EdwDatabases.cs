using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.ETL.Models.EdwLogger
{
    public class EdwDatabases
    {
        public int SNo { get; set; }
        public string DataBaseName { get; set; }
        public string SizeMb { get; set; }
        public string SizeGb { get; set; }
    }
}