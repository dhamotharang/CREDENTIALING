using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Account
{
    public interface IOrganizationManager
    {
        Task AddOrganizationAsync(Organization organization);        

        Task<IEnumerable<Organization>> GetAllOrganizationAsync(bool onlyActiveRecords = true);
        Task<IEnumerable<Organization>> GetAllOrganizationWithLocationAsync(bool onlyActiveRecords = true);
        Task<IEnumerable<Organization>> GetAllOrganizationWithLocationDetailAsync(bool onlyActiveRecords = true);
        Task<IEnumerable<PracticingGroup>> GetAllPracticingGroupsAsync(int organizationId,bool onlyActiveRecords = true);
        Task<IEnumerable<PracticingGroup>> GetAllMidLevelsByOrgId(int organizationId,bool onlyActiveRecords = true);

        
        Task<IEnumerable<PracticingGroup>> GetGroupsAsync(int organizationId);

        #region Checked

        Task AddFacilityAsync(int organizationId, Facility facility);
        Task UpdateFacilityAsync(int organizationId, Facility facility);
        
        #endregion
    }
}
