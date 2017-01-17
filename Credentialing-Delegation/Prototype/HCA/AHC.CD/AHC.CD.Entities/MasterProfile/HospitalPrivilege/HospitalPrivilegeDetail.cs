using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.HospitalPrivilege
{
    public class HospitalPrivilegeDetail
    {
        public HospitalPrivilegeDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int HospitalPrivilegeDetailID { get; set; }

        public Hospital Hospital { get; set; }

        #region Preference

        [Required]
        public string Preference { get; private set; }

        [NotMapped]
        public virtual PreferenceType PreferenceType
        {
            get
            {
                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.Preference);
            }
            set
            {
                this.Preference = value.ToString();
            }
        }   

        #endregion

        #region StaffCategory

        [Required]
        public int StaffCategoryID { get; set; }
        [ForeignKey("StaffCategoryID")]
        public StaffCategory StaffCategory { get; set; }

        #endregion
        
        //public string StaffCategoryExplanation { get; set; }

        [Required]
        public string DepartmentName { get; set; }
        
        [Required]
        public string DepartmentChief { get; set; }

        #region AdmittingPrivilege

        [Required]
        public int AdmittingPrivilegeID { get; set; }
        [ForeignKey("AdmittingPrivilegeID")]
        public AdmittingPrivilege AdmittingPrivilege { get; set; }

        #endregion

        public double AnnualAdmisionPercentage { get; set; }

        #region ArePrevilegesTemporary

        public string ArePrevilegesTemporary { get; private set; }

        [NotMapped]
        public YesNoOption PrevilegesTemporaryYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ArePrevilegesTemporary);
            }
            set
            {
                this.ArePrevilegesTemporary = value.ToString();
            }
        }

        #endregion

        #region FullUnrestrictedPrevilages

        public string FullUnrestrictedPrevilages { get; private set; }

        [NotMapped]
        public YesNoOption FullUnrestrictedPrevilagesYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.FullUnrestrictedPrevilages);
            }
            set
            {
                this.FullUnrestrictedPrevilages = value.ToString();
            }
        }

        #endregion

        #region Speciality

        [Required]
        public int SpecialityID { get; set; }
        [ForeignKey("SpecialityID")]
        public Speciality Speciality { get; set; }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime AffilicationStartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime AffiliationEndDate { get; set; }

        //public string TerminatedAffiliationExplanation { get; set; }

        public string HospitalPrevilegeLetterPath { get; set; }

        #region HospitalContactInfo

        [Required]
        public int HospitalContactInfoID { get; set; }
        [ForeignKey("HospitalContactInfoID")]
        public HospitalContactInfo HospitalContactInfo { get; set; }

        #endregion

        #region HospitalContactPerson

        [Required]
        public int HospitalContactPersonID { get; set; }
        [ForeignKey("HospitalContactPersonID")]
        public HospitalContactPerson HospitalContactPerson { get; set; }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
