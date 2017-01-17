using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class StateController : Controller
    {
        /// <summary>
        /// IStateService object reference
        /// </summary>
        private IStateService _State = null;

        /// <summary>
        /// StateController constructor For StateService
        /// </summary>
        public StateController()
        {
            _State = new StateService();
        }

        //
        // GET: /CMS/State/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "State";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _State.GetAll();
            StateViewModel model = new StateViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/State/Index.cshtml", model);
        }

        //
        // POST: /CMS/State/AddEditState
        [HttpPost]
        public ActionResult AddEditState(string Code)
        {
            StateViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New State";
                model = new StateViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit State";
                model = _State.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/State/_AddEditStateForm.cshtml", model);
        }

        //
        // POST: /CMS/State/SaveState
        [HttpPost]
        public ActionResult SaveState(StateViewModel State)
        {
            if (ModelState.IsValid)
            {
                int TempID = State.StateID;

                if (TempID == 0)
                {
                    State.CreatedBy = "CMS Team";
                    State.Source = "CMS Server";
                    State = _State.Create(State);
                }
                else {
                    State.LastModifiedBy = "CMS Team Update";
                    State.Source = "CMS Server Update";
                    State = _State.Update(State); }

                if (State != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/State/_RowState.cshtml", State);

                    if (TempID == 0)
                        return Json(new { Message = "New State Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "State Updated Successfully", Status = true, Type = "Edit", Template = Template });
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