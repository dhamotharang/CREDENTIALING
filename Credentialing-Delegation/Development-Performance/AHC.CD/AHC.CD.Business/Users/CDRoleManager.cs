using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.UserInfo;
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
            this.userrolerepo = uow.GetGenericRepository<CDUserRole>();
            this.userRepository = uow.GetGenericRepository<CDUser>();
        }


        IGenericRepository<CDRole> cdRoleRepository = null;
        IGenericRepository<CDUser> userRepository = null;
        IGenericRepository<CDUserRole> userrolerepo = null;
        

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
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AssignRoleForUser(CDUser cDUser, int id)
        {
            try
            {
                var ccoRole = cdRoleRepository.Find(r => r.CDRoleID == id, "CDUSers");
                if (ccoRole.CDUsers == null)
                {
                    ccoRole.CDUsers = new List<CDUserRole>();
                }

                CDUserRole userRole = new CDUserRole() { CDUserId = cDUser.CDUserID, CDRoleId = ccoRole.CDRoleID };

                ccoRole.CDUsers.Add(userRole);
                cdRoleRepository.Update(ccoRole);
                await cdRoleRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
            
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

        //Method to Change Role of a User in Profile DB

        public void ChangeRoleofaUser(string newRoleCode, string authId, string oldrole)
        {
            try
            {
                var oldroleid = cdRoleRepository.Find(c => c.Code == oldrole).CDRoleID;
                var newroleid = cdRoleRepository.Find(g => g.Code == newRoleCode).CDRoleID;
                //var cduserid = userRepository.Find(x => x.AuthenicateUserId == authId).CDUserID;
                var cduserid1 = userRepository.Find(x => x.AuthenicateUserId == authId);

                //var userrolerepo = unitOfWork.GetGenericRepository<CDUserRole>();
                var requesteduserrole = userrolerepo.Find(v => v.CDUserId == cduserid1.CDUserID && v.CDRoleId == oldroleid);
                if(requesteduserrole == null)
                {
                    CDUserRole userrole = new CDUserRole{CDUserId = cduserid1.CDUserID,CDRoleId = newroleid,LastModifiedDate = DateTime.Now};
                    userrolerepo.Create(userrole);
                    userrolerepo.Save();
                }
                requesteduserrole.CDRoleId = newroleid;
                userRepository.Update(cduserid1);
                userrolerepo.Update(requesteduserrole);
                userrolerepo.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool RemoveRoleofaUser(string RoleCode, string authId)
        {
            try
            {
                
                var newroleid = cdRoleRepository.Find(g => g.Code == RoleCode).CDRoleID;
                var cduserid1 = userRepository.Find(x => x.AuthenicateUserId == authId);
                CDUserRole userrole = new CDUserRole();
                if (cduserid1.CDRoles.Any(a => a.CDRoleId == newroleid))
                {
                    userrole = userrolerepo.Find(x => x.CDRoleId == newroleid && x.CDUserId == cduserid1.CDUserID);
                    userrolerepo.Delete(userrole.CDUserRoleID);
                    userrolerepo.Save();
                    return true;
                }
                else if(!cduserid1.CDRoles.Any(a => a.CDRoleId == newroleid))
                {
                    return true;
                }
                else { return false; }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AddNewRoletoaUser(string RoleCode, string authId)
        {
            try
            {

                var newroleid = cdRoleRepository.Find(g => g.Code == RoleCode).CDRoleID;
                CDUser cduserid1 = new CDUser();
                cduserid1 =  await userRepository.FindAsync(x => x.AuthenicateUserId == authId);
                CDUserRole userrole = new CDUserRole { CDRoleId = newroleid, CDUserId = cduserid1.CDUserID, LastModifiedDate = DateTime.Now };
                if (!cduserid1.CDRoles.Any(a => a.CDRoleId == newroleid))
                {
                    cduserid1.CDRoles.Add(userrole);
                    userrolerepo.Create(userrole);
                    userrolerepo.Save();
                    return true;
                }
                else { return false; }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
