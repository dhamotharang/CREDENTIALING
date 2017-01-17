using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class OtherWorkInfo
    {
        public OtherWorkInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int OtherWorkInfoID { get; set; }

        public string DepartureReason { get; set; }
        
        [Column(TypeName="datetime2")]
        [Required]
        public DateTime StartDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
