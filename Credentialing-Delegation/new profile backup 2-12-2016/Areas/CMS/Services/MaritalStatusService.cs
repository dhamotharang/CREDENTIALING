using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class MaritalStatusService : IMaritalStatusService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// MaritalStatusService constructor For ServiceUtility
        /// </summary>
        public MaritalStatusService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of MaritalStatus
        /// </summary>
        /// <returns>List of MaritalStatus</returns>
        public List<MaritalStatusViewModel> GetAll()
        {
            List<MaritalStatusViewModel> MaritalStatusList = new List<MaritalStatusViewModel>();
            Task<string> MaritalStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllMaritalStatuss?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (MaritalStatus.Result != null)
                {
                    MaritalStatusList = JsonConvert.DeserializeObject<List<MaritalStatusViewModel>>(MaritalStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return MaritalStatusList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="MaritalStatusCode">MaritalStatus's Code Parameter</param>
        /// <returns>Object Type</returns>
        public MaritalStatusViewModel GetByUniqueCode(string Code)
        {
            MaritalStatusViewModel _object = new MaritalStatusViewModel();
            Task<string> MaritalStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetMaritalStatus?MaritalStatusCode=" + Code + "");
                return msg;
            });
            try
            {
                if (MaritalStatus.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<MaritalStatusViewModel>(MaritalStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New MaritalStatus and Return Updated MaritalStatus
        /// </summary>
        /// <param name="MaritalStatus">MaritalStatus to Create</param>
        /// <returns>Updated MaritalStatus</returns>
        public MaritalStatusViewModel Create(MaritalStatusViewModel MaritalStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddMaritalStatus", MaritalStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MaritalStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update MaritalStatus and Return Updated MaritalStatus
        /// </summary>
        /// <param name="MaritalStatus">MaritalStatus to Update</param>
        /// <returns>Updated MaritalStatus</returns>
        public MaritalStatusViewModel Update(MaritalStatusViewModel MaritalStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateMaritalStatus", MaritalStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MaritalStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}