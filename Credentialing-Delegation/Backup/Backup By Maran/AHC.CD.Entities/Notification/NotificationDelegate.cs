using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class NotificationDelegate
    {
        public delegate void DependencyDelegate(object sender, SqlNotificationEventArgs e);
    }
}
