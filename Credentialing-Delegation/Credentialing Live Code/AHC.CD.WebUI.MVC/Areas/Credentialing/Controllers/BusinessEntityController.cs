using AHC.CD.Business.Notification;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class BusinessEntityController : Controller
    {

        private IErrorLogger errorLogger = null;
        private IChangeNotificationManager notificationManager;

        public BusinessEntityController(IErrorLogger errorLogger, IChangeNotificationManager notificationManager) // Change Notifications
        {
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
        } 

        //
        // GET: /Credentialing/BusinessEntity/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllBussinessEntityPlanMappings()
        {
            var collection = new
            {
                data = "Sorry Back-end not implemented, back-end Dependency here"
            };

            return Json(new { BEPlanMappings = collection }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllBussinessEntityPlanMappingHistories()
        {
            var collection = new
            {
                data = "Sorry Back-end not implemented, back-end Dependency here"
            };

            return Json(new { BEPlanMappingHistories = collection }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddBEPlan(BussinessEntityPlanMappingViewModel BussinessEntityPlanMapping)
        {
            string status = "true";

            try
            {
                if (ModelState.IsValid)
                {
                    //dataModelPersonalDetail = AutoMapper.Mapper.Map<PersonalDetailViewModel, PersonalDetail>(personalDetail);
                    //// Change Notifications
                    //ChangeNotificationDetail notification = new ChangeNotificationDetail(profileId, User.Identity.Name, "Demographics - Personal Details", "Updated");
                    //await notificationManager.SaveNotificationDetailAsync(notification);
                    //await profileManager.UpdatePersonalDetailAsync(profileId, dataModelPersonalDetail);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = "Sorry unable to Map.";
            }

            return Json(new { status = status, BussinessEntityPlanMapping = BussinessEntityPlanMapping }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveBEPlan(int BEPlanMappingID)
        {
            string status = "true";
            //------- here entity model need, temp view model i am used, who changed we need to return object ----------------
            BussinessEntityPlanMappingViewModel BussinessEntityPlanMapping = null;

            try
            {
                BussinessEntityPlanMapping = new BussinessEntityPlanMappingViewModel();
            }
            catch (DatabaseValidationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                status = "Sorry unable to Map.";
            }

            return Json(new { status = status, BussinessEntityPlanMapping = BussinessEntityPlanMapping }, JsonRequestBehavior.AllowGet);
        }
	}
}