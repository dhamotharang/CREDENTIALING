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


namespace AHC.CD.Business.Credentialing
{
    internal class IndividualCredentialingManager : IIndividualCredentialingManager
    {
        IGenericRepository<InsuranceCompany> insuranceCompaniesRepository = null;
        IPlansRepository plansRepository = null;
        ICredentialingRepository credentialingRepository = null;
       
        
        public IndividualCredentialingManager(IUnitOfWork uow)
        {
            //insuranceCompaniesRepository = uow.GetInsuranceCompaniesRepository();
            //plansRepository = uow.GetPlansRepository();
            //credentialingRepository = uow.GetCredentialingRepository();
        }
        
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentialingDetailsDTO"></param>
        /// <returns></returns>
        public async Task<int> InitiateCredentialingAsync(CredentialingDetailsDTO credentialingDetailsDTO)
        {
            throw new NotImplementedException();
            //// Get the Provider
            //IndividualProvider individualProvider = credentialingRepository.Find(credentialingDetailsDTO.ProviderID);

            //// Associate Credentialing Plans to Individual Provider along with Credentialing Log

            //foreach (Plan plan in credentialingDetailsDTO.credentialingPlans)
            //{
            //    CredentialingInfo credentialingInfo = new CredentialingInfo();
            //    credentialingInfo.PlanID = plan.PlanID;
            //    credentialingInfo.Remarks = credentialingDetailsDTO.Remarks;
            //    credentialingInfo.CredentialingStatus = CredentialingStatus.Initiated;
            //    credentialingInfo.CredentialingType = CredentialingType.Credentialing;
            //    // Associate Log details to Credentialing info
                
            //    CredentialingLog log = new CredentialingLog();
            //    log.CredentialingStatus = CredentialingStatus.Initiated;
            //    log.DoneBy = credentialingDetailsDTO.CredentialedBy;
            //    credentialingInfo.CredentialingLogs.Add(log);

            //    // Associate Credentialing History to Individual Provider
            //    //individualProvider.CredentialingHistory.Add(credentialingInfo);
            //}
            
            //// Create and Save Provider Credentials
            //credentialingRepository.Update(individualProvider);
            //return await credentialingRepository.SaveAsync();
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
