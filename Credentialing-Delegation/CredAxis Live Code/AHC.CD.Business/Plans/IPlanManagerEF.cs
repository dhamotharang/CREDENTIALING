using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.DTO;
using AHC.CD.Data.ADO.DTO.Plan;
using AHC.CD.Entities.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Plans
{
    public interface IPlanManagerEF
    {
        Task<PlanDataDTO> getPlanDataByID(int PlanId);
        Task<int> removePlanDataByID(int PlanId);
        Task<int> reactivePlanDataByID(int PlanId);
        Task<PlanMasterDataDTO> getMasterDataForPlan();
        void addPlanContracts(List<PlanContract> PlanContracts);
        void updatePlanContracts(List<PlanContract> PlanContracts);
        Task<List<PlanContract>> getAllPlanContractsByID(int PlanID);
        List<PlanContract> getAllPlanContractsByIDForCotractgrid(int PlanID);
        Task<object> AddPlanData(Plan Plan, DocumentDTO AttachDocument);
        Task<object> UpdatePlanData(Plan Plan, DocumentDTO AttachDocument);
        Task<object> GetPlanContarctDataByID(int PlanID);
        void UpdateSubPlans(List<SubPlan> SubPlans, int? PlanLOBID);
        void UpdatePlanContactDetail(List<LOBContactDetail> planContacts,int? PlanLOBID);
        void UpdatePlanAddress(List<LOBAddressDetail> planAddresses,int? PlanLOBID);
        void UpdatePlanContactForPlan(List<PlanContactDetail> planContactDetails);
        void UpdatePlanAddressForPlan(List<PlanAddress> planAddressDetails);
        void AddRengeOFPlanLOBsData(List<PlanLOB> PlanLOBsData);
    }
}
