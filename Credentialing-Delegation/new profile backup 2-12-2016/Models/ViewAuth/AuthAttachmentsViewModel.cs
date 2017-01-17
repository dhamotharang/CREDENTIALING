using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.ViewAuth
{
    public class AuthAttachmentsViewModel
    {
        public int DocumentID { get; set; }

        public string Name { get; set; }

       // public string Path { get; set; }

       // public string URL { get; set; }

      //  public string MedName { get; set; }

        public bool IncludeFax { get; set; }

    //    public DateTime? LastModifiedDate { get; set; }

        public DateTime? CreatedDate { get; set; }


        public string DocumentType { get; set; }

        //----------------- Need to update View Model and backend DB Model -----------------------
        //[Display(Name = "Document")]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        public HttpPostedFileBase DocumentFile { get; set; }

     //   public int? AuthorizationID { get; set; }

    //    public string MemberID { get; set; }

        public string CreatedBy { get; set; }

      //  public string ModuleName { get; set; }
    }
}