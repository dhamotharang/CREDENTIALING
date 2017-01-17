using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.DTO;
using AHC.CD.Business.Profiles;
using AHC.CD.Data.ADO.DTO.Plan;
using AHC.CD.Data.ADO.Plan;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Resources.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Plans
{
    internal class PlanManagerEF : IPlanManagerEF
    {

        private readonly IPlansRepository iPlansRepository = null;

        private readonly IPlanRepositoryADO iPlanRepositoryADO = null;

        private readonly IUnitOfWork uow = null;

        private readonly IDocumentsManager documentManager = null;

        private readonly IProfileRepository profileRepository = null;

        private readonly ProfileDocumentManager profileDocumentManager = null;

        public PlanManagerEF(IPlanRepositoryADO iPlanRepositoryADO, IPlansRepository iPlansRepository, IUnitOfWork uow, IDocumentsManager documentManager)
        {
            this.iPlanRepositoryADO = iPlanRepositoryADO;
            this.iPlansRepository = iPlansRepository;
            this.uow = uow;
            this.profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
        }

        public async Task<PlanDataDTO> getPlanDataByID(int PlanId)
        {
            try
            {
                PlanDataDTO planDataDTO = new PlanDataDTO();
                planDataDTO.PlanData = await iPlansRepository.GetPlanByIdAsync(PlanId);
                planDataDTO.PlanContracts = await getAllPlanContractsByID(PlanId);
                return planDataDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> removePlanDataByID(int PlanId)
        {
            try
            {
                return await iPlansRepository.RemovePlanByIdAsync(PlanId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> reactivePlanDataByID(int PlanId)
        {
            try
            {
                return await iPlansRepository.ReactivePlanByIdAsync(PlanId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PlanMasterDataDTO> getMasterDataForPlan()
        {
            try
            {
                PlanMasterDataDTO planMasterDataDTO = new PlanMasterDataDTO();
                planMasterDataDTO.LOBs = (from LOBs in await uow.GetGenericRepository<LOB>().GetAllAsync()
                                          where LOBs.Status == "Active"
                                          select LOBs).AsEnumerable();
                planMasterDataDTO.BusinessEnities = (from BEs in await uow.GetGenericRepository<OrganizationGroup>().GetAllAsync()
                                                     where BEs.Status == "Active"
                                                     select BEs).AsEnumerable();
                return planMasterDataDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addPlanContracts(List<PlanContract> PlanContracts)
        {
            try
            {
                var planContractRepo = uow.GetGenericRepository<PlanContract>();
                planContractRepo.CreateRange(PlanContracts);
                planContractRepo.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void updatePlanContracts(List<PlanContract> PlanContracts)
        {
            try
            {
                var planContractRepo = uow.GetGenericRepository<PlanContract>();
                foreach (PlanContract planContract in PlanContracts)
                {
                    PlanContract updatedLOBContactDetail = planContractRepo.Find(p => p.PlanContractID == planContract.PlanContractID);
                    updatedLOBContactDetail = AutoMapper.Mapper.Map<PlanContract, PlanContract>(planContract, updatedLOBContactDetail);
                    planContractRepo.Update(updatedLOBContactDetail);
                }
                planContractRepo.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PlanContract>> getAllPlanContractsByID(int PlanID)
        {
            try
            {
                var PlanContractsData = (from PlanContracts in await uow.GetGenericRepository<PlanContract>().GetAllAsync("PlanLOB,PlanLOB.LOB,BusinessEntity")
                                     where PlanContracts.PlanLOB.PlanID == PlanID && PlanContracts.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                                     select PlanContracts).ToList();
                return PlanContractsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlanContract> getAllPlanContractsByIDForCotractgrid(int PlanID)
        {
            try
            {
                List<PlanContract> PlanContractsData = uow.GetGenericRepository<PlanContract>().GetAll("PlanLOB,PlanLOB.LOB,BusinessEntity").Where(x => x.PlanLOB.PlanID == PlanID && x.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();                    
                return PlanContractsData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<object> AddPlanData(Plan Plan, DocumentDTO AttachDocument)
        {
            try
            {

                Plan.PlanLogoPath = AddDocument(DocumentRootPath.PLANLOGO_DOCUMENT_PATH, AttachDocument);
                Plan.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                iPlansRepository.Create(Plan);
                await iPlansRepository.SaveAsync();
                return new
                {
                    PLanLOB = GetPlanLOBDataByPlanID(Plan.PlanID),
                    PlanDTO = await iPlanRepositoryADO.GetPlanDataByIDAsync(Plan.PlanID)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> UpdatePlanData(Plan Plan,DocumentDTO AttachDocument)
        {
            try
            {
                if (AttachDocument.InputStream!=null)
                {
                    Plan.PlanLogoPath = AddDocument(DocumentRootPath.PLANLOGO_DOCUMENT_PATH, AttachDocument);
                }
                foreach(var data in Plan.PlanLOBs){
                    data.PlanID = Plan.PlanID;
                }
                Plan.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                Plan PlanData = await iPlansRepository.FindAsync(x => x.PlanID == Plan.PlanID, "ContactDetails,ContactDetails.ContactDetail,Locations,PlanLOBs,PlanLOBs.LOB,PlanLOBs.SubPlans,PlanLOBs.LOBContactDetails, PlanLOBs.LOBContactDetails.ContactDetail,PlanLOBs.LOBContactDetails.ContactDetail.PhoneDetails,PlanLOBs.LOBContactDetails.ContactDetail.EmailIDs,PlanLOBs.LOBContactDetails.ContactDetail.PreferredContacts,PlanLOBs.LOBAddressDetails");
                PlanData = AutoMapper.Mapper.Map<Plan, Plan>(Plan, PlanData);
                iPlansRepository.Update(PlanData);
                await iPlansRepository.SaveAsync();
                return new
                {
                    PLanContract = await getAllPlanContractsByID(Plan.PlanID),
                    PLanLOB = GetPlanLOBDataByPlanID(Plan.PlanID),
                    PlanDTO = await iPlanRepositoryADO.GetPlanDataByIDAsync(Plan.PlanID)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetPlanContarctDataByID(int PlanID)
        {
            try
            {
                return new
                    {
                        PLanContract = await getAllPlanContractsByID(PlanID),
                        PLanLOB = GetPlanLOBDataByPlanID(PlanID)
                    };
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }



        private string AddDocument(string docRootPath, DocumentDTO document)
        {
            document.DocRootPath = docRootPath;
            return profileDocumentManager.AddDocumentInPath(document);
        }

        private IEnumerable<PlanLOB> GetPlanLOBDataByPlanID(int PlanID)
        {
            try
            {
                return uow.GetGenericRepository<PlanLOB>().GetAll("LOB").Where(x => x.PlanID == PlanID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSubPlans(List<SubPlan> SubPlans, int? PlanLOBID)
        {
            var subPlanRepo = uow.GetGenericRepository<SubPlan>();
            List<int> TempData = new List<int>();
            int i = 0;
            foreach (var subPlan in SubPlans)
            {
                subPlanRepo.Update(subPlan);
                SubPlan updatedSubPlan = subPlanRepo.Find(p => p.SubPlanId == subPlan.SubPlanId);
                if (updatedSubPlan != null)
                {
                    updatedSubPlan = AutoMapper.Mapper.Map<SubPlan, SubPlan>(subPlan, updatedSubPlan);
                    subPlanRepo.Update(updatedSubPlan);
                }
                else
                {
                    subPlanRepo.Create(subPlan);
                    TempData.Add(i);
                }
                i++;
            }
            subPlanRepo.Save();
            if (TempData.Count > 0)
            {
                var planLOBRepo = uow.GetGenericRepository<PlanLOB>();
                PlanLOB LOBData = planLOBRepo.Find(x => x.PlanLOBID == PlanLOBID, "SubPlans");
                foreach (int data in TempData)
                {
                    LOBData.SubPlans.Add(SubPlans.ElementAt(data));
                }
                planLOBRepo.Save();
            }
        }

        public void UpdatePlanContactDetail(List<LOBContactDetail> planContacts,int? PlanLOBID)
        {
            try
            {
                var planContactRepo = uow.GetGenericRepository<LOBContactDetail>();
                List<int> TempData = new List<int>();
                int i = 0;
                foreach (var planContact in planContacts)
                {
                    
                    LOBContactDetail updatedLOBContactDetail = planContactRepo.Find(p => p.LOBContactDetailID == planContact.LOBContactDetailID, "ContactDetail, ContactDetail.PhoneDetails, ContactDetail.EmailIDs, ContactDetail.PreferredWrittenContacts, ContactDetail.PreferredContacts");
                    if (updatedLOBContactDetail != null)
                    {
                        updatedLOBContactDetail = AutoMapper.Mapper.Map<LOBContactDetail, LOBContactDetail>(planContact, updatedLOBContactDetail);
                        planContactRepo.Update(updatedLOBContactDetail);
                    }
                    else {
                        planContactRepo.Create(planContact);
                        TempData.Add(i);
                    }
                    i++;
                }

                planContactRepo.Save();
                if (TempData.Count>0)
                {
                    var planLOBRepo = uow.GetGenericRepository<PlanLOB>();
                    PlanLOB LOBData = planLOBRepo.Find(x => x.PlanLOBID == PlanLOBID, "LOBContactDetails,LOBContactDetails.ContactDetail,LOBContactDetails.ContactDetail.PhoneDetails, LOBContactDetails.ContactDetail.EmailIDs, LOBContactDetails.ContactDetail.PreferredWrittenContacts, LOBContactDetails.ContactDetail.PreferredContacts");
                    foreach(int data in TempData){
                        LOBData.LOBContactDetails.Add(planContacts.ElementAt(data));
                    }
                    planLOBRepo.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePlanAddress(List<LOBAddressDetail> planAddresses, int? PlanLOBID)
        {
            try
            {
                var planAddressRepo = uow.GetGenericRepository<LOBAddressDetail>();
                List<int> TempData = new List<int>();
                int i = 0;
                foreach (var planAddress in planAddresses)
                {
                    if (planAddress.LOBAddressDetailID==0)
                    {
                        planAddressRepo.Create(planAddress);
                        TempData.Add(i);
                    }
                    else
                    {
                        planAddressRepo.Update(planAddress);
                    }
                    i++;
                }
                planAddressRepo.Save();
                if (TempData.Count > 0)
                {
                    var planLOBRepo = uow.GetGenericRepository<PlanLOB>();
                    PlanLOB LOBData = planLOBRepo.Find(x => x.PlanLOBID == PlanLOBID, "LOBAddressDetails");
                    foreach (int data in TempData)
                    {
                        LOBData.LOBAddressDetails.Add(planAddresses.ElementAt(data));
                    }
                    planLOBRepo.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePlanContactForPlan(List<PlanContactDetail> planContactDetails)
        {
            try
            {
                var planContactDetailRepo = uow.GetGenericRepository<PlanContactDetail>();
                // var planContactDetailRepo1 = uow.GetGenericRepository<ContactDetail>();
                var phoneDetailRepo = uow.GetGenericRepository<PhoneDetail>();
                var EmailDetailRepo = uow.GetGenericRepository<EmailDetail>();

                foreach (var planContact in planContactDetails)
                {
                    if (planContact.ContactDetail != null)
                    {
                        foreach (var PhoneNumber in planContact.ContactDetail.PhoneDetails)
                        {
                            if (PhoneNumber.PhoneDetailID == 0)
                            {
                                phoneDetailRepo.Create(PhoneNumber);
                                phoneDetailRepo.Save();
                            }
                            else
                            {
                                PhoneDetail AddPhoneDetail = phoneDetailRepo.Find(a => a.PhoneDetailID == PhoneNumber.PhoneDetailID);
                                AddPhoneDetail = AutoMapper.Mapper.Map<PhoneDetail, PhoneDetail>(PhoneNumber, AddPhoneDetail);
                                phoneDetailRepo.Update(AddPhoneDetail);
                            }
                        }
                        foreach (var Emailids in planContact.ContactDetail.EmailIDs)
                        {
                            if (Emailids.EmailDetailID == 0)
                            {
                                EmailDetailRepo.Create(Emailids);
                                EmailDetailRepo.Save();
                            }
                            else
                            {
                                EmailDetail AddEmailDetail = EmailDetailRepo.Find(a => a.EmailDetailID == Emailids.EmailDetailID);
                                AddEmailDetail = AutoMapper.Mapper.Map<EmailDetail, EmailDetail>(Emailids, AddEmailDetail);
                                EmailDetailRepo.Update(AddEmailDetail);
                            }
                        }
                    }

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
            try
            {
                var planAddressRepo = uow.GetGenericRepository<PlanAddress>();
                foreach (var planAddress in planAddressDetails)
                {
                    planAddressRepo.Update(planAddress);
                }

                planAddressRepo.Save();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }



        public void AddRengeOFPlanLOBsData(List<PlanLOB> PlanLOBsData)
        {
            try
            {
                var planLOBRepo = uow.GetGenericRepository<PlanLOB>();
                planLOBRepo.CreateRange(PlanLOBsData);
                planLOBRepo.Save();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
