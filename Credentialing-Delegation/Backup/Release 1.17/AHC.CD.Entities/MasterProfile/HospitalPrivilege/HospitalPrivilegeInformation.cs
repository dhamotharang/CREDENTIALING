using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.HospitalPrivilege
{
    public class HospitalPrivilegeInformation
    {
        public HospitalPrivilegeInformation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int HospitalPrivilegeInformationID { get; set; }

        #region HasHospitalPrivilege

        [Required]
        public string HasHospitalPrivilege { get; set; }

        [NotMapped]
        public YesNoOption? HospitalPrivilegeYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.HasHospitalPrivilege))
                    return null;

                if (this.HasHospitalPrivilege.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.HasHospitalPrivilege);
            }
            set
            {
                this.HasHospitalPrivilege = value.ToString();
            }
        }

        #endregion

        public virtual ICollection<HospitalPrivilegeDetail> HospitalPrivilegeDetails { get; set; }

        public string OtherAdmittingArrangements { get; set; }

        public ICollection<HospitalPrivilegeInformationHistory> HospitalPrivilegeInformationHistory { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
