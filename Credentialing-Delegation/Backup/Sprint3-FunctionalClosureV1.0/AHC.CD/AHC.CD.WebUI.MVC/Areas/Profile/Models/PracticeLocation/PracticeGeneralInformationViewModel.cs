using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PracticeGeneralInformationViewModel
    {
        public int PracticeGeneralInformationID { get; set; }

        #region IsPrimary

        [Required]
        public string IsPrimary { get; private set; }

        //[NotMapped]
        //public YesNoOption PrimaryYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsPrimary);
        //    }
        //    set
        //    {
        //        this.IsPrimary = value.ToString();
        //    }
        //}

        #endregion

        #region CurrentlyPracticingAtThisAddress

        [Required]
        [Display(Name = "Currently Practicing At This Address? *")]
        public string CurrentlyPracticingAtThisAddress { get; private set; }

        //[NotMapped]
        //public YesNoOption CurrentlyPracticingYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CurrentlyPracticingAtThisAddress);
        //    }
        //    set
        //    {
        //        this.CurrentlyPracticingAtThisAddress = value.ToString();
        //    }
        //}

        #endregion

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "What Is Your Expected Start Date?")]
        public DateTime ExpectedStartDate { get; set; }

        [Display(Name = "Corporate or Practice Name")]
        public string CorporateOrPracticeName { get; set; }

        [Required]
        [Display(Name = "Telephone *")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Office e-mail address")]
        public string OffcialEmailID { get; set; }

        #region IsGeneralCorrespondence

        [Required]
        [Display(Name = "Send general correspondence here? *")]
        public string IsGeneralCorrespondence { get; private set; }

        //[NotMapped]
        //public YesNoOption GeneralCorrespondenceYesNoOption
        //{
        //    get
        //    {
        //        return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsGeneralCorrespondence);
        //    }
        //    set
        //    {
        //        this.IsGeneralCorrespondence = value.ToString();
        //    }
        //}

        #endregion

        public PracticeAddressViewModel PracticeAddress { get; set; }

        [Display(Name = "Individual Tax ID")]
        public string IndividualTaxID { get; set; }

        [Display(Name = "Group Tax ID")]
        public string GroupTaxID { get; set; }

        #region PrimaryTaxId

        [Display(Name = "Primary Tax ID(one only) *")]
        public string PrimaryTaxId { get; private set; }

        //[NotMapped]
        //public PrimaryTaxId PrimaryTax
        //{
        //    get
        //    {
        //        return (PrimaryTaxId)Enum.Parse(typeof(PrimaryTaxId), this.PrimaryTaxId);
        //    }
        //    set
        //    {
        //        this.PrimaryTaxId = value.ToString();
        //    }
        //}

        #endregion
    }
}