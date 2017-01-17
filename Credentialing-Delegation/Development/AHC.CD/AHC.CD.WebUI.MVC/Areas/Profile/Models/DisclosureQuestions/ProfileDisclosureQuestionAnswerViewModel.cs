using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.DisclosureQuestions
{
    public class ProfileDisclosureQuestionAnswerViewModel
    {
        public int ProfileDisclosureQuestionAnswerID { get; set; }

        [Display(Name="Answer")]
        public YesNoOption? AnswerYesNoOption { get; set; }

        [RequiredIf("AnswerYesNoOption", (int)YesNoOption.YES, ErrorMessage = "Reason is required")]
        [Display(Name="Reason *")]
        public string Reason { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Display(Name = "Document Preview")]
        public string DisclosureQuestionCerificatePath { get; set; }
        //[RequiredIfEmpty("CDSCCerificatePath",ErrorMessage = "Upload a supporting Document")]

        [Display(Name = "Document Preview")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        //[PostedFileExtension(AllowedFileExtensions = "pdf,doc,jpg,docx,jpeg,png,bmp", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp, .doc, .docx")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Document should be less than 10mb in size.")]
        public HttpPostedFileBase DisclosureQuestionFile { get; set; }
    }
}
