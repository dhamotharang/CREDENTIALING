using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class MemberInformationPreviewViewModel
    {
        [Display(Name = "REF#: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "MBR#: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Display(Name = "Mbr Name: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MemberName
        {
            get
            {
                return FirstName + " " + LastName;
            }

        }

        [Display(Name = "DOB: ")]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "G: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "Age: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? Age { get; set; }

        public MemberPCPPreviewViewModel PCP { get; set; }

        public MembershipPreviewViewModel MemberMembership { get; set; }

        public MemberContactPreviewViewModel MemberContact { get; set; }

    }
}