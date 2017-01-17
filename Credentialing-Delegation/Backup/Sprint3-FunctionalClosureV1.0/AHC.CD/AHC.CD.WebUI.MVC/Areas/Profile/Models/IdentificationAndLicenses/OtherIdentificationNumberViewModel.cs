using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class OtherIdentificationNumberViewModel
    {
        public int OtherIdentificationNumberID { get; set; }

        [Required(ErrorMessage = "Please enter the NPI Number.")]
        [Display(Name = "NPI Number *")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter valid NPI Number.Only 10 numbers accepted.")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "NPI number should be of 10 digits.")]
        public string NPINumber { get; set; }

        [Display(Name = "CAQH Number")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Please enter valid CAQH Number.Only numbers accepted.")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "CAQH number should be 8 digits.")]
        public string CAQHNumber { get; set; }
     
        #region NPI UserName & Password

        [RegularExpression(@"[a-zA-Z0-9!#$%^&_-]{8,40}$", ErrorMessage = "NPI Username should be between 8 and 40 characters.")]
        [Display(Name = "NPI User Name")]
        public string NPIUserName { get; set; }
        
        [RegularExpression(@"[a-zA-Z0-9]{8,40}$", ErrorMessage = "NPI Password should be between 8 and 40 characters.")]
        [Display(Name = "NPI Password")]
        public string NPIPassword { get; set; }

        #endregion

        #region CAQH UserName & Password
        
        [RegularExpression(@"[a-zA-Z0-9!#$%^&_-]{8,15}$", ErrorMessage = "CAQH Username should be between 8 and 15 characters.")]
        [Display(Name = "CAQH User Name")]
        public string CAQHUserName { get; set; }
       
        [RegularExpression(@"[a-zA-Z0-9!#$%^&_-]{8,15}$", ErrorMessage = "CAQH Password should be between 8 and 40 characters.")]
        [Display(Name = "CAQH Password")]
        public string CAQHPassword { get; set; }

        #endregion


        [Display(Name = "UPIN Number")]
        [StringLength(6,MinimumLength = 6, ErrorMessage = "Please enter valid UPIN Number.Only numbers and alphabets accepted.")]
        [RegularExpression(@"[a-zA-Z]{1}[0-9]{5}$", ErrorMessage = "UPIN number should be 1 alphabet followed by 5 digits")]
        public string UPINNumber { get; set; }

        [Display(Name = "USMLE Number")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Please enter valid USMLE Number.Only 8 numbers accepted.")]
        [RegularExpression(@"[0-9]*$", ErrorMessage = "USMLE number should be of 8 digits")]
        public string USMLENumber { get; set; }

    }
}
