using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class WorkersCompensationInfoHistory
    {
        public WorkersCompensationInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int WorkersCompensationInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ExpirationDate { get; set; }


        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
