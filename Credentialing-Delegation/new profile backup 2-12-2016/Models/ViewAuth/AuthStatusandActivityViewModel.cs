using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PortalTemplate.Models.ViewAuth
{
    public class AuthStatusandActivityViewModel
    {
        public AuthStatusandActivityViewModel()
        {
            Status = new List<AuthStatusViewModel>();

            StatusActivity = new List<AuthStatusActivityViewModel>();
        }
        public List<AuthStatusViewModel> Status { get; set; }

        public List<AuthStatusActivityViewModel> StatusActivity { get; set; }
    }
}