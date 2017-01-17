using AutoMapper;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class AuthorizationPreviewController : Controller
    {
        public ActionResult SetCreateAuthPreview(string CreateEvent, AuthorizationViewModel CreateNewAuth)
        {
            AuthPreviewViewModal AuthPreview = new AuthPreviewViewModal();
            try
            {
               

                //Mapper.Initialize(config => { config.CreateMap<AuthorizationProviderViewModel, ProviderPreviewViewModel>(); });
                Mapper.Initialize(config =>
                {
                    config.CreateMap<AuthorizationViewModel, AuthPreviewViewModal>();
                    config.CreateMap<MemberViewModel, MemberInformationPreviewViewModel>();
                    config.CreateMap<MembershipViewModel, MembershipPreviewViewModel>();
                    config.CreateMap<MemberContactViewModel, MemberContactPreviewViewModel>();
                    config.CreateMap<MemberPCPViewModel, MemberPCPPreviewViewModel>();
                    config.CreateMap<AuthorizationProviderViewModel, ProviderPreviewViewModel>();
                    config.CreateMap<ICDViewModel, ICDPreviewViewModel>();
                    config.CreateMap<CPTViewModel, CPTPreviewViewModel>();
                    config.CreateMap<AdmissionViewModel, AdmissionPreviewViewModel>();
                    config.CreateMap<DischargeViewModel, DischargePreviewViewModel>();
                    config.CreateMap<FacilityViewModel, FacilityPreviewViewModel>();
                });

             //   CreateNewAuth.Member = new MemberViewModel();
               
                AuthPreview = AutoMapper.Mapper.Map<AuthorizationViewModel, AuthPreviewViewModal>(CreateNewAuth);

                AuthPreview.Member.ReferenceNumber = AuthPreview.ReferenceNumber;
                AuthPreview = FormatAuthPreview(AuthPreview);

                AuthPreview.Content.AuthPreviewFooter = SetAuthPreviewCreateFooter(CreateEvent);
        

            }
            catch (Exception)
            {
                return null;
            }
           // return Json(AuthPreview, JsonRequestBehavior.AllowGet);
           return PartialView("~/Areas/UM/Views/Common/AuthPreview/_AuthPreviewModal.cshtml", AuthPreview);
        }

        

        private AuthPreviewViewModal FormatAuthPreview(AuthPreviewViewModal AuthPreview)
        {
            AuthPreviewConstraintViewModel Constraint = new AuthPreviewConstraintViewModel();
            AuthPreviewContentViewModel Content = new AuthPreviewContentViewModel();
            try
            {
                switch (AuthPreview.PlaceOfService.ToUpper())
                {
                    // -------------- 11(a) Copies
                    case "81- Independent Lab":
                    case "72- Rural Health Clinic":
                    case "71- S/LPHC":
                    case "60- MIC":
                    case "50- FQHC":
                    case "49- INDEPENDENT CLINIC":
                    case "42- AMBULANCE–AIR OR WATER":
                    case "41- AMBULANCE- LAND":
                    case "23- EMERGENCY ROOM- HOSPITAL":   
                    case "24- ASC":
                    case "11(A)- OFFICE":
                          Content = setPOS11CPTSection(AuthPreview.CPTCodes);
                          break;

                    // -------------- 11(b) Copies
                    case "65- E-SRDTF":
                    case "11(B)- OFFICE":
                        Content = setPOS11CPTSection(AuthPreview.CPTCodes);
                        break;

                    // -------------- Plain Language Copies
                    case "62- CORF":
                    case "12- PATIENT HOME":
                        Constraint.IsAgencyOrSupplier = true;
                        Content = SetPlainLanguageCPTSection(AuthPreview.CPTCodes);
                        break;
                   
                    case "26- MILITARY TREATMENT FACILITY":
                    case "21- IP HOSPITAL":
                        Constraint.IsAdmissionAuthorization = true;
                        Constraint.HasDRGCode = true;
                        Constraint.HasMDCCode = true;
                        Content = setAdmissionCPTSection(AuthPreview.CPTCodes);
                        break;
                    
                    // -------------- POS 22 Copies
                    case "53- CMHC":
                    case "52- PFPH":
                    case "25- BIRTHING CENTER":
                    case "22- OP HOSPITAL":
                        Constraint.IsOutPatient = true;
                        if (AuthPreview.OutPatientType.ToUpper() == "OP PROCEDURE" || AuthPreview.OutPatientType.ToUpper() == "OP DIAGNOSTIC")
                        {
                            Content = setPOS22Section(AuthPreview.CPTCodes);
                        }
                        else
                        {
                            Constraint.IsAdmissionAuthorization = true;
                            Constraint.HasDRGCode = true;
                            Constraint.HasMDCCode = true;
                            Content = setAdmissionCPTSection(AuthPreview.CPTCodes);
                        }

                        break;
                    // -------------- POS 31 Copies
                    case "61(b)- LTAC":
                    case "61(a)- IRF":
                    case "56- PRTF":
                    case "55- RS/AT":
                    case "54- ICF/MR":
                    case "51- IPF":
                    case "34- HOSPICE":
                    case "33- CUSTODIAL CARE FACILITY":
                    case "32- NURSING FACILITY":
                    case "31- SNF":
                        Constraint.HasRUGCode = true;
                        Constraint.HasMDCCode = true;
                        Constraint.IsAdmissionAuthorization = true;
                        Content = setAdmissionCPTSection(AuthPreview.CPTCodes);
                        break;
                    
                }
            }
            catch(Exception){
                return null;
            }

            AuthPreview.Constraint = Constraint;
            AuthPreview.Content = Content;

            return AuthPreview;
        }

        private AuthPreviewContentViewModel SetPlainLanguageCPTSection(List<CPTPreviewViewModel> CPTCodes)
        {
            AuthPreviewContentViewModel Content = new AuthPreviewContentViewModel();
            if (CPTCodes.Count <= 20)
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision1.cshtml";

            }
            else if (CPTCodes.Count > 20)
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision2.cshtml";

            }

            Content.CPTPreviewHeader = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodePlainLangHeaderPreview.cshtml";
            Content.CPTPreviewBody = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodePlainLangPreview.cshtml";
            return Content;
        }

        private AuthPreviewContentViewModel setPOS11CPTSection(List<CPTPreviewViewModel> CPTCodes)
        {
            AuthPreviewContentViewModel Content = new AuthPreviewContentViewModel();
            if (CPTCodes.Count <= 20)
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision1.cshtml";

            }
            else if (CPTCodes.Count <= 40)
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision2.cshtml";

            }
            else
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision3.cshtml";

            }

            Content.CPTPreviewHeader = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeNonAdmissionHeaderPreview.cshtml";
            Content.CPTPreviewBody = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeNonAdmissionPreview.cshtml";
            return Content;
        }

        private AuthPreviewContentViewModel setAdmissionCPTSection(List<CPTPreviewViewModel> CPTCodes)
        {
            AuthPreviewContentViewModel Content = new AuthPreviewContentViewModel();
            if (CPTCodes.Count <= 4)
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision2.cshtml";
            }
            else
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision3.cshtml";
            }
            Content.CPTPreviewHeader = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeAdmissionHeaderPreview.cshtml";
            Content.CPTPreviewBody = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeAdmissionPreview.cshtml";
            return Content;
        }

        private AuthPreviewContentViewModel setPOS22Section(List<CPTPreviewViewModel> CPTCodes)
        {
            AuthPreviewContentViewModel Content = new AuthPreviewContentViewModel();
            if (CPTCodes.Count <= 10)
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision1.cshtml";
            }
            else if (CPTCodes.Count < 20)
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision2.cshtml";
            }
            else
            {
                Content.CPTPreviewView = "~/Areas/UM/Views/Common/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeDivision3.cshtml";
            }
            Content.CPTPreviewHeader = "~/Areas/UM/Views/Common/AuthPreview/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeNonAdmissionHeaderPreview.cshtml";
            Content.CPTPreviewBody = "~/Areas/UM/Views/Common/AuthPreview/AuthPreview/AuthPreviewSections/CPTCodes/_CPTCodeNonAdmissionPreview.cshtml";
            return Content;

        }

        private string SetAuthPreviewCreateFooter(string Action)
        {
            string authPreviewFooter="";
            switch (Action.ToUpper())
            {
                case "APPROVE":
                    authPreviewFooter = "~/Areas/UM/Views/Common/AuthPreview/Footer/_AuthPreviewCreate_ApproveFooter.cshtml";
                    break;
                case "REFER":
                    authPreviewFooter = "~/Areas/UM/Views/Common/AuthPreview/Footer/_AuthPreviewCreate_ReferFooter.cshtml";
                    break;
            }
            return authPreviewFooter;
             
        }
	}
}