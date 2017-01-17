using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.PSV
{
    public class PlanPSVDetail
    {
        public PlanPSVDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanPSVDetailID { get; set; }

        public int? PSVDetailID { get; set; }
        [ForeignKey("PSVDetailID")]
        public PSVDetail PSVDetail { get; set; }

        public int? PlanID { get; set; }
        [ForeignKey("PlanID")]
        public Plan Plan { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
