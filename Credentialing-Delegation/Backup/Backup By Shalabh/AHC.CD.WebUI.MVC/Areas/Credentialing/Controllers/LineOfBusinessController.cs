using AHC.CD.Business.Credentialing.PlanManager;
using AHC.CD.Entities.Credentialing;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class LineOfBusinessController : Controller
    {
         //private IPlanManager planManager = null;
        private IErrorLogger errorLogger = null;

        public LineOfBusinessController(IErrorLogger errorLogger)
        {
            //this.planManager = planManager;
            this.errorLogger = errorLogger;
        }

        //
        // GET: /Credentialing/LineOfBusiness/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Credentialing/ListLOB/
        public ActionResult ListLOB()
        {
            return View();
        }

        //
        // GET: /Credentialing/AddEdit/
        public ActionResult AddEdit()
        {

            return View();
        }


        // To add new LOB
        //public async Task<ActionResult> AddLOB(LOBViewModel lineOfBusiness)
        //{
        //    string status = "true";

        //    LOB dataModelLOBDetail = null;

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            dataModelLOBDetail = AutoMapper.Mapper.Map<LOBViewModel, LOB>(lineOfBusiness);

        //            await planManager.AddLOBAsync(dataModelLOBDetail);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
        //        }
        //    }
        //    catch (DatabaseValidationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.ValidationError;
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ExceptionMessage.LOB_ADD_EXCEPTION;
        //    }
        //    return Json(new { status = status, hospitalPrivilegeDetail = dataModelLOBDetail }, JsonRequestBehavior.AllowGet);
        //}

        // To update the existing LOB
        //public async Task<ActionResult> UpdatePlanAsync(int lobId, LOBViewModel lineOfBusiness)
        //{
        //    string status = "true";

        //    LOB dataModelLOBDetail = null;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            dataModelLOBDetail = AutoMapper.Mapper.Map<LOBViewModel, LOB>(lineOfBusiness);

        //            await planManager.UpdateLOBAsync(lobId, dataModelLOBDetail);
        //        }
        //        else
        //        {
        //            status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
        //        }
        //    }
        //    catch (DatabaseValidationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.ValidationError;
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorLogger.LogError(ex);
        //        status = ExceptionMessage.LOB_UPDATE_EXCEPTION;
        //    }
        //    return Json(new { status = status, hospitalPrivilegeDetail = dataModelLOBDetail }, JsonRequestBehavior.AllowGet);
        //}

	}
}