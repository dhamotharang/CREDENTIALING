using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IContactServices
    {
        PortalTemplate.Areas.Portal.Models.Contact.AuthorizationContactViewModel ViewContactServices(int ContactID);
        AuthorizationContactViewModel ADDContactServices(AuthorizationContactViewModel model);
        AuthorizationContactViewModel DeleteContactServices(AuthorizationContactViewModel model);
        AuthorizationContactViewModel EditContactServices(AuthorizationContactViewModel model);
        List<AuthorizationContactViewModel> GetAllContactSubscriberIDServices(string SubscriberID);
        List<AuthorizationContactViewModel> GetAllContactsServices(int AuthorizationID);

    }
}