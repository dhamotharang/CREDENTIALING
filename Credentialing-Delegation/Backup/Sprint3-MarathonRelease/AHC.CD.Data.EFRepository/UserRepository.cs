using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Data.EFRepository
{
    internal class UserRepository : EFGenericRepository<User>, IUserRepository
    {
        
    }
}
