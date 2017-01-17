using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    /// <summary>
    /// Author: Venkat
    /// Date:   20/10/2014
    /// 
    /// Keeps all Repositories here, 
    /// Need to add contract methods for each repository
    /// </summary>
    public interface IUnitOfWork
    {
        IProvidersRepository GetProvidersRepository();

        IGenericRepository<ProviderCategory> GetProviderCategory();

        IGenericRepository<Entities.ProfileDemographicInfo.AddressInfo> GetProviderAddressRepository();

        IGenericRepository<Group> GetGroupsRepository();

        IGenericRepository<ProviderType> GetProviderTypeRepository();

        IGenericRepository<Document> GetDocumentsRepository();

        IGenericRepository<DocumentCategory> GetDocumentsCategoryRepository();

        IGenericRepository<DocumentType> GetDocumentsTypeRepository();

        ICredentialingRepository GetCredentialingRepository();


        IGenericRepository<InsuranceCompany> GetInsuranceCompaniesRepository();

        IPlansRepository GetPlansRepository();

        IGenericRepository<Entities.ProfileDemographicInfo.PersonalInfo> GetPersonalInfoRepository();

        IGenericRepository<Entities.ProfileDemographicInfo.ContactInfo> GetContactInfoRepository();
    }
}
