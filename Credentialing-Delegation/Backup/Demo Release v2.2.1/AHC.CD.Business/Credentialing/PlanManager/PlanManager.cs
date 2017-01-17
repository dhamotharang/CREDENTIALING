using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Exceptions.Plan;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.Business.Credentialing.PlanManager
{
    public class PlanManager : IPlanManager
    {
        private IPlansRepository plansRepository = null;

        private IUnitOfWork uow = null;

        public PlanManager(IUnitOfWork uow)
        {
            this.uow = uow;
            this.plansRepository = uow.GetPlanRepository();
        }

        public async Task UpdatePlanAsync(Entities.Credentialing.Plan plan)
        {
            try
            {
                //Add plan details
                plansRepository.Update(plan);
                //save the information in the repository
                await plansRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlanManagerException(ExceptionMessage.PLAN_UPDATE_EXCEPTION, ex);
            }
        }

        public async Task<int> AddPlanAsync(Entities.Credentialing.Plan plan)
        {
            try
            {
               
                //Add plan details
                plansRepository.Create(plan);
                //save the information in the repository
                await plansRepository.SaveAsync();
                return plan.PlanID;
               
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlanManagerException(ExceptionMessage.PLAN_ADD_EXCEPTION, ex);
            }
        }

        public async Task PlanBEMapping(int PlanId, int BeId)
        {
            try
            {
                var planBeRepository = uow.GetGenericRepository<PlanBE>();
                PlanBE planBe = new PlanBE();
                planBe.PlanID = PlanId;
                planBe.OrganizationGroupID = BeId;
                planBeRepository.Create(planBe);
                await planBeRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlanManagerException(ExceptionMessage.PLAN_ADD_EXCEPTION, ex);
            }
        }


        public async Task<List<int>> AddSubPlans(List<SubPlan> subPlans)
        {
             List<int> subPlanIds = new List<int>(); 
             var subPlanRepo = uow.GetGenericRepository<SubPlan>();
             subPlanRepo.CreateRange(subPlans);
             await subPlanRepo.SaveAsync();

            foreach (var subPlan in subPlans)
            {
                subPlanIds.Add(subPlan.SubPlanId);
            }

            return subPlanIds;
        }

        public async Task PlanLobSubPlanMapping(int PlanId, int LobId, List<SubPlan> SubPlan,List<PlanAddress>addresses,List<PlanContactDetail>contacts)
        {
                var pLobRepo = uow.GetGenericRepository<PlanLOB>();
                PlanLOB pLob = new PlanLOB();
                pLob.PlanID = PlanId;
                pLob.LOBID = LobId;
                pLob.SubPlans = SubPlan;
                pLob.Locations = addresses;
                pLob.ContactDetails = contacts;
                pLobRepo.Create(pLob);
            
               await pLobRepo.SaveAsync();
        }


        public List<LOB> GetAllLobForPlan(int PlanId)
        {
            var planLobRepo = uow.GetGenericRepository<PlanLOB>();
            IEqualityComparer<PlanLOB> custom = new LobComparer();
            var Lobs = planLobRepo.GetAll("LOB").Where(x => x.PlanID == PlanId).Distinct( new LobComparer()).Select(x => x.LOB);
            return  Lobs.ToList();
        }


        public async Task LobBemapping(PlanContract planContract)
        {
            var planContractRepo = uow.GetGenericRepository<PlanContract>();
            planContractRepo.Create(planContract);
            await planContractRepo.SaveAsync();
        }


        public async Task RemovePlanAsync(int planid)
        {
             try
            {
               var planRepo = uow.GetGenericRepository<Plan>();
               planRepo.Find(planid).StatusType = StatusType.Inactive;
               await planRepo.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PlanManagerException(ExceptionMessage.PLAN_REMOVE_EXCEPTION, ex);
            }
        }


        public List<PlanLOB> GetPlanLobForPlan(int planlId)
        {
            var planLOBRepo = uow.GetGenericRepository<PlanLOB>();
            var planLobs = planLOBRepo.GetAll("LOB").Where(x => x.PlanID == planlId);
            return planLobs.ToList();
        }



        public List<PlanContract> GetPlanContractForPlan(int planlId)
        {
            var planContractRepo = uow.GetGenericRepository<PlanContract>();
            var planContracts = planContractRepo.GetAll("PlanLOB,PlanLOB.LOB,BusinessEntity").Where(x=>x.PlanLOB.PlanID==planlId);
            return planContracts.ToList();

        }

        public async Task AddPlanContracts(List<PlanContract> planContracts)
        {
            var planContractRepo = uow.GetGenericRepository<PlanContract>();
            planContractRepo.CreateRange(planContracts);
           await planContractRepo.SaveAsync();
        }





        public bool IsPlanCodeExist(string planCode)
        {
          return plansRepository.GetAll().Any(x=>x.PlanCode.ToLower()== planCode.ToLower());
        }
    }
}