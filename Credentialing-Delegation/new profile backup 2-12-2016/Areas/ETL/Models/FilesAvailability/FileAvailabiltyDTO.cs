using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.ETL.Models.FilesAvailability
{
    public class FileAvailabiltyDTO
    {
        public string DataSource { get; set; }
        public int FilesAvailable { get; set; }
        public int FilesPrestaged { get; set; }
        public int FilesNeedToPrestage { get; set; }
    }
}