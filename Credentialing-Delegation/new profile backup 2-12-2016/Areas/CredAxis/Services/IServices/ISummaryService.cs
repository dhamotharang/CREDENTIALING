using PortalTemplate.Areas.CredAxis.Models.SummaryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
    interface ISummaryService
    {
        SummaryMainViewModel GetSummary();
    }
}
