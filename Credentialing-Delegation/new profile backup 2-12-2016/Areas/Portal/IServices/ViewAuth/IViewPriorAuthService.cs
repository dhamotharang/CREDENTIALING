using PortalTemplate.Areas.Portal.Models.PriorAuth.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.IServices.ViewAuth
{
    public interface IViewPriorAuthService
    {
        ViewPriorAuthorizationViewModel GetAuthByID(int AuthID);        
    }
}