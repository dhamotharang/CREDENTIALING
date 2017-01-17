using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace AHC.CD.Business
{
    internal class IndividualCredentialingManager : IIndividualCredentialingManager
    {
        IGenericRepository<InsuranceCompany> insuranceCompaniesRepository = null;
        IPlansRepository plansRepository = null;
        ICredentialingRepository credentialingRepository = null;
       
        
        public IndividualCredentialingManager(IUnitOfWork uow)
        {
            insuranceCompaniesRepository = uow.GetInsuranceCompaniesRepository();
            plansRepository = uow.GetPlansRepository();
            credentialingRepository = uow.GetCredentialingRepository();
        }
        
        
        public async Task<IEnumerable<Entities.Credentialing.Plan>> GetAllPlansAsync()
        {
           // IPlansRepository = new PlansEFRepository();
            return  await plansRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Entities.Credentialing.InsuranceCompany>> GetAllInsuranceCompaniesAsync()
        {
            return await insuranceCompaniesRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Entities.Credentialing.Plan>> GetAllPlansForInsuranceCompanyAsync(int insuranceCompanyID)
        {
            return await plansRepository.GetAllPlansForInsuranceCompanyAsync(insuranceCompanyID);
        }

        public async Task<IEnumerable<Entities.Credentialing.IndividualPlan>> GetAllCredentialedIndividualPlansAsync(int individualProviderID)
        {
           
            return await credentialingRepository.GetAllCredentialedIndividualPlansAsync(individualProviderID);
        }


        public async Task<IEnumerable<Plan>> GetAllNonInitiatedPlansForProviderAsync(int individualProviderID)
        {
            Individual ip = await credentialingRepository.FindAsync(individualProviderID);
            var initiatedPlans = from ch in ip.CredentialingHistory
                                 select ch.Plan;
            var allPlans = await plansRepository.GetAllAsync();

            
            return allPlans.Except(initiatedPlans.ToList(),new PlanComparer());
        }

        public async Task<IEnumerable<Plan>> GetAllInitiatedPlansForProviderAsync(int individualProviderID)
        {
            Individual ip = await credentialingRepository.FindAsync(individualProviderID);
            var initiatedPlans = from ch in ip.CredentialingHistory
                                 select ch.Plan;

            return initiatedPlans;
        }


        public async Task<IEnumerable<Entities.Credentialing.Plan>> GetAllNonCredentialedIndividualPlansAsync(int individualProviderID)
        {
            var credentialedIndividualPlans = await credentialingRepository.GetAllCredentialedIndividualPlansAsync(individualProviderID);
            var credentialedPlans = from credentialedplan in credentialedIndividualPlans
                        select credentialedplan.Plan;

            return plansRepository.GetAll().Except(credentialedPlans, new PlanComparer());
        }

        /// <summary>
        /// Associate Plans to Individual Provider and Initiate the Credentialing Process
        /// </summary>
        /// <param name="credentialingDetailsDTO"></param>
        /// <returns></returns>
        public async Task<int> InitiateCredentialingAsync(CredentialingDetailsDTO credentialingDetailsDTO)
        {
            
            // Get the Provider
            Individual individualProvider = credentialingRepository.Find(credentialingDetailsDTO.ProviderID);

            // Associate Credentialing Plans to Individual Provider along with Credentialing Log

            foreach (Plan plan in credentialingDetailsDTO.credentialingPlans)
            {
                CredentialingInfo credentialingInfo = new CredentialingInfo();
                credentialingInfo.PlanID = plan.PlanID;
                credentialingInfo.Remarks = credentialingDetailsDTO.Remarks;
                credentialingInfo.CredentialingStatus = CredentialingStatus.Initiated;
                credentialingInfo.CredentialingType = CredentialingType.Credentialing;
                // Associate Log details to Credentialing info
                
                CredentialingLog log = new CredentialingLog();
                log.CredentialingStatus = CredentialingStatus.Initiated;
                log.DoneBy = credentialingDetailsDTO.CredentialedBy;
                credentialingInfo.CredentialingLogs.Add(log);

                // Associate Credentialing History to Individual Provider
                individualProvider.CredentialingHistory.Add(credentialingInfo);
            }
            
            // Create and Save Provider Credentials
            credentialingRepository.Update(individualProvider);
            return await credentialingRepository.SaveAsync();
        }
    }


    class PlanComparer : IEqualityComparer<Plan>
    {
        public bool Equals(Plan x, Plan y)
        {
            return x.PlanID == y.PlanID;
        }

        public int GetHashCode(Plan obj)
        {
            unchecked
            {
                if (obj == null)
                    return 0;
                int hashCode = obj.PlanID.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.PlanID.GetHashCode();
                return hashCode;
            }

        }
    }
}
