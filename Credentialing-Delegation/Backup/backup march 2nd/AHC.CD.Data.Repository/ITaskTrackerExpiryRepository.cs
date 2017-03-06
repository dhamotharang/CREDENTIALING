using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    public interface ITaskTrackerExpiryRepository : IGenericRepository<TaskTrackerExpiry>
    {
         void SaveAllExpiries(List<TaskTrackerExpiry> tasks);
         void DeleteAllExpiries(List<TaskTrackerExpiry> tasks);
    }
}
