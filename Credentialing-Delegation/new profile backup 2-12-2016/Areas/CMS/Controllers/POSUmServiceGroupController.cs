using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class POSUmServiceGroupController : Controller
    {
        /// <summary>
        /// IPOSUmServiceGroupService object reference
        /// </summary>
        private IPOSUmServiceGroupService _POSUmServiceGroup = null;

        /// <summary>
        /// POSUmServiceGroupController constructor For POSUmServiceGroupService
        /// </summary>
        public POSUmServiceGroupController()
        {
            _POSUmServiceGroup = new POSUmServiceGroupService();
        }

        //
        // GET: /CMS/POSUmServiceGroup/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "POSUmServiceGroup";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _POSUmServiceGroup.GetAll();
            POSUmServiceGroupViewModel model = new POSUmServiceGroupViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/POSUmServiceGroup/Index.cshtml", model);
        }

        //
        // POST: /CMS/POSUmServiceGroup/AddEditPOSUmServiceGroup
        [HttpPost]
        public ActionResult AddEditPOSUmServiceGroup(string Code)
        {
            POSUmServiceGroupViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New POSUmServiceGroup";
                model = new POSUmServiceGroupViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit POSUmServiceGroup";
                model = _POSUmServiceGroup.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/POSUmServiceGroup/_AddEditPOSUmServiceGroupForm.cshtml", model);
        }

        //
        // POST: /CMS/POSUmServiceGroup/SavePOSUmServiceGroup
        [HttpPost]
        public ActionResult SavePOSUmServiceGroup(POSUmServiceGroupViewModel POSUmServiceGroup)
        {
            if (ModelState.IsValid)
            {
                int TempID = POSUmServiceGroup.POSUmServiceGroupID;

                if (TempID == 0)
                {
                    POSUmServiceGroup.CreatedBy = "CMS Team";
                    POSUmServiceGroup.Source = "CMS Server";
                    POSUmServiceGroup = _POSUmServiceGroup.Create(POSUmServiceGroup);
                }
                else {
                    POSUmServiceGroup.LastModifiedBy = "CMS Team Update";
                    POSUmServiceGroup.Source = "CMS Server Update";
                    POSUmServiceGroup = _POSUmServiceGroup.Update(POSUmServiceGroup); }

                if (POSUmServiceGroup != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/POSUmServiceGroup/_RowPOSUmServiceGroup.cshtml", POSUmServiceGroup);

                    if (TempID == 0)
                        return Json(new { Message = "New POSUmServiceGroup Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "POSUmServiceGroup Updated Successfully", Status = true, Type = "Edit", Template = Template });
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