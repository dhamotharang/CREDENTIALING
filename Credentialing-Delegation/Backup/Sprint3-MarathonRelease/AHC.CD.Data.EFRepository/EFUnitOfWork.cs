using AHC.CD.Data.EFRepository.Profiles;
using AHC.CD.Data.Repository;
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
    }
}
