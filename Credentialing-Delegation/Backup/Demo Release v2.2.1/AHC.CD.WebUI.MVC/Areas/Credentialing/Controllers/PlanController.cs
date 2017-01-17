using AHC.CD.Business.Credentialing.PlanManager;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class PlanController : Controller
    {
        private IPlanManager planManager = null;
        private IChangeNotificationManager notificationManager;
        private IErrorLogger errorLogger = null;

        public PlanController(IPlanManager planManager, IChangeNotificationManager notificationManager, IErrorLogger errorLogger)
        {
            this.planManager = planManager;
            this.errorLogger = errorLogger;
            this.notificationManager = notificationManager;
        }
      
        // GET: /Credentialing/Plan/
        // For Plan Listing -- All Plans which is present in system
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }


        [HttpGet]
        public ActionResult PlanContract()  
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult GetLobsForPlan(int PlanId)
        //{
        //    var Lobs = planManager.GetAllLobForPlan(PlanId);

        //    return Json(Lobs, JsonRequestBehavior.AllowGet);

        //}
        
        //[HttpGet]
        //public ActionResult ListPlanMapping()
        //{
        //    return View();
        //}

        

        [HttpPost]
        public bool IsPlanCodeExist(string planCode) {

            return planManager.IsPlanCodeExist(planCode);
        
        }

        // To add new PLAN
        [HttpPost]
        public async Task<ActionResult> AddPlan(PlanViewModel planViewModel)
        {
            int PlanId = 0;

            Plan Plan = null;

            string status = "true";

            try
            {
                if (ModelState.IsValid)
                {
                    planViewModel.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                    Plan = AutoMapper.Mapper.Map<PlanViewModel, Plan>(planViewModel);

                    PlanId  = await planManager.AddPlanAsync(Plan);

                    foreach (var item in planViewModel.PlanLOBs)
                    {
                        List<SubPlan> sub1 = AutoMapper.Mapper.Map<ICollection<SubPlanViewModel>, List<SubPlan>>(item.SubPlans);
                        List<PlanContactDetail> contacts = AutoMapper.Mapper.Map<ICollection<PlanContactDetailViewModel>, List<PlanContactDetail>>(item.ContactDetails);
                        List<PlanAddress> addresses = AutoMapper.Mapper.Map<ICollection<PlanAddressViewModel>, List<PlanAddress>>(item.AddressDetails);
                        await planManager.PlanLobSubPlanMapping(PlanId, item.LOBID, sub1, addresses, contacts);
                    }

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
                status = ExceptionMessage.PLAN_ADD_EXCEPTION;
            }

            //  calling method to get the lob for newly added plan
            var data = GetPlanLobForPlan(PlanId);

            return Json(new { status = status, LobDetail = data }, JsonRequestBehavior.AllowGet);
        }

        // To update the existing PLAN
        public async Task<ActionResult> UpdatePlanAsync(PlanViewModel planViewModel)
        {
            string status = "true";

            Plan dataModelPlanDetail = null;

            try
            {
                if (ModelState.IsValid)
                {
                    dataModelPlanDetail = AutoMapper.Mapper.Map<PlanViewModel,Plan>(planViewModel);
                    await planManager.UpdatePlanAsync(dataModelPlanDetail);
                }

                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (DatabaseValidationException ex)
            {
                //errorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                //errorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                //errorLogger.LogError(ex);
                status = ExceptionMessage.PLAN_UPDATE_EXCEPTION;
            }
            return Json(new { status = status, hospitalPrivilegeDetail = dataModelPlanDetail }, JsonRequestBehavior.AllowGet);
        }

        // To remove PLAN
        [HttpPost]
        public async Task<ActionResult> RemovePlan(int planid)  
        {
            string status = "true";

            try
            {
                if (ModelState.IsValid)
                {
                  //planid.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                  await planManager.RemovePlanAsync(planid);
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
                status = ExceptionMessage.PLAN_REMOVE_EXCEPTION;
            }

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        // To add plan contract
        [HttpPost]
        public ActionResult AddPlanContracts(List<PlanContractViewModel> planContracts) {

            string status = "true";

            try { 
            
                 if(ModelState.IsValid){

                     List<PlanContract> dataModelPlanContract = AutoMapper.Mapper.Map<ICollection<PlanContractViewModel>, List<PlanContract>>(planContracts);
                     planManager.AddPlanContracts(dataModelPlanContract);
                 
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
                status = ExceptionMessage.PLAN_REMOVE_EXCEPTION;
            }

            return Json(new { status = status}, JsonRequestBehavior.AllowGet);
        }

        //To get all lobs for a particuar plan based on its ID.
        public ActionResult GetPlanLobForPlan(int PlanId)   
       {

           var planLobs = planManager.GetPlanLobForPlan(PlanId);

           return Json(planLobs,JsonRequestBehavior.AllowGet);
       
       }

        [HttpPost]
        public ActionResult GetPlanContractForPlan(int PlanId)
        {
            var planContracts = planManager.GetPlanContractForPlan(PlanId);

            return Json(planContracts, JsonRequestBehavior.AllowGet);
        }

        // To add plan contact details for a plan
        //public ActionResult LobBeMapping(PlanContractViewModel planContractViewModel)
        //{
        //    string status = "true";

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            PlanContract planContract = AutoMapper.Mapper.Map<PlanContractViewModel, PlanContract>(planContractViewModel);
        //            planManager.LobBemapping(planContract);
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
        //        status = ExceptionMessage.PLAN_ADD_EXCEPTION;
        //    }

        //    return View();
        //}


        // To get plan contact details for a selected plan

        //public async Task<ActionResult> PlanBeMapping(PlanBEViewModel planBEViewModel)
        //{
        //    string status = "true";

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            await planManager.PlanBEMapping(planBEViewModel.PlanID, planBEViewModel.OrganizationGroupID);
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
        //        status = ExceptionMessage.PLAN_ADD_EXCEPTION;
        //    }

        //    return View();
        //}

        //Add plan contract 
        //public ActionResult PlanContract()
        //{
        //    return View();
        //}

        //View Plan Contract
        //public async Task<ActionResult> ViewPlanContract(int planID)
        //{
        //    return View();
        //}

        //Edit Plan Contract
        //public async Task<ActionResult> EditPlanContract(int planID)
        //{
        //    return View();
        //}



        //update plan
        //public string UpdatePlan(PlanViewModel planViewModel)
        //{
        //    return "success";
        //}
    }
}