using PortalTemplate.Areas.Billing.Models.CMS1500.New;
using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info;
using PortalTemplate.Areas.Billing.Models.CreateClaim.CreateClaimTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface ICreateClaimsService
    {
        ClaimInfo GetClaimInfo(MemberRelatedId ids);

        List<MemberResultViewModel> GetMemberResult(string SubscriberID, string MemberName);

        MemberResultViewModel GetSelectedMemberResult(string MemberId);

        List<ProviderResultViewModel> GetProviderResult(string ProviderName, string ProviderNPI);

        List<MemberResultViewModel> GetSelectedProviderResult(List<string> ProviderId);

        List<MemberResultViewModel> GetMemberListForProviderResult();

        MemberResultViewModel GetSelectedMemberForProviderResult(string MemberId);

        BillingInfo GetCms1500Form(ClaimsInfoViewModels ClaimsInfo);

        CreateClaimsTemplate GetCreateClaimTemplate(string MemberId);

        CreateClaimsTemplate GetCreateClaimTemplateForMember(string MemberId);

        List<ProviderResult> GetBillingProviderResult(ProviderResult SearchResult);

        List<ProviderResult> GetRenderingProviderResult(ProviderResult SearchResult);

        List<ProviderResult> GetSupervisingProviderResult(ProviderResult SearchResult);

        List<ProviderResult> GetReferringProviderResult(ProviderResult SearchResult);

        List<FacilityResult> GetFacilityResult();

        List<CodeHistory> GetIcdHistory();

        List<MedicalReview> GetMedicalReview();

        bool SaveCMS1500Form(BillingInfo BillingInfo);

        List<State> GetAllStates(bool IncludedInactive);

        BillingInfo MapMemberProviderToBillingInfo(CreateClaimsTemplate createClaimsTemplate);

        List<CPTCodeViewModel> GetCPTCodeHistory();

        BillingProviderInfo GetSelectedBillingProvider(string ProviderId);

        ProviderResultViewModel GetSelectedRenderingProvider(string ProviderId);

        ReferingProviderInfo GetSelectedReferringProvider(string ProviderId);

        SupervisingProviderInfo GetSelectedSupervisingProvider(string ProviderId);

    }
}
