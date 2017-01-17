using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeGeneralInformation
    {
        public PracticeGeneralInformation()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PracticeGeneralInformationID { get; set; }

        #region IsPrimary

        [Required]
        public string IsPrimary { get; private set; }

        [NotMapped]
        public YesNoOption? PrimaryYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsPrimary))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsPrimary);
            }
            set
            {
                this.IsPrimary = value.ToString();
            }
        }

        #endregion

        #region CurrentlyPracticingAtThisAddress

        [Required]
        public string CurrentlyPracticingAtThisAddress { get; private set; }

        [NotMapped]
        public YesNoOption? CurrentlyPracticingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.CurrentlyPracticingAtThisAddress))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CurrentlyPracticingAtThisAddress);
            }
            set
            {
                this.CurrentlyPracticingAtThisAddress = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpectedStartDate { get; set; }

        public string CorporateOrPracticeName { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string Fax { get; set; }

        public string OffcialEmailID { get; set; }

        #region IsGeneralCorrespondence

        [Required]
        public string IsGeneralCorrespondence { get; private set; }

        [NotMapped]
        public YesNoOption? GeneralCorrespondenceYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsGeneralCorrespondence))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsGeneralCorrespondence);
            }
            set
            {
                this.IsGeneralCorrespondence = value.ToString();
            }
        }

        #endregion

        public PracticeAddress PracticeAddress { get; set; }

        public string IndividualTaxID { get; set; }

        public string GroupTaxID { get; set; }

        #region PrimaryTaxId

        public string PrimaryTaxId { get; private set; }

        [NotMapped]
        public PrimaryTaxId? PrimaryTax
        {
            get
            {
                if (String.IsNullOrEmpty(this.PrimaryTaxId))
                    return null;

                return (PrimaryTaxId)Enum.Parse(typeof(PrimaryTaxId), this.PrimaryTaxId);
            }
            set
            {
                this.PrimaryTaxId = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
