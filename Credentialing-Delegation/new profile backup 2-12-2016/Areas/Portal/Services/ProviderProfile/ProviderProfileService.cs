using Newtonsoft.Json;
using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.Portal.Models.ProviderProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Portal.Services.ProviderProfile
{
    public class ProviderProfileService : IProviderProfileService
    {
        private ProviderProfileViewModel _ProviderProfile;
        public ProviderProfileService()
        {
            _ProviderProfile = new ProviderProfileViewModel();
        }
        Models.ProviderProfile.ProviderProfileViewModel IProviderProfileService.GetProfile(int ProfileId)
        {
            string path;
            try
            {
                path = HostingEnvironment.MapPath("~/Areas/Portal/Resources/ProviderProfile/PersonalInformation.json");
                using (System.IO.TextReader reader = System.IO.File.OpenText(path))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _ProviderProfile.PersonalInformation = serial.Deserialize<PersonalInformationViewModel>(text);
                }
                path = HostingEnvironment.MapPath("~/Areas/Portal/Resources/ProviderProfile/ContactInformation.json");
                using (System.IO.TextReader reader = System.IO.File.OpenText(path))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _ProviderProfile.ContactInformation = serial.Deserialize<ContactInformationViewModal>(text);
                }
                path = HostingEnvironment.MapPath("~/Areas/Portal/Resources/ProviderProfile/WorkInformation.json");
                using (System.IO.TextReader reader = System.IO.File.OpenText(path))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _ProviderProfile.WorkInformation = JsonConvert.DeserializeObject<List<WorkInformationViewModel>>(text).ToList();//List<WorkInformationViewModel>>(text).ToList();
                }

                path = HostingEnvironment.MapPath("~/Areas/Portal/Resources/ProviderProfile/ContractInformation.json");
                using (System.IO.TextReader reader = System.IO.File.OpenText(path))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    _ProviderProfile.ContractInformation = serial.Deserialize<ContractInformationViewModel>(text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _ProviderProfile;
        }
        Models.ProviderProfile.ProviderProfileViewModel IProviderProfileService.EditProfile(Models.ProviderProfile.ProviderProfileViewModel ProviderProfile)
        {
            return ProviderProfile;
        }
    }
}