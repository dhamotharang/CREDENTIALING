using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.ViewAuth
{
    public class AuthStatusViewModel
    {
        public int AuthorizationCurrentStatusID { get; set; }

        public DateTime Date { get; set; }  /*New Entry*/

        public int ExtensionNumber { get; set; } /*New Entry*/

        public string UserName { get; set; }
      //  public string ReferredToID { get; set; }

        //--------------- Authorization Queue Master Table ID -----------------
       // public string CurrentQueueID { get; set; }

       // public string ReferredFromID { get; set; }

       // public string Reason { get; set; }

      //  public string InitialStatus { get; set; }

     //   public string ReasonForUpdate { get; set; }

        #region Status

        public string Status { get; set; }

        //public StatusType? StatusType
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(this.Status))
        //            return null;

        //        return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
        //    }
        //    set
        //    {
        //        this.Status = value.ToString();
        //    }
        //}

        #endregion
    }
}