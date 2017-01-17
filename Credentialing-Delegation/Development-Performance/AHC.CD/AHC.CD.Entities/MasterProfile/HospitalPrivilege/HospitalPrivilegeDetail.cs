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

        //[Required]
        public int? HospitalID { get; set; }
        [ForeignKey("HospitalID")]
        public Hospital Hospital { get; set; }

        #region Preference

        //[Required]
        public string Preference { get; set; }

        [NotMapped]
        public virtual PreferenceType? PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Preference))
                    return null;

                if (this.Preference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.Preference);
            }
            set
            {
                this.Preference = value.ToString();
            }
        }   

        #endregion

        #region StaffCategory

        //[Required]
        public int? StaffCategoryID { get; set; }
        [ForeignKey("StaffCategoryID")]
        public StaffCategory StaffCategory { get; set; }

        #endregion
        
        //public string StaffCategoryExplanation { get; set; }

        public string DepartmentName { get; set; }
        
        public string DepartmentChief { get; set; }

        #region AdmittingPrivilege

        //[Required] // Do not uncomment
        public int? AdmittingPrivilegeID { get; set; }
        [ForeignKey("AdmittingPrivilegeID")] // Do not uncomment
        public AdmittingPrivilege AdmittingPrivilege { get; set; }

        #endregion

        public double? AnnualAdmisionPercentage { get; set; }

        #region ArePrevilegesTemporary

        public string ArePrevilegesTemporary { get; private set; }

        [NotMapped]
        public YesNoOption? PrevilegesTemporaryYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ArePrevilegesTemporary))
                    return null;

                if (this.ArePrevilegesTemporary.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ArePrevilegesTemporary);
            }
            set
            {
                this.ArePrevilegesTemporary = value.ToString();
            }
        }

        #endregion

        #region FullUnrestrictedPrevilages

        //[Required]
        public string FullUnrestrictedPrevilages { get; set; }

        [NotMapped]
        public YesNoOption? FullUnrestrictedPrevilagesYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.FullUnrestrictedPrevilages))
                    return null;

                if (this.FullUnrestrictedPrevilages.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.FullUnrestrictedPrevilages);
            }
            set
            {
                this.FullUnrestrictedPrevilages = value.ToString();
            }
        }

        #endregion

        #region Specialty

        //[Required] // Do not uncomment
        public int? SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")] // Do not uncomment
        public Specialty Specialty { get; set; }

        #endregion        

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? AffilicationStartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AffiliationEndDate { get; set; }

        //[Required]
        public string HospitalPrevilegeLetterPath { get; set; }

        #region HospitalContactInfo

        //[Required] // Do not uncomment
        public int? HospitalContactInfoID { get; set; }
        [ForeignKey("HospitalContactInfoID")]
        public HospitalContactInfo HospitalContactInfo { get; set; }

        #endregion

        #region HospitalContactPerson

        //[Required] // Do not uncomment
        public int? HospitalContactPersonID { get; set; }
        [ForeignKey("HospitalContactPersonID")]
        public HospitalContactPerson HospitalContactPerson { get; set; }

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

        public virtual ICollection<HospitalPrivilegeDetailHistory> HospitalPrivilegeDetailHistory { get; set; }
    }
}
