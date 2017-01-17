using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class GroupController : Controller
    {
        /// <summary>
        /// IGroupService object reference
        /// </summary>
        private IGroupService _Group = null;

        /// <summary>
        /// GroupController constructor For GroupService
        /// </summary>
        public GroupController()
        {
            _Group = new GroupService();
        }

        //
        // GET: /CMS/Group/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "Group";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _Group.GetAll();
            GroupViewModel model = new GroupViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/Group/Index.cshtml", model);
        }

        //
        // POST: /CMS/Group/AddEditGroup
        [HttpPost]
        public ActionResult AddEditGroup(string Code)
        {
            GroupViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New Group";
                model = new GroupViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit Group";
                model = _Group.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/Group/_AddEditGroupForm.cshtml", model);
        }

        //
        // POST: /CMS/Group/SaveGroup
        [HttpPost]
        public ActionResult SaveGroup(GroupViewModel Group)
        {
            if (ModelState.IsValid)
            {
                int TempID = Group.GroupID;

                if (TempID == 0)
                {
                    Group.CreatedBy = "CMS Team";
                    Group.Source = "CMS Server";
                    Group = _Group.Create(Group);
                }
                else {
                    Group.LastModifiedBy = "CMS Team Update";
                    Group.Source = "CMS Server Update";
                    Group = _Group.Update(Group); }

                if (Group != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/Group/_RowGroup.cshtml", Group);

                    if (TempID == 0)
                        return Json(new { Message = "New Group Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "Group Updated Successfully", Status = true, Type = "Edit", Template = Template });
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