using Newtonsoft.Json;
using PortalTemplate.Areas.Portal.IServices.ViewAuth;
using PortalTemplate.Areas.Portal.Models.PriorAuth.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PortalTemplate.Areas.Portal.Services.PriorAuth.ViewPriorAuth
{
    public class ViewPriorAuthService : IViewPriorAuthService
    {
        public ViewPriorAuthorizationViewModel GetAuthByID(int ID)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Authorization/ViewAuth.txt");
            var json = System.IO.File.ReadAllText(file);
            ViewPriorAuthorizationViewModel ViewAuthModal = new ViewPriorAuthorizationViewModel();
            ViewAuthModal = JsonConvert.DeserializeObject<ViewPriorAuthorizationViewModel>(json);
            return ViewAuthModal;
        }
    }
}