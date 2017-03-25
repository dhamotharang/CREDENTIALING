using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Accessibility
{
    public class FacilityAccessibilityQuestion
    {
        public FacilityAccessibilityQuestion()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FacilityAccessibilityQuestionId { get; set; }

        //[Required]
        [MaxLength(100)]
        //[Index(IsUnique = true)]
        public string Title { get; set; }

        public string ShortTitle { get; set; }

        #region Facility Type

        public int? FacilityTypeID { get; set; }
        [ForeignKey("FacilityTypeID")]
        public FacilityType FacilityType { get; set; }

        #endregion

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
