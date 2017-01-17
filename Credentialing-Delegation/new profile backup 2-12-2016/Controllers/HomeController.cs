using Newtonsoft.Json;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using PortalTemplate.Areas.UM.Models.ViewModels.InsertText;
using PortalTemplate.Models.UM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetCulture(string id)
        {
            if (id == null)
            {
                id = "";
            }
            CultureInfo ci = new CultureInfo(id);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            HttpCookie cultureCookie = new HttpCookie("cultureCookie");
            cultureCookie.Value = id;
            cultureCookie.Expires = DateTime.MaxValue;
            HttpContext.Response.Cookies.Add(cultureCookie);
            return RedirectToAction("Index", "Home");
        }       

        public ActionResult GetView(string viewName)
        {
            return PartialView("~/Views/ViewAuth/_ViewAuth.cshtml");
            //return PartialView("~/Views/" + viewName + ".cshtml");
        }
         public ActionResult GetPartial(string partialURL, string dataURL)
        {
            if (dataURL != null)
            {
                ViewBag.TableData = null;
                using (StreamReader r = new StreamReader(Server.MapPath(dataURL)))
                {
                    string json = r.ReadToEnd();
                    var items = JsonConvert.DeserializeObject(json);
                    ViewBag.TableData = items;
                }
            }
            PartialViewResult part = PartialView(partialURL);
            string p = part.ToString();
            return part;
        }

        public PartialViewResult GetPartialOnRequest(string partialURL)
        {
            return PartialView(partialURL);
        }

        public ActionResult GetBucketView()
        {
            return PartialView("~/Areas/UM/Views/ReferBuckets/_ReferBucket.cshtml");
        }

        [HttpPost]
        public ActionResult Authorization(AuthorizationViewModel CreateNewAuth)
        {
            try
            {
                return PartialView("~/Views/Home/_AuthorizationPreviewModal.cshtml", CreateNewAuth);
                //    return Json(new { Status = true, data = CreateNewAuth, Message = "New Authorization Created..." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception excp)
            {
                return null;
                //    return Json(new { Status = false, data = " ", Message = excp.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PendAuthorization(AuthorizationViewModel CreateNewAuth)
        {
            try
            {
                return PartialView("~/Views/Home/_AuthPendModal.cshtml", CreateNewAuth);
                //    return Json(new { Status = true, data = CreateNewAuth, Message = "New Authorization Created..." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception excp)
            {
                return null;
                //    return Json(new { Status = false, data = " ", Message = excp.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PortalAuthorization(AuthorizationViewModel CreateNewAuth)
        {
            try
            {
                return PartialView("~/Areas/UM/Views/PortalAuth/_PortalAuthPreviewModal.cshtml", CreateNewAuth);
            }
            catch (Exception excp)
            {
                return null;
            }
        }
        [HttpPost]
        public ActionResult PendPoratlAuthorization(AuthorizationViewModel CreateNewAuth)
        {
            try
            {
                return PartialView("~/Areas/UM/Views/PortalAuth/_PortalAuthPendModal.cshtml", CreateNewAuth);
            }
            catch (Exception excp)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult GetPlainLanguages(AuthorizationViewModel authobj)
        {
            try
            {
                return PartialView("~/Views/Home/_GetPlainLanguageModal.cshtml", authobj);
            }
            catch (Exception excp)
            {
                return null;
            }
        }

        public ActionResult GetApprovalPlainLanguages(AuthorizationViewModel authobj)
        {
            try
            {
                return PartialView("~/Areas/UM/Views/Common/Letter/_ApprovalLetter.cshtml", authobj);
            }
            catch (Exception excp)
            {
                return null;
            }
        }


        public object GetCPTCodes()
        {
            try
            {
                String path;
                path = HttpContext.Server.MapPath("~/Areas/UM/Resources/JSONData/Authorization/CPTCodes.JSON");
                using (System.IO.TextReader reader = System.IO.File.OpenText(path))
                {
                    string text = reader.ReadToEnd();
                    var data = JsonConvert.DeserializeObject(text);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetInsertTextList()
        {
            string file = "";
            file = HostingEnvironment.MapPath("~/Areas/UM/Resources/ServiceData/InsertTextList.js");
            var json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<InsertTextViewModel> InsertTextList = serial.Deserialize<List<InsertTextViewModel>>(json);
            return PartialView("~/Views/Home/Modal/_AddModifyTextSnippets.cshtml", InsertTextList);
        }

        public ActionResult AddNewInsertText()
        {
            return PartialView("~/Views/Home/Modal/_AddNewTextSnippet.cshtml");
        }
    }
}