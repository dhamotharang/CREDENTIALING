//using Newtonsoft.Json;
//using PortalTemplate.Areas.UM.CustomHelpers;
//using PortalTemplate.Areas.UM.IServices;
//using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Hosting;
//using System.Web.Script.Serialization;

//namespace PortalTemplate.Areas.UM.Services.Contact
//{
//    public class ContactService : IContactServices
//    {
//        private readonly string baseURL;
//        private readonly ServiceUtility serviceUtility;

//        public ContactService()
//        {
//            this.baseURL = ConfigurationManager.AppSettings["UMService"].ToString();
//            this.serviceUtility = new ServiceUtility();
//        }
//        public Models.ViewModels.Contact.AuthorizationContactViewModel ViewContactServices(int ContactID)
//        {
//            throw new NotImplementedException();
//        }

//        public Models.ViewModels.Contact.AuthorizationContactViewModel ADDContactServices(Models.ViewModels.Contact.AuthorizationContactViewModel model)
//        {
//            Models.ViewModels.Contact.AuthorizationContactViewModel authorizationContactViewModel = new Models.ViewModels.Contact.AuthorizationContactViewModel();
//            Task<string> authorizationContact = Task.Run(async () =>
//            {
//                string msg = await serviceUtility.PostDataToService(baseURL, "api/Contact/SaveAuthorizationContact", model);
//                return msg;
//            });
//            if (authorizationContact.Result != null)
//            {
//                authorizationContactViewModel = JsonConvert.DeserializeObject<Models.ViewModels.Contact.AuthorizationContactViewModel>(authorizationContact.Result);
//            }
//            return model;
//        }

//        public Models.ViewModels.Contact.AuthorizationContactViewModel DeleteContactServices(Models.ViewModels.Contact.AuthorizationContactViewModel model)
//        {
//            throw new NotImplementedException();
//        }

//        public Models.ViewModels.Contact.AuthorizationContactViewModel EditContactServices(Models.ViewModels.Contact.AuthorizationContactViewModel model)
//        {
//            throw new NotImplementedException();
//        }

//        public List<Models.ViewModels.Contact.AuthorizationContactViewModel> GetAllContactSubscriberIDServices(string SubscriberID)
//        {
//            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Member/jsonForMemberContacts.txt");
//            string json = System.IO.File.ReadAllText(file);
//            JavaScriptSerializer serial = new JavaScriptSerializer();
//            List<AuthorizationContactViewModel> Contacts = new List<AuthorizationContactViewModel>();
//            Contacts = serial.Deserialize<List<AuthorizationContactViewModel>>(json);
//            return Contacts;
//        }

//        public List<Models.ViewModels.Contact.AuthorizationContactViewModel> GetAllContactsServices(int AuthorizationID)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}