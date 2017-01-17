using AHC.CD.Data.Repository;
using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository.Notification
{
    internal class TaskTrackerExpiryRepository : EFGenericRepository<TaskTrackerExpiry>, ITaskTrackerExpiryRepository
    {
        public void SaveAllExpiries(List<TaskTrackerExpiry> tasks)
        {
            
            this.CreateRange(tasks);
            this.Save();
            
        }
        public void DeleteAllExpiries(List<TaskTrackerExpiry> tasks)
        {
            foreach(var task in tasks)
            {
                this.Delete(task);
               
            }
            this.Save();
        }
    }
}
