using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.ViewAuth
{
    public class AuthNotesViewModel
    {
        public int NoteID { get; set; }
        
        public DateTime? Date { get; set; }

        public string NoteType { get; set; }

        public string UserName { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public bool IncludeFax { get; set; }

        /*---------------------------------------*/
      //  public string NoteStatus { get; set; }

      //  public string RationaleDescription { get; set; }

      //  public string AlternatePlanDescription { get; set; }

     //   public string CriteriaUsedDescription { get; set; }

     //   public string ServiceSubjectToNotice { get; set; }

        /*  need to create list of medicalnecessary*/
      //  public string MedicalNecessaries { get; set; }

     //   public bool IsPrivate { get; set; }

     //   public int? AuthorizationID { get; set; }

    //    public string MemberID { get; set; }

   //     public string ModuleName { get; set; }

    }
}