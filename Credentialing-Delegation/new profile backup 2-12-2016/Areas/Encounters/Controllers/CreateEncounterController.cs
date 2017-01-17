using AutoMapper;
using PortalTemplate.Areas.Encounters.Models.CreateEncounter;
using PortalTemplate.Areas.Encounters.Services;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Encounters.Controllers
{
    public class CreateEncounterController : Controller
    {
        ICreateEncounter _CreateEncounterService;

        public CreateEncounterController()
        {

            _CreateEncounterService = new CreateEncounter();

        }

        // GetProviderSelection() method return the partial view of selected provider which will intern call the GetProviderResult()
        public ActionResult GetProviderSelection()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_SelectProvider.cshtml");
        }

        public ActionResult GetProviderResult(string ProviderSearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_ProviderResult.cshtml", _CreateEncounterService.GetProviderResultList(ProviderSearchParameter));
        }

        // GetSelectedProviderResult() will return the partial view which will contain the member information of corresponding Rendering provider
        public ActionResult GetSelectedProviderResult(string ProviderId)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_ProviderSelectMember.cshtml", _CreateEncounterService.GerProviderSelectMembers(ProviderId));
        }

        // GetEncounterDetails() will return the encounter Details of particular encounter
        public ActionResult GetEncounterDetails()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_EncounterTemplate.cshtml", _CreateEncounterService.GetEncounterDetails());
        }

        // GetMember() method return the partial view of selected member which will intern call the GetMemberResult()
        public ActionResult GetMember()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_MemberSelectMember.cshtml", _CreateEncounterService.GetMemberResultList());
        }


        public ActionResult GetMemberForScheduler()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_MemberSelectMemberForScheduler.cshtml", _CreateEncounterService.GetMemberResultList());
        }



        public ActionResult GetMemberResult(string MemberSearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_MemberResult.cshtml", _CreateEncounterService.GetMemberResultList());
        }

        // GetEncounterDetailsForMember() will return the template of member encounter
        public ActionResult GetEncounterDetailsForMember()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_EncounterTemplateForMember.cshtml", _CreateEncounterService.GetEncounterDetailsForMember());
        }

        // returns the coding details of the encounter
        public ActionResult GetCodingDetails(PrimaryEncounterViewModel EncounterDetails)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_CodingDetails.cshtml", _CreateEncounterService.GetCodingDetails(EncounterDetails));
        }

        // returns the Auditing details of the encounter
        [HttpPost]
        public ActionResult GetAuditingDetails(EncounterCodingDetailsViewModel CodeDetails)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_AuditingDetails.cshtml", _CreateEncounterService.GetAuditingDetails(CodeDetails));
        }

        // Getting active diagnosis codes
        public ActionResult GetActiveDiagnosisCode()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_ActiveDiagnosisCodeDetails.cshtml", _CreateEncounterService.GetActiveDiagnosisCode());
        }

        // Getting the active procedure codes
        public ActionResult GetActiveProcedureCode()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_ActiveProcedureCodeDetails.cshtml", _CreateEncounterService.GetActiveProcedureCode());
        }

        // Getting the Documents History
        public ActionResult GetDocumentHistory()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_DocumentHistoryDetails.cshtml", _CreateEncounterService.GetDocumentHistory());
        }

        // returns the Referring provider list
        public ActionResult GetReferingProviderList(string SearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_ReferingProviderList.cshtml", _CreateEncounterService.GetReferingProviderList());
        }

        // returns the Billing provider list
        public ActionResult GetBillingProviderList(string SearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_BillingProviderList.cshtml", _CreateEncounterService.GetBillingProviderList());
        }

        // returns the Rendering provider list
        public ActionResult GetRenderingProviderList(string SearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_RenderingProviderList.cshtml", _CreateEncounterService.GetRenderingProviderList());
        }

        // returns the Facility list
        public ActionResult GetFacilityList(string SearchParameter = null)
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_FacilityList.cshtml", _CreateEncounterService.GetFacilityList());
        }

        // returns the Claim History list
        public ActionResult GetClaimsHistory()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_CLaimsHistory.cshtml", _CreateEncounterService.GetClaimsHistory());
        }

        // returns the DP Model
        public ActionResult OpenDiagnosisPointer()
        {
            return PartialView("~/Areas/Encounters/Views/CreateEncounter/_DiagnosisPointerModal.cshtml");
        }
    }
}