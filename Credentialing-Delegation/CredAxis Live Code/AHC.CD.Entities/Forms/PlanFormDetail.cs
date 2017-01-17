using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Forms
{
    public class PlanFormDetail
    {
        public int PlanFormDetailID { get; set; }

        public string PlanFormName { get; set; }

        public string PlanFormFileName { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
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

        public int? PlanFormPayerID { get; set; }
        [ForeignKey("PlanFormPayerID")]
        public PlanFormPayer PlanFormPayer { get; set; }


        public int? PlanFormRegionID { get; set; }
        [ForeignKey("PlanFormRegionID")]
        public PlanFormRegion PlanFormRegion { get; set; }
    }
}
