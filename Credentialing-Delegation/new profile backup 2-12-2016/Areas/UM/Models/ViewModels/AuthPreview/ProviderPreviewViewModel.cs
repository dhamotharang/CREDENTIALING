using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class ProviderPreviewViewModel
    {

        [Display(Name = "First Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "Svc Provider Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Name { get; set; }

        [Display(Name = "Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderName
        {
            get
            {
                string name = "Dr. " + FirstName + " " + LastName;
                return name;
            }

        }

        [DisplayFormat(NullDisplayText = "-")]
        public string NPI { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string TaxID { get; set; }

        [Display(Name = "TAXID/NPI: ")]
        public string TaxIDOrNPI
        {
            get
            {
                string TaxIDMerge = TaxID;
                string NPIMerge = NPI;
                if (TaxIDMerge == null)
                {
                    TaxIDMerge = "-";
                }
                if (NPIMerge == null)
                {
                    NPIMerge = "-";
                }
                return TaxIDMerge + " / " + NPIMerge;
            }

        }

        [Display(Name = "Ph: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FaxNumber { get; set; }

        [Display(Name = "Speciality: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Specialty { get; set; }

        [Display(Name = "Type: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderType { get; set; }

        [Display(Name = "Contact Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactName { get; set; }

        public string ProviderNetwork {
            get {
                string Network = null;
                if (NPI != null)
                {
                    Network = "IN";
                }

                return Network;
            }
        
         }

        public bool IsProviderNetwork { get; set; }


    }
}