using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.ETL.Models.StagingTracker
{
    public class StagingTrackerDTO
    {
        public string plan { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string IPA { get; set; }
        public string MigrationStartDate { get; set; }
        public string MigrationEndtDate { get; set; }
        public string MigrationStatus { get; set; }
        public string FileRecievedDate { get; set; }
        public string Source { get; set; }
        public string StagingType { get; set; }
        public int StagingLoggerID { get; set; }
    }
}
