using AHC.CD.Data.Repository.MasterDataRepo;
using AHC.CD.Data.Repository.Profiles;
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
        Profiles.IProfileRepository GetProfileRepository();
        Profiles.IContactDetailRepository GetContactDetailRepository();
        IRepository GetRepository();
        IUserRepository GetUserRepository();

        IGenericRepository<T> GetGenericRepository<T>() where T : class;
        IPracticeLocationRepository GetPracticeLocationRepository();
        IExpiryNotificationRepository GetExpiryNotificationRepository();
        IMilitaryRankRepository GetMilitaryRankRepository();
        IPlansRepository GetPlanRepository();
        ICredentialingRequestRepository GetCredentialingrequestRepository();    
    }
}
