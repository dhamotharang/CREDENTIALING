using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services.CommonServices
{
    public class ContactServices //: IContactServices
    {
        AuthorizationContactViewModel ContactModel = new AuthorizationContactViewModel();
        List<AuthorizationContactViewModel> ContactModels = new List<AuthorizationContactViewModel>();
        public AuthorizationContactViewModel ViewContactServices(int ContactID)
        {
            throw new NotImplementedException();
        }

        public AuthorizationContactViewModel ADDContactServices(AuthorizationContactViewModel model)
        {
            throw new NotImplementedException();
        }

        public AuthorizationContactViewModel DeleteContactServices(AuthorizationContactViewModel model)
        {
            throw new NotImplementedException();
        }

        public AuthorizationContactViewModel EditContactServices(AuthorizationContactViewModel model)
        {
            throw new NotImplementedException();
        }


        public List<AuthorizationContactViewModel> GetAllContactsServices(int AuthorizationID)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Authorization/Contacts.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            ContactModels = serial.Deserialize<List<AuthorizationContactViewModel>>(json);
            return ContactModels;
        }


        public List<AuthorizationContactViewModel> GetAllContactSubscriberIDServices(string SubscriberID)
        {
            throw new NotImplementedException();
        }
    }
}