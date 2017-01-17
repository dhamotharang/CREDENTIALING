using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.DTO
{
    public class UserDTO
    {
        public List<QueueUsers> QueueUsers { get; set; }

        public UserDTO()
        {
            QueueUsers = new List<QueueUsers>();
        }
    }
}