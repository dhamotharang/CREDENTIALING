using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Letter
{
    public class LetterViewModel
    {
        public int? AuthorizationID { get; set; }

        public int? LetterID { get; set; }

        [Display(Name = "BATCHNUMBER", ResourceType = typeof(App_LocalResources.Content))]
        public string BatchNumber { get; set; }

        [Display(Name = "GENERATEDDATE", ResourceType = typeof(App_LocalResources.Content))]
        public DateTime MailDate { get; set; }

        [Display(Name = "USERNAME", ResourceType = typeof(App_LocalResources.Content))]
        public string SentBy { get; set; }

        [Display(Name = "LETTERREASON", ResourceType = typeof(App_LocalResources.Content))]
        public string Reason { get; set; }

        public string Path { get; set; }

        public int LetterTemplateID { get; set; }

        public string LetterTemplate { get; set; }

        [Display(Name = "Entity", ResourceType = typeof(App_LocalResources.Content))]
        public string LetterEntity { get; set; }

        public int? LetterQuestionID { get; set; }

       // public virtual LetterQuestion LetterQuestion { get; set; }

        public int? LetterAnswerID { get; set; }

       // public virtual LetterAnswer LetterAnswer { get; set; }

        public int? NonCoverageDetailID { get; set; }

        public NonCoverageDetailViewModel NonCoverageDetail { get; set; }
        
        public DateTime? LastModifiedDate { get; set; }

        public HttpPostedFileBase LetterFile { get; set; }

        public string Status { get; set; }
    }
}