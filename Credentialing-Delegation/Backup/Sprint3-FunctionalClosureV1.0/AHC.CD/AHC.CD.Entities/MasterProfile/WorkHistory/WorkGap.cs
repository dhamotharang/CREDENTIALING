using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class WorkGap
    {
        public WorkGap()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int WorkGapID { get; set; }

        [Column(TypeName="datetime2")]
        [Required]
        public DateTime StartDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
