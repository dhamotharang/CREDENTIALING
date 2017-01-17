using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class StateLicenseStatusService : IStateLicenseStatusService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// StateLicenseStatusService constructor For ServiceUtility
        /// </summary>
        public StateLicenseStatusService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of StateLicenseStatus
        /// </summary>
        /// <returns>List of StateLicenseStatus</returns>
        public List<StateLicenseStatusViewModel> GetAll()
        {
            List<StateLicenseStatusViewModel> StateLicenseStatusList = new List<StateLicenseStatusViewModel>();
            Task<string> StateLicenseStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllStateLicenseStatuss?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (StateLicenseStatus.Result != null)
                {
                    StateLicenseStatusList = JsonConvert.DeserializeObject<List<StateLicenseStatusViewModel>>(StateLicenseStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return StateLicenseStatusList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="StateLicenseStatusCode">StateLicenseStatus's Code Parameter</param>
        /// <returns>Object Type</returns>
        public StateLicenseStatusViewModel GetByUniqueCode(string Code)
        {
            StateLicenseStatusViewModel _object = new StateLicenseStatusViewModel();
            Task<string> StateLicenseStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetStateLicenseStatus?StateLicenseStatusCode=" + Code + "");
                return msg;
            });
            try
            {
                if (StateLicenseStatus.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<StateLicenseStatusViewModel>(StateLicenseStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New StateLicenseStatus and Return Updated StateLicenseStatus
        /// </summary>
        /// <param name="StateLicenseStatus">StateLicenseStatus to Create</param>
        /// <returns>Updated StateLicenseStatus</returns>
        public StateLicenseStatusViewModel Create(StateLicenseStatusViewModel StateLicenseStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddStateLicenseStatus", StateLicenseStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<StateLicenseStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update StateLicenseStatus and Return Updated StateLicenseStatus
        /// </summary>
        /// <param name="StateLicenseStatus">StateLicenseStatus to Update</param>
        /// <returns>Updated StateLicenseStatus</returns>
        public StateLicenseStatusViewModel Update(StateLicenseStatusViewModel StateLicenseStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateStateLicenseStatus", StateLicenseStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<StateLicenseStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}