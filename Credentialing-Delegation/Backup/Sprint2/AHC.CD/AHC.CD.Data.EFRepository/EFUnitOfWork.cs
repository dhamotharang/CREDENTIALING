using AHC.CD.Data.Repository;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.ProfileDemographicInfo;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    /// <summary>
    /// Author: Venkat
    /// Date:   20/10/2014
    /// All repositories should access through this class
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// this is just a sample implementation
        /// </summary>
        /// <returns></returns>
      

        public IProvidersRepository GetProvidersRepository()
        {
            return new ProvidersEFRepository();
        }


        public IGenericRepository<Entities.ProviderInfo.ProviderCategory> GetProviderCategory()
        {
            return new EFGenericRepository<ProviderCategory>();
        }


        public IGenericRepository<Entities.ProfileDemographicInfo.AddressInfo> GetProviderAddressRepository()
        {
            return new EFGenericRepository<AddressInfo>();
        }


        public IGenericRepository<Group> GetGroupsRepository()
        {
            return new EFGenericRepository<Group>();
        }


        public IGenericRepository<ProviderType> GetProviderTypeRepository()
        {
            return new EFGenericRepository<ProviderType>();
        }


        public IGenericRepository<Document> GetDocumentsRepository()
        {
            return new EFGenericRepository<Document>();
        }

        public IGenericRepository<DocumentCategory> GetDocumentsCategoryRepository()
        {
            return new EFGenericRepository<DocumentCategory>();
        }


        public IGenericRepository<DocumentType> GetDocumentsTypeRepository()
        {
            return new EFGenericRepository<DocumentType>();
        }


        public ICredentialingRepository GetCredentialingRepository()
        {
            return new IndividualCredentialingEFRepository();
        }


        public IGenericRepository<Entities.Credentialing.InsuranceCompany> GetInsuranceCompaniesRepository()
        {
            return new EFGenericRepository<InsuranceCompany>();
        }

        public IPlansRepository GetPlansRepository()
        {
            return new PlansEFRepository();
        }


        public IGenericRepository<PersonalInfo> GetPersonalInfoRepository()
        {
            return new EFGenericRepository<PersonalInfo>();
        }


        public IGenericRepository<ContactInfo> GetContactInfoRepository()
        {
            return new EFGenericRepository<ContactInfo>();
        }
    }
}
