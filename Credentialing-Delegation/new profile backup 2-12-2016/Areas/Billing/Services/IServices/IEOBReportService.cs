using PortalTemplate.Areas.Billing.Models.EOB_Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Services.IServices
{
    public interface IEOBReportService
    {
        List<EOBReportRenderingProviderByDateOfServiceViewModel> GetEobReportOfRenderingProviderByDos();

        List<EOBReportRenderingProviderByPaymentDateViewModel> GetEobReportOfRenderingProviderByPaymentDate();

        List<EOBReportServiceLocationByDateOfServiceViewModel> GetEobReportOfServiceLocationByDos();

        List<EOBReportServiceLocationByPaymentDateViewModel> GetEobReportOfServiceLocationByPaymentDate();
    }
}
