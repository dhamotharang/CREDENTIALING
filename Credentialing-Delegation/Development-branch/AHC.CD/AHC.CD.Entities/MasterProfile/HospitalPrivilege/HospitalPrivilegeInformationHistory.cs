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
    public class HospitalPrivilegeInformationHistory
    {
        public HospitalPrivilegeInformationHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int HospitalPrivilegeInformationHistoryID { get; set; }
        
        #region HasHospitalPrivilege

        //[Required]
        public string HasHospitalPrivilege { get; set; }

        [NotMapped]
        public YesNoOption? HospitalPrivilegeYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.HasHospitalPrivilege))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.HasHospitalPrivilege);
            }
            set
            {
                this.HasHospitalPrivilege = value.ToString();
            }
        }

        #endregion

        public string OtherAdmittingArrangements { get; set; }

        public virtual ICollection<HospitalPrivilegeDetail> HospitalPrivilegeDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
