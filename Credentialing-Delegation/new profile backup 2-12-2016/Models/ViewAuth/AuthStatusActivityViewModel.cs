using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.ViewAuth
{
    public class AuthStatusActivityViewModel
    {
        /*All the place-holders are new entries*/
        public DateTime Date { get; set; }

        public string Time { get; set; }

        public string User { get; set; }

        public string Module { get; set; }

        public string Category { get; set; }

        public string Id { get; set; }

        public string Screen { get; set; }

        public string Action { get; set; }

        public string Outcome { get; set; }

        public string ActivityLog { get; set; }
    }
}