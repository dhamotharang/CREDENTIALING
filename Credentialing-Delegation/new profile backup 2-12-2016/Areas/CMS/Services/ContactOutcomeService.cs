using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ContactOutcomeService : IContactOutcomeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ContactOutcomeService constructor For ServiceUtility
        /// </summary>
        public ContactOutcomeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ContactOutcome
        /// </summary>
        /// <returns>List of ContactOutcome</returns>
        public List<ContactOutcomeViewModel> GetAll()
        {
            List<ContactOutcomeViewModel> ContactOutcomeList = new List<ContactOutcomeViewModel>();
            Task<string> ContactOutcome = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllContactOutcomes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ContactOutcome.Result != null)
                {
                    ContactOutcomeList = JsonConvert.DeserializeObject<List<ContactOutcomeViewModel>>(ContactOutcome.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ContactOutcomeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ContactOutcomeCode">ContactOutcome's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ContactOutcomeViewModel GetByUniqueCode(string Code)
        {
            ContactOutcomeViewModel _object = new ContactOutcomeViewModel();
            Task<string> ContactOutcome = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetContactOutcome?ContactOutcomeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ContactOutcome.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ContactOutcomeViewModel>(ContactOutcome.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ContactOutcome and Return Updated ContactOutcome
        /// </summary>
        /// <param name="ContactOutcome">ContactOutcome to Create</param>
        /// <returns>Updated ContactOutcome</returns>
        public ContactOutcomeViewModel Create(ContactOutcomeViewModel ContactOutcome)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddContactOutcome", ContactOutcome);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactOutcomeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ContactOutcome and Return Updated ContactOutcome
        /// </summary>
        /// <param name="ContactOutcome">ContactOutcome to Update</param>
        /// <returns>Updated ContactOutcome</returns>
        public ContactOutcomeViewModel Update(ContactOutcomeViewModel ContactOutcome)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateContactOutcome", ContactOutcome);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ContactOutcomeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}