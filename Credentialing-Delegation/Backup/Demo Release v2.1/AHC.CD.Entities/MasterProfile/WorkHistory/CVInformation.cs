using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
   public class CVInformation
    {
        public CVInformation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CVInformationID { get; set; }

        public string CVDocumentPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
