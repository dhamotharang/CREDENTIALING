using AHC.CD.Data.EFRepository.ContractGrid;
using AHC.CD.Data.EFRepository.MasterDataEFRepo;
using AHC.CD.Data.EFRepository.Notification;
using AHC.CD.Data.EFRepository.Profiles;
using AHC.CD.Data.EFRepository.TaskTracker;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.MasterDataRepo;
using AHC.CD.Entities.Credentialing;
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

        public Repository.Profiles.IProfileRepository GetProfileRepository()
        {
            return new ProfileEFRepository();
        }

        public Repository.Profiles.IContactDetailRepository GetContactDetailRepository()
        {
            return new ContactDetailEFRepository();
        }

        public AHC.CD.Data.Repository.IRepository GetRepository()
        {
            return new GenericRepository();
        }

        public IUserRepository GetUserRepository()
        {
            return new UserRepository();
        }

        public IGenericRepository<T> GetGenericRepository<T>() where T : class
        {
            return new EFGenericRepository<T>();
        }

        public Repository.Profiles.IPracticeLocationRepository GetPracticeLocationRepository()
        {
            return new PracticeLocationRepository();
        }

        public IExpiryNotificationRepository GetExpiryNotificationRepository()
        {
            return new ExpiryNotificationRepository();
        }


        public Repository.MasterDataRepo.IMilitaryRankRepository GetMilitaryRankRepository()
        {
            return new MilitaryRankEFRepository();
        }

        public IPlansRepository GetPlanRepository()
        {
            return new PlansEFRepository();
        }


        public ICredentialingRequestRepository GetCredentialingrequestRepository()
        {
            return new CredentialingRequestEFRepository();
        }


        public Repository.TaskTracker.ITaskTrackerRepository GetTaskTrackerRepository()
        {
            return new TaskTrackerEFRepository();
        }

        public ITaskTrackerExpiryRepository GetTaskTrackerExpiryRepository()
        {
            return new TaskTrackerExpiryRepository();
        }

        public Repository.ContractGrid.IContractGridRepository GetContractGridRepository()
        {
            return new ContractGridRepository();
        }
        public INotesTemplateRepository GetNotesTemplateRepository()
        {
            return new NotesTemplateEFRepository();
        }
    }
}
