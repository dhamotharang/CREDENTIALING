using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class FacilityPreviewViewModel
    {

        public bool FacilityNetwork { get; set; }

        public string FacilityNetworkName
        {
            get
            {
                string Network = null;
                if (FacilityNPI != null)
                {
                    Network = "IN";
                }

                return Network;
            }
        }


        [Display(Name = "Facility Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FullName { get; set; }

      //   [Display(Name = "Name: ")]
         [DisplayFormat(NullDisplayText = "-")]
        public string Name { get; set; }

        [Display(Name = "Contact Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactName { get; set; }

        [Display(Name = "Ph: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FaxNumber { get; set; }

        public string TaxID { get; set; }

        public string FacilityNPI { get; set; }

         [Display(Name = "TAXID/NPI: ")]
        public string TaxIDOrNPI
        {
            get
            {
                string TaxIDMerge = TaxID;
                string NPIMerge = FacilityNPI;
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

        [Display(Name = "Facility Type: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FacilityType { get; set; }

    }
}