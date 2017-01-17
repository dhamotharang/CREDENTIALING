using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.DTO
{
   public class QueueUsers
    {
        public String QueueType {get;set;}
        public List<UserData> User { get; set; }

        public  QueueUsers()
        {
            User = new List<UserData>();
        }
    }
}
