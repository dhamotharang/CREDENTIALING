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
    public class CDRoleManager : ICDRoleManager
    {
        public CDRoleManager(IUnitOfWork uow)
        {
            this.cdRoleRepository = uow.GetGenericRepository<CDRole>();
        }

        IGenericRepository<CDRole> cdRoleRepository = null;

        public async Task AssignProviderRoleAsync(CDUser cdUser)
        {
            var providerRole = cdRoleRepository.Find(r => r.Code.Equals("PRO"), "CDUSers");
            if (providerRole == null)
            {
                providerRole = new CDRole() { Code = "PRO", Name = "Provider", StatusType = StatusType.Active };
                cdRoleRepository.Create(providerRole);
                await cdRoleRepository.SaveAsync();
            }

            await AssignRole(cdUser, providerRole);
        }

        public async Task AssignCCORoleAsync(CDUser cDUser)
        {
            var ccoRole = cdRoleRepository.Find(r => r.Code.Equals("CCO"), "CDUSers");
            if (ccoRole == null)
            {
                ccoRole = new CDRole() { Code = "CCO", Name = "Credential Coordinator", StatusType = StatusType.Active };
                cdRoleRepository.Create(ccoRole);
                await cdRoleRepository.SaveAsync();
            }

            await AssignRole(cDUser, ccoRole);
        }

        private async Task AssignRole(CDUser cDUser, CDRole cdRole)
        {
            if (cdRole.CDUsers == null)
            {
                cdRole.CDUsers = new List<CDUserRole>();
            }

            CDUserRole userRole = new CDUserRole() { CDUserId = cDUser.CDUserID, CDRoleId = cdRole.CDRoleID };

            cdRole.CDUsers.Add(userRole);
            cdRoleRepository.Update(cdRole);
            await cdRoleRepository.SaveAsync();
        }

        public async Task AssignTeamLeadRoleAsync(CDUser cDUser)
        {
            var tlRole = cdRoleRepository.Find(r => r.Code.Equals("TL"), "CDUSers");
            if (tlRole == null)
            {
                tlRole = new CDRole() { Code = "TL", Name = "Team Lead", StatusType = StatusType.Active };
                cdRoleRepository.Create(tlRole);
                await cdRoleRepository.SaveAsync();
            }

            await AssignRole(cDUser, tlRole);
        }
    }
}
