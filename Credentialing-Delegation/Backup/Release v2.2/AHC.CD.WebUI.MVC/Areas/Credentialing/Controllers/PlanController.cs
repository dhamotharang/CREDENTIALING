using AHC.CD.Business.Credentialing.PlanManager;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public bool IsPlanNameExist(string planName) {

            return planManager.IsPlanNameExist(planName);   
        
        }

        // To add new PLAN
        [HttpPost]
        public async Task<ActionResult> AddPlan(PlanViewModel planViewModel,HttpPostedFileBase PlanLogoFile)
        {
            int PlanId = 0;
       
            Plan Plan = null;

            string status = "true";

            try
            {
                if (ModelState.IsValid)
                {

                    planViewModel.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                    string fileName = null;

                    if (planViewModel .PlanLogoFile!= null)
                    {
                     fileName = Path.GetFileName(planViewModel.PlanLogoFile.FileName);
                     string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/PlanLogo"),fileName);
                     planViewModel.PlanLogoFile.SaveAs(filePath);
                    }
                  
                    Plan = AutoMapper.Mapper.Map<PlanViewModel, Plan>(planViewModel);
                    Plan.PlanLogoPath = "~/Content/Images/PlanLogo/" + fileName;

                    PlanId  = await planManager.AddPlanAsync(Plan);

                    foreach (var item in planViewModel.PlanLOBs)
                    {
                        List<SubPlan> sub1 = AutoMapper.Mapper.Map<ICollection<SubPlanViewModel>, List<SubPlan>>(item.SubPlans);
                        List<LOBContactDetail> contacts = AutoMapper.Mapper.Map<ICollection<LOBContactDetailViewModel>, List<LOBContactDetail>>(item.LOBContactDetails);
                        List<LOBAddressDetail> addresses = AutoMapper.Mapper.Map<ICollection<LOBAddressDetailViewModel>, List<LOBAddressDetail>>(item.LOBAddressDetails);
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
                    string fileName = null;

                    dataModelPlanDetail = AutoMapper.Mapper.Map<PlanViewModel, Plan>(planViewModel);

                    if (planViewModel.PlanLogoFile != null && planViewModel.PlanLogoPath == null)
                    {
                        fileName = Path.GetFileName(planViewModel.PlanLogoFile.FileName);
                        string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/PlanLogo"), fileName);
                        planViewModel.PlanLogoFile.SaveAs(filePath);
                        dataModelPlanDetail.PlanLogoPath = "~/Content/Images/PlanLogo/" + fileName;
                    }
                    else if (planViewModel.PlanLogoFile == null && planViewModel.PlanLogoPath != null)
                    {
                        dataModelPlanDetail.PlanLogoPath = planViewModel.PlanLogoPath;
                    }
                    else if (planViewModel.PlanLogoFile != null && planViewModel.PlanLogoPath != null)
                    {
                        fileName = Path.GetFileName(planViewModel.PlanLogoFile.FileName);
                        string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/PlanLogo"), fileName);
                        planViewModel.PlanLogoFile.SaveAs(filePath);
                        dataModelPlanDetail.PlanLogoPath = "~/Content/Images/PlanLogo/" + fileName;
                    }
                    else
                    {
                        dataModelPlanDetail.PlanLogoPath = "";
                    }

                    dataModelPlanDetail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                    foreach (var item in planViewModel.PlanLOBs)
                    {
                        List<SubPlan> sub1 = AutoMapper.Mapper.Map<ICollection<SubPlanViewModel>, List<SubPlan>>(item.SubPlans);
                        List<LOBContactDetail> contacts = AutoMapper.Mapper.Map<ICollection<LOBContactDetailViewModel>, List<LOBContactDetail>>(item.LOBContactDetails);
                        List<LOBAddressDetail> addresses = AutoMapper.Mapper.Map<ICollection<LOBAddressDetailViewModel>, List<LOBAddressDetail>>(item.LOBAddressDetails);
                        planManager.UpdateSubPlans(sub1);
                        planManager.UpdatePlanContactDetail(contacts);
                        planManager.UpdatePlanAddress(addresses);
                    }
                    planManager.UpdatePlanContactForPlan(dataModelPlanDetail.ContactDetails.ToList());
                    planManager.UpdatePlanAddressForPlan(dataModelPlanDetail.Locations.ToList());
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

            List<PlanContract> dataModelPlanContract = null;

            try { 
            
                 if(ModelState.IsValid){

                     dataModelPlanContract = AutoMapper.Mapper.Map<ICollection<PlanContractViewModel>, List<PlanContract>>(planContracts);
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


        // To update plan contract
        [HttpPost]
        public ActionResult UpdatePlanContracts(int PlanID , List<PlanContractViewModel> planContracts)  
        {
            string status = "true";

            try
            {
                if (ModelState.IsValid)
                {
                    var PlanContracts = planManager.GetPlanContractForPlan(PlanID);

                    List<PlanContract> dataModelPlanContract = new List<PlanContract>();

                    foreach (var item in PlanContracts)
                    {
                        item.StatusType = StatusType.Inactive;
                        dataModelPlanContract.Add(item);
                    }

                    planManager.UpdatePlanContracts(dataModelPlanContract); 

                    dataModelPlanContract = AutoMapper.Mapper.Map<ICollection<PlanContractViewModel>, List<PlanContract>>(planContracts);

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

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
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