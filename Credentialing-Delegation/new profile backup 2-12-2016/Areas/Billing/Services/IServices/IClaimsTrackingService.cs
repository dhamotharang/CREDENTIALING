using PortalTemplate.Areas.Billing.Models.Claims_Tracking;
using PortalTemplate.Areas.Billing.Models.CreateClaim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface IClaimsTrackingService
    {
        List<OpenClaimViewModel> GetOpenClaimsList();

        List<OnHoldClaimViewModel> GetOnHoldClaimsList();

        List<RejectedClaimViewModel> GetRejectedClaimsList();

        List<CommitteeReviewClaimViewModel> GetComitteeReviewListClaimsList();

        List<PhysicianOnHoldClaimViewModel> GetPhysicianOnHoldListClaimsList();

        List<AcceptedClaimViewModel> GetAcceptedClaimsList();

        List<DispatchedClaimViewModel> GetDispatchedClaimsList();

        List<UnAckByCHClaimViewModel> GetUnAckByCHClaimsList();

        List<RejectedByCHClaimViewModel> GetRejectedByCHClaimsList();

        List<AcceptedByCHClaimViewModel> GetAcceptedByCHClaimsList();

        List<UnAckByPayerClaimViewModel> GetUnackByPayerClaimsList();

        List<RejectedByPayerClaimViewModel> GetRejectedByPayerClaimsList();

        List<AcceptedByPayerClaimViewModel> GetAcceptedByPayerClaimsList();

        List<PendingClaimViewModel> GetPendingClaimsList();

        List<DeniedByPayerClaimViewModel> GetDeniedByPayerClaimsList();

        List<EobReceivedClaimViewModel> GetEOBReceivedClaimsList();

        Cms1500ViewModels GetCms1500Form(int ClaimId);

        Cms1500ViewModels ViewCms1500Form(int ClaimId);

        Cms1500ViewModels GetCms1500FormInstance(int ClaimId);

        List<EobReport>  GetEobReport(int ClaimId);
    }
}
