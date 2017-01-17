using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    public interface IExpiryNotificationRepository : IGenericRepository<ExpiryNotificationDetail>
    {
        void UpdateExpiryDetails(IEnumerable<ExpiryNotificationDetail> expiries);
    }
}
