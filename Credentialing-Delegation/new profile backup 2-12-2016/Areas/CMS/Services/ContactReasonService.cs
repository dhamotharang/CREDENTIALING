using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ContactReasonService : IContactReasonService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ContactReasonService constructor For ServiceUtility
        /// </summary>
        public ContactReasonService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ContactReason
        /// </summary>
        /// <returns>List of ContactReason</returns>
        public List<ContactReasonViewModel> GetAll()
        {
            List<ContactReasonViewModel> ContactReasonList = new List<ContactReasonViewModel>();
            Task<string> ContactReason = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllContactReasons?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ContactReason.Result != null)
                {
                    ContactReasonList = JsonConvert.DeserializeObject<List<ContactReasonViewModel>>(ContactReason.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ContactReasonList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ContactReasonCode">ContactReason's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ContactReasonViewModel GetByUniqueCode(string Code)
        {
            ContactReasonViewModel _object = new ContactReasonViewModel();
            Task<string> ContactReason = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetContactReason?ContactReasonCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ContactReason.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ContactReasonViewModel>(ContactReason.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ContactReason and Return Updated ContactReason
        /// </summary>
        /// <param name="ContactReason">ContactReason to Create</param>
        /// <returns>Updated ContactReason</returns>
        public ContactReasonViewModel Create(ContactReasonViewModel ContactReason)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddContactReason", ContactReason);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactReasonViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ContactReason and Return Updated ContactReason
        /// </summary>
        /// <param name="ContactReason">ContactReason to Update</param>
        /// <returns>Updated ContactReason</returns>
        public ContactReasonViewModel Update(ContactReasonViewModel ContactReason)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateContactReason", ContactReason);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactReasonViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}