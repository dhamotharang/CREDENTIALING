using AHC.CD.Data.Repository;
using AHC.CD.Entities.Credentialing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System;
using AHC.CD.Entities.MasterData.Tables;

namespace AHC.CD.Data.EFRepository
{
    internal class PlansEFRepository : EFGenericRepository<Plan>, IPlansRepository
    {
        public async Task<Plan> GetPlanByIdAsync(int PlanId)
        {
            try 
            {
                //var result = this.Find(x => x.PlanID == PlanId, "ContactDetails,ContactDetails.ContactDetail,Locations,PlanLOBs,PlanLOBs.LOB,PlanLOBs.SubPlans,PlanLOBs.LOBContactDetails, PlanLOBs.LOBContactDetails.ContactDetail,PlanLOBs.LOBContactDetails.ContactDetail.PhoneDetails,PlanLOBs.LOBContactDetails.ContactDetail.EmailIDs,PlanLOBs.LOBContactDetails.ContactDetail.PreferredContacts,PlanLOBs.LOBAddressDetails");
                //result.PlanLOBs = result.PlanLOBs.Where(x => x.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();
                return await this.FindAsync(x => x.PlanID == PlanId, "ContactDetails,ContactDetails.ContactDetail,Locations,PlanLOBs,PlanLOBs.LOB,PlanLOBs.SubPlans,PlanLOBs.LOBContactDetails, PlanLOBs.LOBContactDetails.ContactDetail,PlanLOBs.LOBContactDetails.ContactDetail.PhoneDetails,PlanLOBs.LOBContactDetails.ContactDetail.EmailIDs,PlanLOBs.LOBContactDetails.ContactDetail.PreferredContacts,PlanLOBs.LOBAddressDetails");
                //return result;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
        public async Task<int> RemovePlanByIdAsync(int PlanID)
        {
            try 
            {
                Plan Plan = await this.FindAsync(x => x.PlanID == PlanID);
                Plan.StatusType=AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                this.Update(Plan);
                this.Save();
                return Plan.PlanID;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }


        public async Task<int> ReactivePlanByIdAsync(int PlanID)
        {
            try 
            {
                Plan Plan = await this.FindAsync(x => x.PlanID == PlanID);
                Plan.StatusType=AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                this.Update(Plan);
                this.Save();
                return Plan.PlanID;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
