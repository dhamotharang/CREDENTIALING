using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization;
using PortalTemplate.Areas.Portal.Models.PriorAuth.History;

namespace PortalTemplate.Areas.Portal.IServices
{
    public interface IPortalServices
    {
        PriorAuthorizationViewModel SavePreAuth(PriorAuthorizationViewModel dto);
        PriorAuthorizationViewModel RequestPreAuth(PriorAuthorizationViewModel dto);
        PriorAuthorizationViewModel EditPreAuth(PriorAuthorizationViewModel dto);
        PriorAuthorizationViewModel GetPreAuthByID(int ID);
        PortalHistory GetPortalHistory(string SubscriberID);        
    }
}