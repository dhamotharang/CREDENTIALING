using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Users
{
    class CDRoleManager : ICDRoleManager
    {
        public CDRoleManager(IUnitOfWork uow)
        {
            this.cdRoleRepository = uow.GetGenericRepository<CDRole>();
        }

        IGenericRepository<CDRole> cdRoleRepository = null;
        
        public async Task AssignProviderRoleAsync(CDUser cdUser)
        {
            var providerRole = cdRoleRepository.Find(r => r.Code.Equals("PRO"), "CDUSers");
            if(providerRole == null)
            {
                providerRole = new CDRole() { Code = "PRO", Name = "Provider", StatusType = StatusType.Active };
                cdRoleRepository.Create(providerRole);
                await cdRoleRepository.SaveAsync();
            }

            if(providerRole.CDUsers == null)
            {
                providerRole.CDUsers = new List<CDUserRole>();
            }

            CDUserRole userRole = new CDUserRole() { CDUserId = cdUser.CDUserID, CDRoleId = providerRole.CDRoleID };

            providerRole.CDUsers.Add(userRole);
            cdRoleRepository.Update(providerRole);
            await cdRoleRepository.SaveAsync();
        }
    }
}
