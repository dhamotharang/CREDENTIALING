using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class RelationshipController : Controller
    {
        /// <summary>
        /// IRelationshipService object reference
        /// </summary>
        private IRelationshipService _Relationship = null;

        /// <summary>
        /// RelationshipController constructor For RelationshipService
        /// </summary>
        public RelationshipController()
        {
            _Relationship = new RelationshipService();
        }

        //
        // GET: /CMS/Relationship/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Relationship";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Relationship.GetAll();
            RelationshipViewModel model = new RelationshipViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Relationship/Index.cshtml", model);
        }

        //
        // POST: /CMS/Relationship/AddEditRelationship
        [HttpPost]
        public ActionResult AddEditRelationship(string Code)
        {
            RelationshipViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Relationship";
                model = new RelationshipViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Relationship";
                model = _Relationship.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Relationship/_AddEditRelationshipForm.cshtml", model);
        }

        //
        // POST: /CMS/Relationship/SaveRelationship
        [HttpPost]
        public ActionResult SaveRelationship(RelationshipViewModel Relationship)
        {
            if (ModelState.IsValid)
            {
                int TempID = Relationship.RelationshipID;

                if (TempID == 0)
                {
                    Relationship.CreatedBy = "CMS Team";
                    Relationship.Source = "CMS Server";
                    Relationship = _Relationship.Create(Relationship);
                }
                else {
                    Relationship.LastModifiedBy = "CMS Team Update";
                    Relationship.Source = "CMS Server Update";
                    Relationship = _Relationship.Update(Relationship); }

                if (Relationship != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Relationship/_RowRelationship.cshtml", Relationship);

                    if (TempID == 0)
                        return Json(new { Message = "New Relationship Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Relationship Updated Successfully", Status = true, Type = "Edit", Template = Template });
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