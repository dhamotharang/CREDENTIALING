using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class ProfilePictureViewModel
    {
        
        public string ProfilePicturePath { get; set; }

        [Required(ErrorMessage="Please select Profile pictures.")]
        [PostedFileExtension(AllowedFileExtensions = "jpeg,jpg,png,bmp", ErrorMessage = "Profile Picture should be .jpeg, .jpg, .png or .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Profile Picture should be less than 10 MB of size.")]
        public HttpPostedFileBase ProfilePictureFile { get; set; }

    }
}