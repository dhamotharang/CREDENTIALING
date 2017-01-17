using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class ECFMGDetail
    {
        public ECFMGDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ECFMGDetailID { get; set; }

        //[Required] //This can not be mandatory, as not all providers have this number
        [MaxLength(100)]
        //[Index(IsUnique = true)]
        public string ECFMGNumber { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ECFMGIssueDate { get; set; }
        
        //[Required]
        public string ECFMGCertPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
