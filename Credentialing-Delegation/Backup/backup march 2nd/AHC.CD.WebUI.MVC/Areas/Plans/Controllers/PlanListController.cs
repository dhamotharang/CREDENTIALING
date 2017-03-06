using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.DTO;
using AHC.CD.Business.Plans;
using AHC.CD.Entities.Credentialing;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan;
using AHC.CD.WebUI.MVC.Areas.Plans.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Controllers
{
    public class PlanListController : Controller
    {
        private readonly IPlanManagerADO iPlanManagerADO = null;
        private readonly IPlanManagerEF iPlanManagerEF = null;
        public PlanListController(IPlanManagerADO iPlanManagerADO, IPlanManagerEF iPlanManagerEF)
        {
            this.iPlanManagerADO = iPlanManagerADO;
            this.iPlanManagerEF = iPlanManagerEF;
        }
        [Authorize(Roles = "CCO,CRA")]
        public async Task<ActionResult> Index()
        {
            ViewBag.PlanList = await iPlanManagerADO.getAllPlansAsync(); 
            return View();
        }        
        [HttpGet]
        public async Task<string> GetPlanDataByID(int ID)
        {
            string status = "false";
            PlanDataDTO temporaryPlanData = new PlanDataDTO();
            try
            {
                temporaryPlanData = await iPlanManagerEF.getPlanDataByID(ID);
                status = "true";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { PlanData = temporaryPlanData, Status = status }, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        [HttpPost]
        public async Task<string> RemovePlanDataByID(int ID)
        {
            string status = "true";
            int temporaryPlanID = 0;
            try
            {
                temporaryPlanID = await iPlanManagerEF.removePlanDataByID(ID);
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { PlanDataID = temporaryPlanID, Status = status });
        }
        [HttpPost]
        public async Task<string> ReactivePlanDataByID(int ID) {
            string status = "true";
            int temporaryPlanID = 0;
            try
            {
                temporaryPlanID = await iPlanManagerEF.reactivePlanDataByID(ID);
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { PlanDataID = temporaryPlanID, Status = status });
        }
        [HttpGet]
        public async Task<string> LoadMasterDataForPlan()
        {
            string status = "false";
            PlanMasterDataDTO planMasterDataDTO = new PlanMasterDataDTO();
            try
            {
                planMasterDataDTO = await iPlanManagerEF.getMasterDataForPlan();
                status = "true";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { PlanMasterDataDTO = planMasterDataDTO, Status = status });
        }
        [HttpPost]
        public async Task<string> SavePlanData(PlanListViewModel PlanData)
        {
            string status = "true";
            Plan Plan = new Plan();
            object data = new object();
            DocumentDTO PlanLogo = new DocumentDTO();
            try
            {
                if (ModelState.IsValid)
                {
                    if (PlanData.PlanLogoFile != null)
                    {
                        PlanLogo = CreateDocument(PlanData.PlanLogoFile);
                    }
                    Plan = AutoMapper.Mapper.Map<PlanListViewModel, Plan>(PlanData);
                    data = await iPlanManagerEF.AddPlanData(Plan, PlanLogo);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { PlanAddedDataByID = data, Status = status });
        }
        [HttpPost]
        public async Task<string> UpdatePlanData(PlanListViewModel PlanData)
        {
            string status = "true";
            Plan Plan = new Plan();
            object data = new object();
            DocumentDTO PlanLogo = new DocumentDTO();
            try
            {
                if (ModelState.IsValid)
                {
                    if (PlanData.PlanLogoFile != null)
                    {
                        PlanLogo = CreateDocument(PlanData.PlanLogoFile);
                    }
                    List<PlanListLOBViewModel> PlanLOBsViewModel = new List<PlanListLOBViewModel>();
                    foreach (var item in PlanData.PlanLOBs)
                    {
                        if (item.PlanID != 0 && item.PlanID!=null)
                        {
                            List<SubPlan> sub1 = AutoMapper.Mapper.Map<ICollection<SubPlansViewModel>, List<SubPlan>>(item.SubPlans);
                            List<LOBContactDetail> contacts = AutoMapper.Mapper.Map<ICollection<PlanLOBContactDetailsViewModel>, List<LOBContactDetail>>(item.LOBContactDetails);
                            List<LOBAddressDetail> addresses = AutoMapper.Mapper.Map<ICollection<PlanLOBAddressDetailsViewModel>, List<LOBAddressDetail>>(item.LOBAddressDetails);
                            iPlanManagerEF.UpdateSubPlans(sub1,item.PlanLOBID);
                            iPlanManagerEF.UpdatePlanContactDetail(contacts,item.PlanLOBID);
                            iPlanManagerEF.UpdatePlanAddress(addresses, item.PlanLOBID);
                        }
                        else
                        {
                            
                            item.PlanID = PlanData.PlanID;
                            PlanLOBsViewModel.Add(item);
                        }
                        
                    }
                    if (PlanLOBsViewModel.Count>0)
                    {
                        foreach (var item in PlanLOBsViewModel)
                        {
                            //PlanData.PlanLOBs.Remove(item);
                        }
                        List<PlanLOB> Data=AutoMapper.Mapper.Map<List<PlanListLOBViewModel>, List<PlanLOB>>(PlanLOBsViewModel);
                        iPlanManagerEF.AddRengeOFPlanLOBsData(Data);
                    }
                    Plan = AutoMapper.Mapper.Map<PlanListViewModel, Plan>(PlanData);
                    iPlanManagerEF.UpdatePlanContactForPlan(Plan.ContactDetails.ToList());
                    iPlanManagerEF.UpdatePlanAddressForPlan(Plan.Locations.ToList());
                    data = await iPlanManagerEF.UpdatePlanData(Plan, PlanLogo);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { PlanUpdatedDataByID = data, Status = status });
        }
        [HttpPost]
        public string AddPlanContract(List<PlanContractsViewModel> PlanContracts)
        {
            string status = "true";
            try
            {
                if (ModelState.IsValid)
                {
                    var DataModelPlanContract = AutoMapper.Mapper.Map<List<PlanContractsViewModel>, List<PlanContract>>(PlanContracts);
                    iPlanManagerEF.addPlanContracts(DataModelPlanContract);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new {Status = status});
        }
        [HttpPost]
        public string UpdatePlanContract(int PlanID, List<PlanContractsViewModel> PlanContracts)
        {
            string status = "true";
            try
            {
                if (ModelState.IsValid)
                {
                    List<PlanContract> PlanContractsDataFromDatabase = iPlanManagerEF.getAllPlanContractsByIDForCotractgrid(PlanID);
                    foreach(PlanContract planContract in PlanContractsDataFromDatabase){
                        planContract.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    }
                    iPlanManagerEF.updatePlanContracts(PlanContractsDataFromDatabase);
                    PlanContractsDataFromDatabase = AutoMapper.Mapper.Map<List<PlanContractsViewModel>, List<PlanContract>>(PlanContracts);
                    iPlanManagerEF.addPlanContracts(PlanContractsDataFromDatabase);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { Status = status });
        }
        [HttpGet]
        public async Task<string> GetPlanContarctDataByID(int PlanID)
        {
            string status = "true";
            object planContractDataByID = new object();
            try
            {
                planContractDataByID = await iPlanManagerEF.GetPlanContarctDataByID(PlanID);
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return JsonConvert.SerializeObject(new { PlanContractDataByID = planContractDataByID, Status = status });
        }



        private DocumentDTO CreateDocument(HttpPostedFileBase file, bool isRemoved = false)
        {
            DocumentDTO document = new DocumentDTO() { IsRemoved = isRemoved };
            if (file != null)
            {
                document.FileName = file.FileName;
                document.InputStream = file.InputStream;
            }

            return document;
        }

	}
}