using PortalTemplate.Areas.Billing.Models.PayerDrilldownReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface IProviderDrilldownReportService
    {
        CPayerReportViewModel GetPayerClaimsCountReport(int BillingProviderId, int RenderingProviderId);

        APayerReportViewModel GetPayerClaimsAmountReport(int BillingProviderId, int RenderingProviderId);

        CMemberReportViewModel GetMemberClaimsCountReport(int PayerId, int BillingProviderId, int RenderingProviderId);

        AMemberReportViewModel GetMemberClaimsAmountReport(int PayerId, int BillingProviderId, int RenderingProviderId);

        CRenderingProviderReportViewModel GetRenderingProviderCountReport();

        ARenderingProviderReportViewModel GetRenderingProviderAmountReport();

        CBillingProviderReportViewModel GetBillingClaimsCountReport(int RenderingProviderId);

        ABillingProviderReportViewModel GetBillingClaimsAmountReport(int RenderingProviderId);

    }
}
