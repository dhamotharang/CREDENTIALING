using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class ChangeNotificationDetail
    {
        public ChangeNotificationDetail()
        {

        }
        public ChangeNotificationDetail(int profileID, string actionPerformedUser, string sectionName, string actionPerformed)
        {
            DateTime = DateTime.Now;
            this.ProfileID = profileID;
            this.ActionPerformedUser = actionPerformedUser;
            
            this.ActionPerformed = actionPerformed;
            this.SectionName = sectionName;
        }

        public int ChangeNotificationDetailID { get; set; }
        public string NPINumber { get; set; }
        public string ActionPerformedUser { get; set; }
        public string ProviderFullName { get; set; }
        public int ProfileID { get; set; }
        public string ProviderEmailID { get; set; }
        //public List<NotificationInfo> NotificationInformations { get; set; }
        public string SectionName { get; set; }
        public string ActionPerformed { get; set; }
        public DateTime DateTime { get; set; }
    }
}
