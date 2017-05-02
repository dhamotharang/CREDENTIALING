using AHC.CD.Business.Credentialing.AppointmentInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class CCMPortalController : Controller
    {
        private IAppointmentManager appointmentManager = null;

        public CCMPortalController(IAppointmentManager appointmentManager)
        {
            this.appointmentManager = appointmentManager;
        }
        //
        // GET: /Credentialing/CCMPortal/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SPAIndexPage()
        {
            return PartialView("~/Areas/Credentialing/Views/CCMPortal/_SPAIndex.cshtml");
        }
        public ActionResult SPA_CCMAction()
        {
            return PartialView("~/Areas/Credentialing/Views/CCMPortal/_SPA_CCMAction.cshtml");
        }
        public ActionResult SPA_PSV()
        {
            return PartialView("~/Areas/Credentialing/Views/CCMPortal/_SPA_PSV.cshtml");
        }
        public ActionResult SPA_Document()
        {
            return PartialView("~/Areas/Credentialing/Views/CCMPortal/_SPA_Document.cshtml");
        }

        public async Task<ActionResult> GetAllAppointmentsList(string ApprovalStatus = null)
        {
            var data = await appointmentManager.GetCCMAppointmentsInfo(ApprovalStatus);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}