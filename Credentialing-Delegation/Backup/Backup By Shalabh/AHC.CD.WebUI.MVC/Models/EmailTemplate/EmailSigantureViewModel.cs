using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.EmailTemplate
{
    public class EmailSigantureViewModel
    {
        public int EmailSigantureID { get; set; }

        public string Signature { get; set; }

        public ICollection<HttpPostedFileBase> AttachedFiles { get; set; }
    }
}