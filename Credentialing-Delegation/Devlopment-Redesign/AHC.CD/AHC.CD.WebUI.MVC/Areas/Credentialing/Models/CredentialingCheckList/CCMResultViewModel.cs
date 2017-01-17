
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList
{
    public class CCMResultViewModel
    {
        public int CredentialingAppointmentResultID { get; set; }

        public string SignaturePath { get; set; }

        public DateTime? SignedDate { get; set; }

        public int? SignedByID { get; set; }
       

        #region Level

        public string Level { get; set; }

        //[NotMapped]
        //public CredentialingLevel? CredentialingLevel
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(this.Level))
        //            return null;

        //        if (this.Level.Equals("Not Available"))
        //            return null;

        //        return (CredentialingLevel)Enum.Parse(typeof(CredentialingLevel), this.Level);
        //    }
        //    set
        //    {
        //        this.Level = value.ToString();
        //    }
        //}

        #endregion
    }
}