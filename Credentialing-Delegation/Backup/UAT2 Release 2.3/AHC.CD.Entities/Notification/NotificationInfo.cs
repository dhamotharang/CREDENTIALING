using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class NotificationInfo
    {
        public NotificationInfo()
        {
            DateTime = DateTime.Now;
        }
        public int NotificationInfoID { get; set; }
        public string SectionName { get; set; }
        public ActionPerformed ActionPerformed { get; set; }
        public DateTime DateTime { get; set; }
    }
}
