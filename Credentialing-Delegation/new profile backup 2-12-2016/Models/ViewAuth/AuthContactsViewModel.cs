using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.ViewAuth
{
    public class AuthContactsViewModel
    {
       // public int? AuthorizationContactsID { get; set; }

      //  public int? ContactEntityTypeID { get; set; }

        public string ContactEntity { get; set; }  /*New Entry*/

        public string ContactName { get; set; }

        public string EMailFaxOther { get; set; }

        public string Direction { get; set; }

        public bool IncludeFax { get; set; }

      //  public DateTime? CallDateTime { get; set; }

        public DateTime? CreatedDate { get; set; }

   //     public int? ContactTypeID { get; set; }

        public string ContactType { get; set; }    /*New Entry*/

        public string OutcomeType { get; set; }     /*New Entry*/

   //     public int? OutcomeTypeID { get; set; }

    //    public int? OutcomeID { get; set; }

        public string Description { get; set; }

      //  public int? ReasonID { get; set; }

      //  public string Remarks { get; set; }

        public string CreatedBy { get; set; }

     //   public int? AuthorizationID { get; set; }

    //    public string MemberID { get; set; }

    //    public string ModuleName { get; set; }

      //  public int? ContactEntityID { get; set; }
    }
}