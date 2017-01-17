using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class OtherLegalNameViewModel
    {
        public OtherLegalNameViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int OtherLegalNameID { get; set; }

        [Display(Name = "Other First Name *")]
        [Required(ErrorMessage = "Please enter First Name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid First Name. Only characters, spaces, comma and hyphen accepted.")]
        public string OtherFirstName { get; set; }

        [Display(Name = "Other Middle Name")]
        [StringLength(50, ErrorMessage = "Middle Name must be less than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid Middle Name. Only characters, spaces, comma and hyphen accepted.")]
        public string OtherMiddleName { get; set; }

        [Display(Name = "Other Last Name *")]
        [Required(ErrorMessage = "Please enter Last Name.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last Name must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid Last Name.(a-z, A-Z, space, comma, hyphen)")]
        public string OtherLastName { get; set; }

        [Display(Name = "Suffix (JR,III)")]
        [StringLength(10, ErrorMessage = "Suffix must be less than {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-]*$", ErrorMessage = "Please enter valid Suffix. Only characters, spaces, comma and hyphen accepted.")]
        public string Suffix { get; set; }

        [Display(Name = "When did you start using other name")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Start Date should not be greater than current date.")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "When did you stop using other name")]
        [DateEnd(DateStartProperty = "StartDate", MaxYear = "0", ErrorMessage = "Stop Date should be greater than Start Date and less than current date.")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string DocumentPath { get; set; }

        [Display(Name = "Supporting Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bitmap")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Other legal document should be less than 10mb in size.")]
        public HttpPostedFileBase File { get; set; }

        #region Status

        [Display(Name = "Status")]
        public string Status
        {
            get
            {
                return this.StatusType.ToString();
            }
            private set
            {
                this.StatusType = (StatusType)Enum.Parse(typeof(StatusType), value);
            }
        }

        public StatusType StatusType { get; set; }

        #endregion

        public DateTime LastModifiedDate { get; set; }
    }
}
