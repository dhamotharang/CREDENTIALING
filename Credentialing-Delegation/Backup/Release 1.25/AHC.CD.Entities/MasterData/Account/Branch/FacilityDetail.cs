using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Language;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Branch
{
    public class FacilityDetail
    {
        public FacilityDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FacilityDetailID { get; set; }

        #region Facility Type

        //public int? FacilityTypeID { get; set; }
        //[ForeignKey("FacilityTypeID")]
        //public FacilityType FacilityType { get; set; }

        #endregion

        public virtual FacilityService Service { get; set; }
        public virtual FacilityAccessibility Accessibility { get; set; }
        public virtual FacilityLanguage Language { get; set; }
        public virtual ICollection<FacilityEmployee> Employees { get; set; }
        public virtual ICollection<FacilityWorkHour> FacilityWorkHours { get; set; }
        public virtual PracticeOfficeHour PracticeOfficeHour { get; set; }
        public virtual ICollection<PracticeProvider> MidLevels { get; set; }

        #region Status

        public string Status { get; set; }

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
