using PortalTemplate.Areas.Billing.Models.PayerDrilldownReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface IPayerDrilldownReportService
    {
        CPayerReportViewModel GetPayerClaimsCountReport();

        APayerReportViewModel GetPayerClaimsAmountReport();

        CMemberReportViewModel GetMemberClaimsCountReport(int PayerId, int BillingProviderId, int RenderingProviderId);

        AMemberReportViewModel GetMemberClaimsAmountReport(int PayerId, int BillingProviderId, int RenderingProviderId);

        CRenderingProviderReportViewModel GetRenderingProviderCountReport(int PayerId, int BillingProviderId);

        ARenderingProviderReportViewModel GetRenderingProviderAmountReport(int PayerId, int BillingProviderId);

        CBillingProviderReportViewModel GetBillingClaimsCountReport(int PayerId);

        ABillingProviderReportViewModel GetBillingClaimsAmountReport(int PayerId);


    }
}
