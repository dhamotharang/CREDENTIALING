using AHC.CD.Data.Repository;
using AHC.CD.Entities.Credentialing.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.CnD
{
    internal class ApplicationManager : IApplicationManager
    {
        private IUnitOfWork uow = null;

        public ApplicationManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<CredentialingInfo> GetCredentialingInfoByIdAsync(int credInfo)
        {
            try
            {
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                CredentialingInfo resultSet = await credInfoRepo.FindAsync(c => c.CredentialingInfoID == credInfo, "Plan, Profile, Profile.SpecialtyDetails.Specialty, Profile.PracticeLocationDetails.PracticeProviders, Profile.PersonalDetail.ProviderTitles.ProviderType");
                return resultSet;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
