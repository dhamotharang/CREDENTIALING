using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business
{
    public interface IIndividualCredentialingManager
    {
        Task<IEnumerable<Plan>> GetAllPlansAsync();
        Task<IEnumerable<InsuranceCompany>> GetAllInsuranceCompaniesAsync();
        Task<IEnumerable<Plan>> GetAllPlansForInsuranceCompanyAsync(int insuranceCompanyID);
        Task<IEnumerable<IndividualPlan>> GetAllCredentialedIndividualPlansAsync(int individualProviderID);
        Task<IEnumerable<Plan>> GetAllNonCredentialedIndividualPlansAsync(int individualProviderID);
        Task<int> InitiateCredentialingAsync(CredentialingDetailsDTO credentialingDetailsDTO);
        Task<IEnumerable<Plan>> GetAllNonInitiatedPlansForProviderAsync(int individualProviderID);
        Task<IEnumerable<Plan>> GetAllInitiatedPlansForProviderAsync(int individualProviderID);

    }
}
