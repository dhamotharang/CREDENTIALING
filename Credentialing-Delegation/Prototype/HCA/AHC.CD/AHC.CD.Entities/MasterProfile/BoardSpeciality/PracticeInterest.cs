using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpeciality
{
    public class PracticeInterest
    {
        public PracticeInterest()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PracticeInterestID { get; set; }

        public string Title { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
