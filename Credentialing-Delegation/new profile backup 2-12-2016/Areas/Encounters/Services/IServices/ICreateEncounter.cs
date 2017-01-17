using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Models.CreateEncounter;
using PortalTemplate.Areas.Encounters.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Encounters.Services.IServices
{
    public interface ICreateEncounter
    {
        List<ProviderViewModel> GetProviderResultList(string ProviderSearchParameter);
        List<MemberViewModel> GetMemberResultList();
        PrimaryEncounterViewModel GetEncounterDetails();
        PrimaryEncounterViewModel GetEncounterDetailsForMember();
        CodeDetailsViewModel GetCodingDetails(PrimaryEncounterViewModel EncounterDetails);
        AuditingViewModel GetAuditingDetails(EncounterCodingDetailsViewModel CodeDetails);
        List<ICDCodeViewModel> GetActiveDiagnosisCode();
        List<ActiveProcedureCodeViewModel> GetActiveProcedureCode();
        List<DocumentHistoryViewModel> GetDocumentHistory();
        List<ReferingProviderListViewModel> GetReferingProviderList();
        List<BillingProviderListViewModel> GetBillingProviderList();
        List<RenderingProviderListViewModel> GetRenderingProviderList();
        List<FacilityListViewModel> GetFacilityList();
        List<ClaimHistoryViewModel> GetClaimsHistory();
        ProviderSelectMemberViewModel GerProviderSelectMembers(string ProviderId);
    }
}
