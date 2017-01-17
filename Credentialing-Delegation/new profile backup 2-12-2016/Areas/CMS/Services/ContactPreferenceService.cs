using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ContactPreferenceService : IContactPreferenceService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ContactPreferenceService constructor For ServiceUtility
        /// </summary>
        public ContactPreferenceService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ContactPreference
        /// </summary>
        /// <returns>List of ContactPreference</returns>
        public List<ContactPreferenceViewModel> GetAll()
        {
            List<ContactPreferenceViewModel> ContactPreferenceList = new List<ContactPreferenceViewModel>();
            Task<string> ContactPreference = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllContactPreferences?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ContactPreference.Result != null)
                {
                    ContactPreferenceList = JsonConvert.DeserializeObject<List<ContactPreferenceViewModel>>(ContactPreference.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ContactPreferenceList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ContactPreferenceCode">ContactPreference's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ContactPreferenceViewModel GetByUniqueCode(string Code)
        {
            ContactPreferenceViewModel _object = new ContactPreferenceViewModel();
            Task<string> ContactPreference = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetContactPreference?ContactPreferenceCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ContactPreference.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ContactPreferenceViewModel>(ContactPreference.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ContactPreference and Return Updated ContactPreference
        /// </summary>
        /// <param name="ContactPreference">ContactPreference to Create</param>
        /// <returns>Updated ContactPreference</returns>
        public ContactPreferenceViewModel Create(ContactPreferenceViewModel ContactPreference)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddContactPreference", ContactPreference);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactPreferenceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ContactPreference and Return Updated ContactPreference
        /// </summary>
        /// <param name="ContactPreference">ContactPreference to Update</param>
        /// <returns>Updated ContactPreference</returns>
        public ContactPreferenceViewModel Update(ContactPreferenceViewModel ContactPreference)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateContactPreference", ContactPreference);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactPreferenceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}