using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.History
{
    public class MemberHistoriesViewModel
    {
        [Display(Name = "ABV", ResourceType = typeof(App_LocalResources.Content))]
        public string POSAbbreviation { get; set; }

        [Display(Name = "REF", ResourceType = typeof(App_LocalResources.Content))]
        public string ReferenceNumber { get; set; }

        [Display(Name = "AUTHNO", ResourceType = typeof(App_LocalResources.Content))]
        public string AuthNumber { get; set; }

        [Display(Name = "FROMDATE", ResourceType = typeof(App_LocalResources.Content))]
        public string FromDate { get; set; }

        [Display(Name = "TODATE", ResourceType = typeof(App_LocalResources.Content))]
        public string ToDate { get; set; }

        [Display(Name = "SVCPROVIDER", ResourceType = typeof(App_LocalResources.Content))]
        public string ServiceProviderName { get; set; }

        [Display(Name = "REQUEST", ResourceType = typeof(App_LocalResources.Content))]
        public string RequestType { get; set; }

        [Display(Name = "FACILITY", ResourceType = typeof(App_LocalResources.Content))]
        public string FacilityName { get; set; }

        [Display(Name = "AUTH", ShortName = "AUTH TYPE", ResourceType = typeof(App_LocalResources.Content))]
        public string AuthorizationType { get; set; }

        [Display(Name = "UNITS", ResourceType = typeof(App_LocalResources.Content))]
        public int AuthUnits { get; set; }

        public string AuthStatus { get; set; }

        public string ReferUserName { get; set; }

        [Display(Name = "STATUS", ResourceType = typeof(App_LocalResources.Content))]
        public string StatusUserName { get; set; }

        [Display(Name = "POS", ResourceType = typeof(App_LocalResources.Content))]
        public string Pos { get; set; }

        [Display(Name = "POS", ResourceType = typeof(App_LocalResources.Content))]
        public string AbbreviatedPOS { get; set; }

        [Display(Name = "PRIMARYDX", ShortName = "DX", ResourceType = typeof(App_LocalResources.Content))]
        public string PrimaryDx { get; set; }

        [Display(Name = "DXDESC", ResourceType = typeof(App_LocalResources.Content))]
        public string DXDescription { get; set; }

        public bool IsAuthorizationEditable { get; set; }

        public bool IsOTApplicable { get; set; }

        public bool IsCopyApplicable { get; set; }

        [Display(Name = "CPTLIST", ResourceType = typeof(App_LocalResources.Content))]
        public List<CPTViewModel> CPTList { get; set; }

        [Display(Name = "REQUNITS", ResourceType = typeof(App_LocalResources.Content))]
        public int RequestedUnits { get; set; }

        [Display(Name = "DATEOFSVC", ShortName = "EXP DOS", ResourceType = typeof(App_LocalResources.Content))]
        public DateTime? ExpectedDischargeDate { get; set; }

        [Display(Name = "ACTUALLOS", ResourceType = typeof(App_LocalResources.Content))]
        public int ActualLOS { get; set; }

        [Display(Name = "APPROVEDDAYS", ResourceType = typeof(App_LocalResources.Content))]
        public int ApprovedDays { get; set; }

        [Display(Name = "APPROVEDLOS", ResourceType = typeof(App_LocalResources.Content))]
        public int ApprovedLOS { get; set; }

        [Display(Name = "TOS", ResourceType = typeof(App_LocalResources.Content))]
        public string TypeOfService { get; set; }

        [Display(Name = "OUTPATIENTTYPE", ResourceType = typeof(App_LocalResources.Content))]
        public string OutpatientType { get; set; }

        [Display(Name = "APPDATE", ResourceType = typeof(App_LocalResources.Content))]
        public string AppointmentDate { get; set; }

        [Display(Name = "PROVIDERID", ResourceType = typeof(App_LocalResources.Content))]
        public string ServiceProviderID { get; set; }

        [Display(Name = "MBRID", ResourceType = typeof(App_LocalResources.Content))]
        public string MemberID { get; set; }

        [Display(Name = "MBRNAME", ResourceType = typeof(App_LocalResources.Content))]
        public string MemberName { get; set; }
    }
}