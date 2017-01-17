using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ContactTypeService : IContactTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ContactTypeService constructor For ServiceUtility
        /// </summary>
        public ContactTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ContactType
        /// </summary>
        /// <returns>List of ContactType</returns>
        public List<ContactTypeViewModel> GetAll()
        {
            List<ContactTypeViewModel> ContactTypeList = new List<ContactTypeViewModel>();
            Task<string> ContactType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllContactTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ContactType.Result != null)
                {
                    ContactTypeList = JsonConvert.DeserializeObject<List<ContactTypeViewModel>>(ContactType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ContactTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ContactTypeCode">ContactType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ContactTypeViewModel GetByUniqueCode(string Code)
        {
            ContactTypeViewModel _object = new ContactTypeViewModel();
            Task<string> ContactType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetContactType?ContactTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ContactType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ContactTypeViewModel>(ContactType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ContactType and Return Updated ContactType
        /// </summary>
        /// <param name="ContactType">ContactType to Create</param>
        /// <returns>Updated ContactType</returns>
        public ContactTypeViewModel Create(ContactTypeViewModel ContactType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddContactType", ContactType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ContactType and Return Updated ContactType
        /// </summary>
        /// <param name="ContactType">ContactType to Update</param>
        /// <returns>Updated ContactType</returns>
        public ContactTypeViewModel Update(ContactTypeViewModel ContactType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateContactType", ContactType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}