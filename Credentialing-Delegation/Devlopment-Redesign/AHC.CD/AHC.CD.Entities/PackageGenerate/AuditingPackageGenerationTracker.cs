using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.PackageGenerate
{
    public class AuditingPackageGenerationTracker
    {
        public AuditingPackageGenerationTracker()
        {
            GeneratedDate = DateTime.Now;
        }

        public int AuditingPackageGenerationTrackerID { set; get; }       

        public string PackageFilePath { get; set; }

        public string GeneratedByName { get; set; }

        public int? GeneratedByID { get; set; }
        [ForeignKey("GeneratedByID")]
        public CDUser GeneratedBy { get; set; }

        public int? ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public Profile GeneratedFor { get; set; }        

        [Column(TypeName = "datetime2")]
        public DateTime GeneratedDate { get; set; }
    }
}
