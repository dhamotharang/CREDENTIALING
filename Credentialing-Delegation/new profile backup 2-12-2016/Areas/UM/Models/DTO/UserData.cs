using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.DTO
{
    public class UserData
    {
        public String UserName {get; set; }
        public String UserRole { get; set; }
        public String ImagePath { get; set; }
        public int? StandardCount { get; set; }
        public int? ExpediteCount { get; set; }
    }
}
