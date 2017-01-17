using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class ContactPersonViewModel
    {
        public int ContactPersonID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string MobileNumber { get; set; }

        public string EmailAddress { get; set; }
    }
}
