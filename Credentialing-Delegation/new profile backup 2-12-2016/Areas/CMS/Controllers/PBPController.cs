using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class PBPController : Controller
    {
        /// <summary>
        /// IPBPService object reference
        /// </summary>
        private IPBPService _PBP = null;

        /// <summary>
        /// PBPController constructor For PBPService
        /// </summary>
        public PBPController()
        {
            _PBP = new PBPService();
        }

        //
        // GET: /CMS/PBP/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "PBP";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _PBP.GetAll();
            PBPViewModel model = new PBPViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/PBP/Index.cshtml", model);
        }

        //
        // POST: /CMS/PBP/AddEditPBP
        [HttpPost]
        public ActionResult AddEditPBP(string Code)
        {
            PBPViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New PBP";
                model = new PBPViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit PBP";
                model = _PBP.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/PBP/_AddEditPBPForm.cshtml", model);
        }

        //
        // POST: /CMS/PBP/SavePBP
        [HttpPost]
        public ActionResult SavePBP(PBPViewModel PBP)
        {
            if (ModelState.IsValid)
            {
                int TempID = PBP.PBPID;

                if (TempID == 0)
                {
                    PBP.CreatedBy = "CMS Team";
                    PBP.Source = "CMS Server";
                    PBP = _PBP.Create(PBP);
                }
                else {
                    PBP.LastModifiedBy = "CMS Team Update";
                    PBP.Source = "CMS Server Update";
                    PBP = _PBP.Update(PBP); }

                if (PBP != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/PBP/_RowPBP.cshtml", PBP);

                    if (TempID == 0)
                        return Json(new { Message = "New PBP Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "PBP Updated Successfully", Status = true, Type = "Edit", Template = Template });
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