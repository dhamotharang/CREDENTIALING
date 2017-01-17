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

        public string HasHospitalPrivilege { get; private set; }

        [NotMapped]
        public YesNoOption HospitalPrivilegeYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.HasHospitalPrivilege);
            }
            set
            {
                this.HasHospitalPrivilege = value.ToString();
            }
        }

        #endregion

        public HospitalPrivilegeDetail HospitalPrivilegeDetail { get; set; }

        public string OtherAdmittingArrangements { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
