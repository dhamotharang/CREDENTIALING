using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models
{
    public class UsersList
    {
        public string AuthenticateUserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int NumberofRoles { get; set; }
        public IList<string> RoleList { get; set; }
    }
}