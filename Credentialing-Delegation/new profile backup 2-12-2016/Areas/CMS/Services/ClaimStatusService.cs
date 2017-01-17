using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ClaimStatusService : IClaimStatusService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ClaimStatusService constructor For ServiceUtility
        /// </summary>
        public ClaimStatusService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ClaimStatus
        /// </summary>
        /// <returns>List of ClaimStatus</returns>
        public List<ClaimStatusViewModel> GetAll()
        {
            List<ClaimStatusViewModel> ClaimStatusList = new List<ClaimStatusViewModel>();
            Task<string> ClaimStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllClaimStatuss?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ClaimStatus.Result != null)
                {
                    ClaimStatusList = JsonConvert.DeserializeObject<List<ClaimStatusViewModel>>(ClaimStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimStatusList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ClaimStatusCode">ClaimStatus's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ClaimStatusViewModel GetByUniqueCode(string Code)
        {
            ClaimStatusViewModel _object = new ClaimStatusViewModel();
            Task<string> ClaimStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetClaimStatus?ClaimStatusCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ClaimStatus.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ClaimStatusViewModel>(ClaimStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ClaimStatus and Return Updated ClaimStatus
        /// </summary>
        /// <param name="ClaimStatus">ClaimStatus to Create</param>
        /// <returns>Updated ClaimStatus</returns>
        public ClaimStatusViewModel Create(ClaimStatusViewModel ClaimStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddClaimStatus", ClaimStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ClaimStatus and Return Updated ClaimStatus
        /// </summary>
        /// <param name="ClaimStatus">ClaimStatus to Update</param>
        /// <returns>Updated ClaimStatus</returns>
        public ClaimStatusViewModel Update(ClaimStatusViewModel ClaimStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateClaimStatus", ClaimStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}