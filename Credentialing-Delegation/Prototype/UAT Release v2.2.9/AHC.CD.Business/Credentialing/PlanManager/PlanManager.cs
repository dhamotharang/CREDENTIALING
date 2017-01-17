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
using AHC.CD.Entities.MasterProfile.Demographics;

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

        public async Task PlanLobSubPlanMapping(int PlanId, int LobId, List<SubPlan> SubPlan,List<LOBAddressDetail>addresses,List<LOBContactDetail>contacts)
        {
                var pLobRepo = uow.GetGenericRepository<PlanLOB>();
                PlanLOB pLob = new PlanLOB();
                pLob.PlanID = PlanId;
                pLob.LOBID = LobId;
                pLob.SubPlans = SubPlan;
                pLob.LOBAddressDetails = addresses;
                pLob.LOBContactDetails = contacts;
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
            var planContracts = planContractRepo.GetAll("PlanLOB,PlanLOB.LOB,BusinessEntity").Where(x=>x.PlanLOB.PlanID==planlId && x.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
            return planContracts.ToList();

        }

        public void AddPlanContracts(List<PlanContract> planContracts)
        {
            try
            {
                var planContractRepo = uow.GetGenericRepository<PlanContract>();
                planContractRepo.CreateRange(planContracts);
                planContractRepo.Save();                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsPlanNameExist(string planName)        
        {
            return plansRepository.GetAll().Any(x => x.PlanName.ToLower() == planName.ToLower());
        }

        public void UpdateSubPlans(List<SubPlan> SubPlans)
        {
            var subPlanRepo = uow.GetGenericRepository<SubPlan>();

            foreach (var subPlan in SubPlans)
            {
                subPlanRepo.Update(subPlan);
            }
            subPlanRepo.Save();
        }

        public void UpdatePlanContactDetail(List<LOBContactDetail> planContacts)
        {
            try
            {
                var planContactRepo = uow.GetGenericRepository<LOBContactDetail>();
                foreach (var planContact in planContacts)
                {
                    LOBContactDetail updatedLOBContactDetail = planContactRepo.Find(p => p.LOBContactDetailID == planContact.LOBContactDetailID, "ContactDetail, ContactDetail.PhoneDetails, ContactDetail.EmailIDs, ContactDetail.PreferredWrittenContacts, ContactDetail.PreferredContacts");
                    updatedLOBContactDetail = AutoMapper.Mapper.Map<LOBContactDetail, LOBContactDetail>(planContact, updatedLOBContactDetail);
                    planContactRepo.Update(updatedLOBContactDetail);
                }

                planContactRepo.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePlanAddress(List<LOBAddressDetail> planAddresses)
        {
            var planAddressRepo = uow.GetGenericRepository<LOBAddressDetail>();

            foreach (var planAddress in planAddresses)
            {
                planAddressRepo.Update(planAddress);
            }
            planAddressRepo.Save();
        }

        public void UpdatePlanContactForPlan(List<PlanContactDetail> planContactDetails)
        {
            try
            {
            var planContactDetailRepo = uow.GetGenericRepository<PlanContactDetail>();
               // var planContactDetailRepo1 = uow.GetGenericRepository<ContactDetail>();

            foreach (var planContact in planContactDetails)
            {
                PlanContactDetail updatedPlanContactDetail = planContactDetailRepo.Find(p => p.PlanContactDetailID == planContact.PlanContactDetailID, "ContactDetail, ContactDetail.PhoneDetails, ContactDetail.EmailIDs, ContactDetail.PreferredWrittenContacts, ContactDetail.PreferredContacts");
                updatedPlanContactDetail = AutoMapper.Mapper.Map<PlanContactDetail, PlanContactDetail>(planContact, updatedPlanContactDetail);
                planContactDetailRepo.Update(updatedPlanContactDetail);
            }
                //planContactDetailRepo1.Save();
                planContactDetailRepo.Save();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public void UpdatePlanAddressForPlan(List<PlanAddress> planAddressDetails)
        {
            var planAddressRepo = uow.GetGenericRepository<PlanAddress>();
            foreach (var planAddress in planAddressDetails)
            {
                planAddressRepo.Update(planAddress);
            }

            planAddressRepo.Save();
        }

        public async Task<object> GetPlanDetail(int planId)
        {
            try
            {
                //var includeProperties = new string[]
                //{
                //    "PlanLOBs",
                   
                //};

                var plan = await plansRepository.FindAsync(p => p.PlanID == planId);

                var planLOB = GetAllLobForPlan(planId);

                if (plan == null)
                    throw new Exception("Invalid Plan");

                return new
                {
                    PlanDetail = plan,
                    PlanLob = planLOB,
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
            //catch (Exception ex)
            //{
            //    throw new ProfileManagerException(ExceptionMessage.PROFILE_CONTRACT_INFORAMTION_BY_ID_GET_EXCEPTION, ex);
            //}
        }

        public void UpdatePlanContracts(List<PlanContract> planContracts)
        {
            try
            {
                var planContractRepo = uow.GetGenericRepository<PlanContract>();

                foreach (var planContract in planContracts) 
                {
                    PlanContract updatedLOBContactDetail = planContractRepo.Find(p => p.PlanContractID == planContract.PlanContractID); 
                    updatedLOBContactDetail = AutoMapper.Mapper.Map<PlanContract, PlanContract>(planContract, updatedLOBContactDetail);
                    planContractRepo.Update(updatedLOBContactDetail);
                }

                planContractRepo.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}