using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ContactEntityService : IContactEntityService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ContactEntityService constructor For ServiceUtility
        /// </summary>
        public ContactEntityService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ContactEntity
        /// </summary>
        /// <returns>List of ContactEntity</returns>
        public List<ContactEntityViewModel> GetAll()
        {
            List<ContactEntityViewModel> ContactEntityList = new List<ContactEntityViewModel>();
            Task<string> ContactEntity = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllContactEntitys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ContactEntity.Result != null)
                {
                    ContactEntityList = JsonConvert.DeserializeObject<List<ContactEntityViewModel>>(ContactEntity.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ContactEntityList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ContactEntityCode">ContactEntity's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ContactEntityViewModel GetByUniqueCode(string Code)
        {
            ContactEntityViewModel _object = new ContactEntityViewModel();
            Task<string> ContactEntity = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetContactEntity?ContactEntityCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ContactEntity.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ContactEntityViewModel>(ContactEntity.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ContactEntity and Return Updated ContactEntity
        /// </summary>
        /// <param name="ContactEntity">ContactEntity to Create</param>
        /// <returns>Updated ContactEntity</returns>
        public ContactEntityViewModel Create(ContactEntityViewModel ContactEntity)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddContactEntity", ContactEntity);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactEntityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ContactEntity and Return Updated ContactEntity
        /// </summary>
        /// <param name="ContactEntity">ContactEntity to Update</param>
        /// <returns>Updated ContactEntity</returns>
        public ContactEntityViewModel Update(ContactEntityViewModel ContactEntity)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateContactEntity", ContactEntity);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactEntityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}