using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class IPAController : Controller
    {
        /// <summary>
        /// IIPAService object reference
        /// </summary>
        private IIPAService _IPA = null;

        /// <summary>
        /// IPAController constructor For IPAService
        /// </summary>
        public IPAController()
        {
            _IPA = new IPAService();
        }

        //
        // GET: /CMS/IPA/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "IPA";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _IPA.GetAll();
            IPAViewModel model = new IPAViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/IPA/Index.cshtml", model);
        }

        //
        // POST: /CMS/IPA/AddEditIPA
        [HttpPost]
        public ActionResult AddEditIPA(string Code)
        {
            IPAViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New IPA";
                model = new IPAViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit IPA";
                model = _IPA.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/IPA/_AddEditIPAForm.cshtml", model);
        }

        //
        // POST: /CMS/IPA/SaveIPA
        [HttpPost]
        public ActionResult SaveIPA(IPAViewModel IPA)
        {
            if (ModelState.IsValid)
            {
                int TempID = IPA.IPAID;

                if (TempID == 0)
                {
                    IPA.CreatedBy = "CMS Team";
                    IPA.Source = "CMS Server";
                    IPA = _IPA.Create(IPA);
                }
                else {
                    IPA.LastModifiedBy = "CMS Team Update";
                    IPA.Source = "CMS Server Update";
                    IPA = _IPA.Update(IPA); }

                if (IPA != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/IPA/_RowIPA.cshtml", IPA);

                    if (TempID == 0)
                        return Json(new { Message = "New IPA Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "IPA Updated Successfully", Status = true, Type = "Edit", Template = Template });
                }
                else
                {
                    return Json(new { Message = "Sorry! Try Again", Status = false });
                }
            }
            return Json(new { Message = "Sorry! Try Again", Status = false });
        }
    }
}