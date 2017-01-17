using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class PlanBenificiaryPackageViewModel
    {
        public int PlanBenefitPackageId { get; set; }

        public string PbpCode { get; set; }

        public string PBPName { get; set; }

        public string GroupID { get; set; }

        public string EntityID { get; set; }

        public string EntityName { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public string LastModifiedDate { get; set; }
    }
}