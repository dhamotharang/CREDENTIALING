using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.ViewAuth
{
    public class AuthLettersViewModel
    {
        public int LetterID { get; set; }

        public DateTime MailDate { get; set; }

        public string SentBy { get; set; }

        public string Reason { get; set; }

   //     public string Path { get; set; }

    //    public int LetterTemplateID { get; set; }
        public int BatchNumber { get; set; }                    /*New Entry*/

      //  public virtual LetterTemplate LetterTemplate { get; set; }

        public string LetterEntity { get; set; }

     //   public int? LetterQuestionID { get; set; }

     //   public virtual LetterQuestion LetterQuestion { get; set; }

      //  public int? LetterAnswerID { get; set; }

      //  public virtual LetterAnswer LetterAnswer { get; set; }

    //    public int? NonCoverageDetailID { get; set; }

      //  public NonCoverageDetailViewModel NonCoverageDetail { get; set; }

      //  public DateTime? LastModifiedDate { get; set; }

        [Display(Name = "Letter")]
        public HttpPostedFileBase LetterFile { get; set; }
    }
}