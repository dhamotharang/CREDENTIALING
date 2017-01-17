using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class PublicHealthService
    {
        public PublicHealthService()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PublicHealthServiceID { get; set; }

        [Required]
        public string LastLocation { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
