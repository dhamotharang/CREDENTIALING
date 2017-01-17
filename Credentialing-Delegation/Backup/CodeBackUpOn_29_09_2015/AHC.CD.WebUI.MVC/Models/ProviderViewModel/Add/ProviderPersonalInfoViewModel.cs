using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add
{
    public class ProviderPersonalInfoViewModel
    {
        public string LastName
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string MiddleName
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public char Gender
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

       public ProviderProfileContactViewModel Contact { get; set; }

    }
}