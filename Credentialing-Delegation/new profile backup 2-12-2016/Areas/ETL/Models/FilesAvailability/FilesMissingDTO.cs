using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.ETL.Models.FilesAvailability
{
    public class FilesMissingDTO
    {
        public int Year { get; set; }
        public int ACO { get; set; }
        public int Census { get; set; }
        public int Roster { get; set; }
        public int MonthlyClaims { get; set; }
        public int Sumit { get; set; }
        public int Raps { get; set; }
    }
}