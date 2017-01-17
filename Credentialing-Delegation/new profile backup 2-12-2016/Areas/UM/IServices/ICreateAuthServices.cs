using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface ICreateAuthServices
    {
        AuthorizationViewModel CreateAuth(AuthorizationViewModel auth);
        AuthorizationViewModel EditAuth(AuthorizationViewModel auth);
        AuthorizationViewModel CopyAuth (AuthorizationViewModel auth);
        AuthorizationViewModel RescindAuth (AuthorizationViewModel auth);
        int GetHHCCount(string PlaceOfService, string SubscriberID);
    }
}