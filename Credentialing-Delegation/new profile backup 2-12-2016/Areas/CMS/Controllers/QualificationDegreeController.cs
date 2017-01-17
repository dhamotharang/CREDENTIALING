using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services;
using PortalTemplate.Areas.CMS.Services.IServices;
using PortalTemplate.Helper;
using System;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CMS.Controllers
{
    public class QualificationDegreeController : Controller
    {
        /// <summary>
        /// IQualificationDegreeService object reference
        /// </summary>
        private IQualificationDegreeService _QualificationDegree = null;

        /// <summary>
        /// QualificationDegreeController constructor For QualificationDegreeService
        /// </summary>
        public QualificationDegreeController()
        {
            _QualificationDegree = new QualificationDegreeService();
        }

        //
        // GET: /CMS/QualificationDegree/
        public ActionResult Index()
        {
            ViewBag.TableData = null;
            ViewBag.Title = "QualificationDegree";
            ViewBag.FormTitle = "Add New " + ViewBag.Title;
            ViewBag.TableData = _QualificationDegree.GetAll();
            QualificationDegreeViewModel model = new QualificationDegreeViewModel() { Status = true };
            return PartialView("~/Areas/CMS/Views/QualificationDegree/Index.cshtml", model);
        }

        //
        // POST: /CMS/QualificationDegree/AddEditQualificationDegree
        [HttpPost]
        public ActionResult AddEditQualificationDegree(string Code)
        {
            QualificationDegreeViewModel model = null;
            if (String.IsNullOrEmpty(Code))
            {
                ViewBag.FormTitle = "Add New QualificationDegree";
                model = new QualificationDegreeViewModel() { Status = true };
            }
            else
            {
                ViewBag.FormTitle = "Edit QualificationDegree";
                model = _QualificationDegree.GetByUniqueCode(Code);
            }
            return PartialView("~/Areas/CMS/Views/QualificationDegree/_AddEditQualificationDegreeForm.cshtml", model);
        }

        //
        // POST: /CMS/QualificationDegree/SaveQualificationDegree
        [HttpPost]
        public ActionResult SaveQualificationDegree(QualificationDegreeViewModel QualificationDegree)
        {
            if (ModelState.IsValid)
            {
                int TempID = QualificationDegree.QualificationDegreeID;

                if (TempID == 0)
                {
                    QualificationDegree.CreatedBy = "CMS Team";
                    QualificationDegree.Source = "CMS Server";
                    QualificationDegree = _QualificationDegree.Create(QualificationDegree);
                }
                else {
                    QualificationDegree.LastModifiedBy = "CMS Team Update";
                    QualificationDegree.Source = "CMS Server Update";
                    QualificationDegree = _QualificationDegree.Update(QualificationDegree); }

                if (QualificationDegree != null)
                {
                    string Template = CustomHelper.RenderPartialToString(this.ControllerContext, "~/Areas/CMS/Views/QualificationDegree/_RowQualificationDegree.cshtml", QualificationDegree);

                    if (TempID == 0)
                        return Json(new { Message = "New QualificationDegree Added Successfully", Status = true, Type = "Add", Template = Template });
                    else
                        return Json(new { Message = "QualificationDegree Updated Successfully", Status = true, Type = "Edit", Template = Template });
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