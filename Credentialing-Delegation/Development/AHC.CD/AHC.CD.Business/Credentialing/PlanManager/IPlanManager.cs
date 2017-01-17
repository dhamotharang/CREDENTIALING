using AHC.CD.Entities.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.PlanManager
{
    public interface IPlanManager
    {
        Task UpdatePlanAsync(Plan plan);

        Task<int> AddPlanAsync(Plan plan);

        Task PlanBEMapping(int PlanId, int BeId);

        Task<List<int>> AddSubPlans(List<SubPlan> SubPlans);
        void UpdateSubPlans(List<SubPlan> SubPlans);

        Task PlanLobSubPlanMapping(int PlanId, int LobId, List<SubPlan> SubPlan, List<LOBAddressDetail> addresses, List<LOBContactDetail> contacts);

        List<LOB> GetAllLobForPlan(int PlanId);

        Task LobBemapping(PlanContract PlanContract);

        Task RemovePlanAsync(int planid);
        List<PlanLOB> GetPlanLobForPlan(int planlId);

        void AddPlanContracts(List<PlanContract> planContracts);
        void UpdatePlanContracts(List<PlanContract> planContracts);

        List<PlanContract> GetPlanContractForPlan(int planlId);

        bool IsPlanNameExist(string planName);

        void UpdatePlanContactDetail(List<LOBContactDetail> planContacts);
        void UpdatePlanAddress(List<LOBAddressDetail> planAddresses);

        void UpdatePlanContactForPlan(List<PlanContactDetail> planContactDetails);
        void UpdatePlanAddressForPlan(List<PlanAddress> planAddressDetails);

        Task<object> GetPlanDetail(int planId);
    }
}
