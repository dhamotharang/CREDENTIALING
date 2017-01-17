using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpecialty
{
    public class PracticeInterest
    {
        public PracticeInterest()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PracticeInterestID { get; set; }

        [Required]
        public string Interest { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
