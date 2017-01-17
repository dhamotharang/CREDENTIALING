using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Initiation
{
    public class SearchProviderForCred
    {
        public string NPINumber { set; get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CAQH { set; get; }

        public string IPAGroupName { get; set; }

        public string Specialty { get; set; }

        public string ProviderType { get; set; }
    }
}