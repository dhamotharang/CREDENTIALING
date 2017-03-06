using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.Demographics;
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

        #region PracticeExclusively

        public string PracticeExclusively { get; private set; }

        [NotMapped]
        public YesNoOption? PracticeExclusivelyYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticeExclusively))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.PracticeExclusively);
            }
            set
            {
                this.PracticeExclusively = value.ToString();
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
        public DateTime PracticeStartDate { get; set; }

        #region Provider Title

        public virtual ICollection<ProviderTitle> ProviderTypes { get; set; }

        #endregion

        [Required]
        public string CorporateOrPracticeName { get; set; }

        
        public string Telephone { get; set; }

        public string CountryCodeTelephone { get; set; }

        public string Fax { get; set; }

        public string CountryCodeFax { get; set; }

        public string OffcialEmailID { get; set; }

        #region SendGeneralCorrespondence

        public string SendGeneralCorrespondence { get; private set; }

        [NotMapped]
        public YesNoOption? GeneralCorrespondenceYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.SendGeneralCorrespondence))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.SendGeneralCorrespondence);
            }
            set
            {
                this.SendGeneralCorrespondence = value.ToString();
            }
        }

        #endregion

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



        #region address

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        public string County { get; set; }

        [Required]
        public string Country { get; set; }
        
        public string Building { get; set; }

        [Required]
        public string Street { get; set; }

        public string Zipcode { get; set; }

    #endregion 

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
