using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ContactEntityTypeService : IContactEntityTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ContactEntityTypeService constructor For ServiceUtility
        /// </summary>
        public ContactEntityTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ContactEntityType
        /// </summary>
        /// <returns>List of ContactEntityType</returns>
        public List<ContactEntityTypeViewModel> GetAll()
        {
            List<ContactEntityTypeViewModel> ContactEntityTypeList = new List<ContactEntityTypeViewModel>();
            Task<string> ContactEntityType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllContactEntityTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ContactEntityType.Result != null)
                {
                    ContactEntityTypeList = JsonConvert.DeserializeObject<List<ContactEntityTypeViewModel>>(ContactEntityType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ContactEntityTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ContactEntityTypeCode">ContactEntityType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ContactEntityTypeViewModel GetByUniqueCode(string Code)
        {
            ContactEntityTypeViewModel _object = new ContactEntityTypeViewModel();
            Task<string> ContactEntityType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetContactEntityType?ContactEntityTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ContactEntityType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ContactEntityTypeViewModel>(ContactEntityType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ContactEntityType and Return Updated ContactEntityType
        /// </summary>
        /// <param name="ContactEntityType">ContactEntityType to Create</param>
        /// <returns>Updated ContactEntityType</returns>
        public ContactEntityTypeViewModel Create(ContactEntityTypeViewModel ContactEntityType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddContactEntityType", ContactEntityType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactEntityTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ContactEntityType and Return Updated ContactEntityType
        /// </summary>
        /// <param name="ContactEntityType">ContactEntityType to Update</param>
        /// <returns>Updated ContactEntityType</returns>
        public ContactEntityTypeViewModel Update(ContactEntityTypeViewModel ContactEntityType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateContactEntityType", ContactEntityType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactEntityTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}