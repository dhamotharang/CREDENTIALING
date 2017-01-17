using AutoMapper;
using PortalTemplate.Areas.Billing.Models.CMS1500.New;
using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info;
using PortalTemplate.Areas.Billing.Models.CreateClaim.CreateClaimTemplate;
using PortalTemplate.Areas.Billing.Services;
using PortalTemplate.Areas.Billing.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Controllers
{
    public class CreateClaimController : Controller
    {
        readonly ICreateClaimsService _creatClaimsService;
        public CreateClaimController()
        {
            _creatClaimsService = new CreateClaimsService();
            this.ProvidersList = new List<ProviderResultViewModel>();
        }
        //
        // GET: /CreateClaim/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Action()
        {
            BillingInfo BillingInfo = new BillingInfo();
            return PartialView("~/Areas/Billing/Views/CreateClaim/_ClaimInfo.cshtml", BillingInfo);
        }


        [HttpPost]
        public PartialViewResult GetClaimInfo(CreateClaimsTemplate createClaimsTemplate)
        {

            return PartialView("~/Areas/Billing/Views/CreateClaim/_ClaimInfo.cshtml", _creatClaimsService.MapMemberProviderToBillingInfo(createClaimsTemplate));
        }

        /// <summary>
        /// ***********Claim Creation Type::Create Multiple Claims for Member, Create Single Claim ***********
        /// Gets the List of Members by searching through SubscriberID/MemberName
        /// </summary>
        /// <param name="SubscriberID"></param>
        /// <param name="MemberName"></param>
        /// <returns MemberResultViewModel>Returns the List of Members</returns>
        public ActionResult GetMemberResult(string SubscriberID, string MemberName)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/_MemberResult.cshtml", _creatClaimsService.GetMemberResult(SubscriberID, MemberName));
        }
        //**************Load More Data*****************//
        public ActionResult GetMemberResultByIndex(string SubscriberID, string MemberName)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/GridTableIntegration/_MemberResultTableBody.cshtml", _creatClaimsService.GetMemberResult(SubscriberID, MemberName));
        }

        public ActionResult GetSelectedMemberResult(string MemberId)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/_SelectedMember.cshtml", _creatClaimsService.GetSelectedMemberResult(MemberId));
        }

        List<ProviderResultViewModel> ProvidersList;
        /// <summary>
        /// ************Claim Creation Type::Create Multiple Claims for Providers**********
        /// Gets the List of Providers by Searching through Provider NPI or ProviderName
        /// </summary>
        /// <param name="ProviderName"></param>
        /// <param name="ProviderNPI"></param>
        /// <returns ProviderResultViewModel>ProviderResult</returns>
        public ActionResult GetProviderResult(string ProviderName, string ProviderNPI)
        {
            ProvidersList = _creatClaimsService.GetProviderResult(ProviderName, ProviderNPI);
            TempData["ProviderSelectedResult"] = ProvidersList;
            return PartialView("~/Areas/Billing/Views/CreateClaim/_ProviderResult.cshtml", ProvidersList);
        }
        //************Load More Data*****************//
        public ActionResult GetProviderResultByIndex(string ProviderName, string ProviderNPI)
        {
            ProvidersList = _creatClaimsService.GetProviderResult(ProviderName, ProviderNPI);
            //TempData["ProviderSelectedResul
            return PartialView("~/Areas/Billing/Views/CreateClaim/GridTableIntegration/_ProviderResultTableBody.cshtml", ProvidersList);
        }

        /// <summary>
        /// *********Claim Creation Type::Create Multiple Claims for Providers*********
        /// Gets the List of Members for the selected List of Providers
        /// </summary>
        /// <param name="ProviderId"></param>
        /// <returns MemberResult and ProvidersSelected>It returns the view contains the List of Selected Providers and List of Members for the Selected Providers</returns>
        public ActionResult GetSelectedProviderResult(string ProviderId)
        {
            List<string> ProviderIDs = new List<string>();
            ProviderIDs = ProviderId.Split(',').ToList();
            List<ProviderResultViewModel> PreviousSelectedList = TempData["ProviderSelectedResult"] as List<ProviderResultViewModel>;
            List<ProviderResultViewModel> SelectedProviders = new List<ProviderResultViewModel>();
            SelectedProviders = PreviousSelectedList.Where(x => ProviderIDs.Contains(x.ProviderUniqueId)).ToList();

            ProviderMemberSelectViewModel SelectedProviderMemberResult = new ProviderMemberSelectViewModel();
            SelectedProviderMemberResult.MemberResult = _creatClaimsService.GetSelectedProviderResult(ProviderIDs);
            SelectedProviderMemberResult.ProviderResult = SelectedProviders;

            return PartialView("~/Areas/Billing/Views/CreateClaim/NewCreateClaims/_ProviderSelectMember.cshtml", SelectedProviderMemberResult);
        }


        public ActionResult GetMemberListForProviderResult()
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/_MemberResultForProvider.cshtml", _creatClaimsService.GetMemberListForProviderResult());
        }


        public ActionResult GetSelectedMemberForProviderResult(string MemberId)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/_SelectedMemberForProvider.cshtml", _creatClaimsService.GetSelectedMemberForProviderResult(MemberId));
        }

        [HttpPost]
        public ActionResult GetCms1500Form(BillingInfo BillingInfo)
        {
            try
            {

                ViewBag.VBStateList = new SelectList(_creatClaimsService.GetAllStates(true), "StateId", "Name");

                ActionResult partialView = PartialView("~/Areas/Billing/Views/CreateClaim/_CMS1500Container.cshtml", BillingInfo);

                return partialView;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult GetProviderSelection()
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/NewCreateClaims/_SelectProvider.cshtml");
        }

        /// <summary>
        /// *********Claim Creation Type::Create Multiple Claims for Providers********
        /// Gets the Member Information, Billing Provider Info, Referring Provider Info, Facility Info, Payer Info, Rendering Provider Info for the selected Member
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns>Returns the Create Claim Template</returns>
        public ActionResult GetCreateClaimTemplate(string MemberId)
        {

            return PartialView("~/Areas/Billing/Views/CreateClaim/NewCreateClaims/_CreateClaimsTemplate.cshtml", _creatClaimsService.GetCreateClaimTemplateForMember(MemberId));
        }


        public ActionResult GetMember()
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/NewCreateClaims/_MemberSelectMembrer.cshtml");
        }

        /// <summary>
        /// **********Claim Creation Type::Create Multiple Claims for Member***********
        /// Gets the Member Information, Billing Provider Info, Referring Provider Info, Facility Info, Payer Info, Rendering Provider Info for the selected Member
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns>Returns the Create Claim Template</returns>
        public ActionResult GetCreateClaimTemplateForMember(string MemberId)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/NewCreateClaims/_CreateClaimTemplateForMember.cshtml", _creatClaimsService.GetCreateClaimTemplateForMember(MemberId));
        }


        //------------------------------billing provider---------------------------

        public ActionResult GetBillingProviderResult()
        {
            ProviderResult provider = new ProviderResult();
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_BillingProviderResult.cshtml", _creatClaimsService.GetBillingProviderResult(provider));
        }

        public ActionResult GetBillingProviderResultByIndex(int index, string sortingType, string sortBy, ProviderResult provider)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_TBodyBillingProviderResult.cshtml", _creatClaimsService.GetBillingProviderResult(provider));
        }

        [HttpGet]
        public JsonResult GetSelectedBillingProvider(string ProviderId)
        {
            BillingProviderInfo BillingProviderInfo = _creatClaimsService.GetSelectedBillingProvider(ProviderId);

            return Json(new { success = true, data = BillingProviderInfo },
             JsonRequestBehavior.AllowGet);
        }

        //---------------------------------rendering provider--------------------

        public ActionResult GetRenderingProviderResult()
        {
            ProviderResult provider = new ProviderResult();
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_RenderingProviderResult.cshtml", _creatClaimsService.GetRenderingProviderResult(provider));
        }

        public ActionResult GetRenderingProviderResultByIndex(int index, string sortingType, string sortBy, ProviderResult provider)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_TBodyRenderingProviderResult.cshtml", _creatClaimsService.GetRenderingProviderResult(provider));
        }

        [HttpGet]
        public JsonResult GetSelectedRenderingProvider(string ProviderId)
        {
            ProviderResultViewModel RenderingProviderInfo = _creatClaimsService.GetSelectedRenderingProvider(ProviderId);

            return Json(new { success = true, data = RenderingProviderInfo },
             JsonRequestBehavior.AllowGet);
        }

        //-------------------------------supervising provider-----------------------

        public ActionResult GetSupervisingProviderResult()
        {
            ProviderResult provider = new ProviderResult();
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_SupervisingProviderResult.cshtml", _creatClaimsService.GetSupervisingProviderResult(provider));
        }

        public ActionResult GetSupervisingProviderResultByIndex(int index, string sortingType, string sortBy, ProviderResult provider)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_TBodySupervisingProviderResult.cshtml", _creatClaimsService.GetSupervisingProviderResult(provider));
        }

        [HttpGet]
        public JsonResult GetSelectedSupervisingProvider(string ProviderId)
        {
            SupervisingProviderInfo SupervisingProviderInfo = _creatClaimsService.GetSelectedSupervisingProvider(ProviderId);

            return Json(new { success = true, data = SupervisingProviderInfo },
             JsonRequestBehavior.AllowGet);
        }

        //----------------------------------referring provider--------------------------

        public ActionResult GetReferringProviderResult()
        {
            ProviderResult provider = new ProviderResult();
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_RefferingProviderResult.cshtml", _creatClaimsService.GetReferringProviderResult(provider));
        }

        public ActionResult GetReferringProviderResultByIndex(int index, string sortingType, string sortBy, ProviderResult provider)
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/ProviderResult/_TBodyRefferingProviderResult.cshtml", _creatClaimsService.GetReferringProviderResult(provider));
        }

        [HttpGet]
        public JsonResult GetSelectedReferringProvider(string ProviderId)
        {
            ReferingProviderInfo ReferringProviderInfo = _creatClaimsService.GetSelectedReferringProvider(ProviderId);

            return Json(new { success = true, data = ReferringProviderInfo },
             JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFacilityResult()
        {

            return PartialView("~/Areas/Billing/Views/CreateClaim/_FacilityResult.cshtml", _creatClaimsService.GetFacilityResult());
        }


        public ActionResult GetIcdHistory()
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/ClaimsInfo/_codingHistory.cshtml", _creatClaimsService.GetIcdHistory());
        }


        public ActionResult GetMedicalReview()
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/ClaimsInfo/_medicalReviewNew.cshtml", _creatClaimsService.GetMedicalReview());
        }

        public bool SaveCMS1500Form(BillingInfo BillingInfo)
        {


            return _creatClaimsService.SaveCMS1500Form(BillingInfo);
        }

        public ActionResult CreateServiceLine(int index)
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = "CodedEncounter.Procedures[" + index + "]";
            return PartialView("~/Areas/Billing/Views/CreateClaim/ClaimsInfo/_AddServiceLine.cshtml");
        }

        public ActionResult GetCPTCodeHistory()
        {
            return PartialView("~/Areas/Billing/Views/CreateClaim/ClaimsInfo/_CPTCodeHistory.cshtml", _creatClaimsService.GetCPTCodeHistory());
        }

    }
}