using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpecialty
{
    public class SpecialtyDetail
    {
        public SpecialtyDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int SpecialtyDetailID { get; set; }

        #region SpecialtyPreference

        [Required]
        public string SpecialtyPreference { get; set; }

        [NotMapped]
        public PreferenceType? PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.SpecialtyPreference))
                    return null;

                if (this.SpecialtyPreference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.SpecialtyPreference);
            }
            set
            {
                this.SpecialtyPreference = value.ToString();
            }
        }

        #endregion        

        #region Specialty

        //[Required]
        public int? SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public Specialty Specialty { get; set; }

        #endregion

        #region Currently Practicing

        public string IsCurrentlyPracting { get; set; }

        [NotMapped]
        public YesNoOption? CurrentlyPractingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsCurrentlyPracting))
                    return null;

                if (this.IsCurrentlyPracting.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsCurrentlyPracting);
            }
            set
            {
                this.IsCurrentlyPracting = value.ToString();
            }
        }

        #endregion

        public Double? PercentageOfTime { get; set; }

        #region ListedInHMO

        [Required]
        public string ListedInHMO { get; set; }

        [NotMapped]
        public YesNoOption? ListedInHMOYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ListedInHMO))
                    return null;

                if (this.ListedInHMO.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ListedInHMO);
            }
            set
            {
                this.ListedInHMO = value.ToString();
            }
        }

        #endregion

        #region ListedInPPO

        [Required]
        public string ListedInPPO { get; set; }

        [NotMapped]
        public YesNoOption? ListedInPPOYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ListedInPPO))
                    return null;

                if (this.ListedInPPO.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ListedInPPO);
            }
            set
            {
                this.ListedInPPO = value.ToString();
            }
        }
        
        #endregion

        #region ListedInPOS

        [Required]
        public string ListedInPOS { get; set; }

        [NotMapped]
        public YesNoOption? ListedInPOSYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ListedInPOS))
                    return null;

                if (this.ListedInPOS.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ListedInPOS);
            }
            set
            {
                this.ListedInPOS = value.ToString();
            }
        }

        #endregion        

        #region SpecialtyBoardDetail

        #region BoardCertified Enum Mapping

        [Required]
        public string IsBoardCertified { get; set; }

        [NotMapped]
        public YesNoOption? BoardCertifiedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsBoardCertified))
                    return null;

                if (this.IsBoardCertified.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsBoardCertified);
            }
            set
            {
                this.IsBoardCertified = value.ToString();
            }
        }

        #endregion

        #region SpecialtyBoard

        [Required]
        public int SpecialtyBoardID { get; set; }
        [ForeignKey("SpecialtyBoardID")]
        public SpecialtyBoard SpecialtyBoard { get; set; }

        #endregion  

        public virtual SpecialtyBoardCertifiedDetail SpecialtyBoardCertifiedDetail { get; set; }

        public virtual SpecialtyBoardNotCertifiedDetail SpecialtyBoardNotCertifiedDetail { get; set; }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
