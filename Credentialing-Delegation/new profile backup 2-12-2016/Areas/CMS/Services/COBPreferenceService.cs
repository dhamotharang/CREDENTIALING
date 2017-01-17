using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class COBPreferenceService : ICOBPreferenceService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// COBPreferenceService constructor For ServiceUtility
        /// </summary>
        public COBPreferenceService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of COBPreference
        /// </summary>
        /// <returns>List of COBPreference</returns>
        public List<COBPreferenceViewModel> GetAll()
        {
            List<COBPreferenceViewModel> COBPreferenceList = new List<COBPreferenceViewModel>();
            Task<string> COBPreference = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllCOBPreferences?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (COBPreference.Result != null)
                {
                    COBPreferenceList = JsonConvert.DeserializeObject<List<COBPreferenceViewModel>>(COBPreference.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return COBPreferenceList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="COBPreferenceCode">COBPreference's Code Parameter</param>
        /// <returns>Object Type</returns>
        public COBPreferenceViewModel GetByUniqueCode(string Code)
        {
            COBPreferenceViewModel _object = new COBPreferenceViewModel();
            Task<string> COBPreference = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetCOBPreference?COBPreferenceCode=" + Code + "");
                return msg;
            });
            try
            {
                if (COBPreference.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<COBPreferenceViewModel>(COBPreference.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New COBPreference and Return Updated COBPreference
        /// </summary>
        /// <param name="COBPreference">COBPreference to Create</param>
        /// <returns>Updated COBPreference</returns>
        public COBPreferenceViewModel Create(COBPreferenceViewModel COBPreference)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddCOBPreference", COBPreference);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<COBPreferenceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update COBPreference and Return Updated COBPreference
        /// </summary>
        /// <param name="COBPreference">COBPreference to Update</param>
        /// <returns>Updated COBPreference</returns>
        public COBPreferenceViewModel Update(COBPreferenceViewModel COBPreference)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateCOBPreference", COBPreference);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<COBPreferenceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}