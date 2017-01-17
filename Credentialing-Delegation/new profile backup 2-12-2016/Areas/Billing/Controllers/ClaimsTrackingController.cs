
using PortalTemplate.Areas.Billing.Models.Claims_Tracking;
using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Services;
using PortalTemplate.Areas.Billing.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing.Controllers
{
    public class ClaimsTrackingController : Controller
    {
        //
        // GET: /ClaimsTracking/
        readonly IClaimsTrackingService _ClaimsTrackingService;
        public ClaimsTrackingController()
        {
            _ClaimsTrackingService = new ClaimsTrackingService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTrakingClaimList(int status)
        {

            ActionResult partialView = null;
            try
            {

                switch (status)
                {
                    case 0:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_Open.cshtml", _ClaimsTrackingService.GetOpenClaimsList());
                        break;
                    case 1:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_OnHold.cshtml", _ClaimsTrackingService.GetOnHoldClaimsList());
                        break;
                    case 2:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_Rejected.cshtml", _ClaimsTrackingService.GetRejectedClaimsList());
                        break;
                    case 3:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_CommitteeReview.cshtml", _ClaimsTrackingService.GetComitteeReviewListClaimsList());
                        break;
                    case 4:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_PhysicianOnHold.cshtml", _ClaimsTrackingService.GetPhysicianOnHoldListClaimsList());
                        break;
                    case 5:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_Accepted.cshtml", _ClaimsTrackingService.GetAcceptedClaimsList());
                        break;
                    case 6:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_Dispatched.cshtml", _ClaimsTrackingService.GetDispatchedClaimsList());
                        break;
                    case 7:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_UnAckByCH.cshtml", _ClaimsTrackingService.GetUnAckByCHClaimsList());
                        break;
                    case 8:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_RejectedByCH.cshtml", _ClaimsTrackingService.GetRejectedByCHClaimsList());
                        break;
                    case 9:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_AcceptedByCH.cshtml", _ClaimsTrackingService.GetAcceptedByCHClaimsList());
                        break;
                    case 10:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_RejectedByPayer.cshtml", _ClaimsTrackingService.GetRejectedByPayerClaimsList());
                        break;
                    case 11:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_UnAckByPayer.cshtml", _ClaimsTrackingService.GetUnackByPayerClaimsList());
                        break;
                    case 12:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_AcceptedByPayer.cshtml", _ClaimsTrackingService.GetAcceptedByPayerClaimsList());
                        break;
                    case 13:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_Pending.cshtml", _ClaimsTrackingService.GetPendingClaimsList());
                        break;
                    case 14:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_DeniedByPayer.cshtml", _ClaimsTrackingService.GetDeniedByPayerClaimsList());
                        break;
                    case 15:
                        partialView = PartialView("~/Areas/Billing/Views/ClaimsTracking/ClaimList/_EOBReceived.cshtml", _ClaimsTrackingService.GetEOBReceivedClaimsList());
                        break;
                    default:
                        partialView = View("Error, please pass valid parameter");
                        break;
                }

                return partialView;
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public ActionResult GetCms1500Form(int ClaimId)
        {
            return PartialView("~/Areas/Billing/Views/ClaimsTracking/_CMS1500Form.cshtml", _ClaimsTrackingService.ViewCms1500Form(ClaimId));
        }


        public ActionResult ViewCms1500Form(int ClaimId)
        {
            return PartialView("~/Areas/Billing/Views/ClaimsTracking/_CMS1500ViewForm.cshtml", _ClaimsTrackingService.ViewCms1500Form(ClaimId));
        }


        public ActionResult GetCms1500FormInstance(int ClaimId)
        {
            return PartialView("~/Areas/Billing/Views/ClaimsTracking/_Cms1500ActivityLogForm.cshtml", _ClaimsTrackingService.GetCms1500FormInstance(ClaimId));
        }


        public ActionResult GetEobReport(int ClaimId)
        {
            return PartialView("~/Areas/Billing/Views/ClaimsTracking/_EOBReport.cshtml", _ClaimsTrackingService.GetEobReport(ClaimId));
        }

    }
}