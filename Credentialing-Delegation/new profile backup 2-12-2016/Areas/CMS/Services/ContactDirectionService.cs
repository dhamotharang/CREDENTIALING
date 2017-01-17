using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ContactDirectionService : IContactDirectionService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ContactDirectionService constructor For ServiceUtility
        /// </summary>
        public ContactDirectionService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ContactDirection
        /// </summary>
        /// <returns>List of ContactDirection</returns>
        public List<ContactDirectionViewModel> GetAll()
        {
            List<ContactDirectionViewModel> ContactDirectionList = new List<ContactDirectionViewModel>();
            Task<string> ContactDirection = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllContactDirections?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ContactDirection.Result != null)
                {
                    ContactDirectionList = JsonConvert.DeserializeObject<List<ContactDirectionViewModel>>(ContactDirection.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ContactDirectionList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ContactDirectionCode">ContactDirection's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ContactDirectionViewModel GetByUniqueCode(string Code)
        {
            ContactDirectionViewModel _object = new ContactDirectionViewModel();
            Task<string> ContactDirection = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetContactDirection?ContactDirectionCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ContactDirection.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ContactDirectionViewModel>(ContactDirection.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ContactDirection and Return Updated ContactDirection
        /// </summary>
        /// <param name="ContactDirection">ContactDirection to Create</param>
        /// <returns>Updated ContactDirection</returns>
        public ContactDirectionViewModel Create(ContactDirectionViewModel ContactDirection)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddContactDirection", ContactDirection);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactDirectionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ContactDirection and Return Updated ContactDirection
        /// </summary>
        /// <param name="ContactDirection">ContactDirection to Update</param>
        /// <returns>Updated ContactDirection</returns>
        public ContactDirectionViewModel Update(ContactDirectionViewModel ContactDirection)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateContactDirection", ContactDirection);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactDirectionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}