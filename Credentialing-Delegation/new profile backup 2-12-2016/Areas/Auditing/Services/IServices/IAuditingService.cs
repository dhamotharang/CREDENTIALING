using PortalTemplate.Areas.Auditing.Models.AuditingList;
using PortalTemplate.Areas.Auditing.Models.CreateAuditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Auditing.Services.IServices
{
    public interface IAuditingService
    {
        List<CoderQueueViewModel> GetCoderList();
        List<CommitteeQueueViewModel> GetCommitteeList();
        List<ProviderQueueViewModel> GetProviderList();
        List<QCQueueViewModel> GetQCList();
        List<InactiveQueueViewModel> GetInactiveList();
        List<ReadytoBillViewModel> GetRBAuditingList();
        List<ReadytoBillViewModel> GetDraftAuditingList();
        AuditingQueuesCountViewModel GetAuditingListStatusCount();
        CreateAuditingViewModel ViewAuditing();
        CreateAuditingViewModel EditAuditing();
        List<CategoryViewModel> GetAllCategories();
    }
}
