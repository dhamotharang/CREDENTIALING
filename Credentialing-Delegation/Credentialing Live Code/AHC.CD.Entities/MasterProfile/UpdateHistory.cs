using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile
{
   public  class UpdateHistory
    {
       public UpdateHistory()
       {
           this.UpdatedDate = DateTime.Now.ToUniversalTime();
       }
        public  int  UpdateHistoryID{ get; set; }

        public int ProfileIDOfRecord { get; set; }

        public string OldRecord { get; set; }

        public int SectionTableID { get; set; }

        public string SectionName { get; set; }

        public int? UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public CDUser UpdatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedDate { get; set; }

        
    }
}
