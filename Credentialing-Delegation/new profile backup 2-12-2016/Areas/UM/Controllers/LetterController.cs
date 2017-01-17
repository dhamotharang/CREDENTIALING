using PortalTemplate.Areas.UM.Models.ViewModels.Letter;
using PortalTemplate.Areas.UM.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class LetterController : Controller
    {
        LetterServices service = new LetterServices();
        //
        // GET: /UM/Letter/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LetterPreview(int ID,string MemberID,string LetterTemplateName)
        {
            ApprovalLetterViewModel ApprovalLetterModel = new ApprovalLetterViewModel();
            ApprovalLetterModel = service.PreviewLetter(ID,MemberID,LetterTemplateName);
            return PartialView("~/Areas/UM/Views/Common/Letter/_ApprovalLetter.cshtml", ApprovalLetterModel);
            
        }
	}
}