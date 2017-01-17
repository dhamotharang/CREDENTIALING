using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ClaimFormStatusService : IClaimFormStatusService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ClaimFormStatusService constructor For ServiceUtility
        /// </summary>
        public ClaimFormStatusService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ClaimFormStatus
        /// </summary>
        /// <returns>List of ClaimFormStatus</returns>
        public List<ClaimFormStatusViewModel> GetAll()
        {
            List<ClaimFormStatusViewModel> ClaimFormStatusList = new List<ClaimFormStatusViewModel>();
            Task<string> ClaimFormStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllClaimFormStatuss?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ClaimFormStatus.Result != null)
                {
                    ClaimFormStatusList = JsonConvert.DeserializeObject<List<ClaimFormStatusViewModel>>(ClaimFormStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimFormStatusList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ClaimFormStatusCode">ClaimFormStatus's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ClaimFormStatusViewModel GetByUniqueCode(string Code)
        {
            ClaimFormStatusViewModel _object = new ClaimFormStatusViewModel();
            Task<string> ClaimFormStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetClaimFormStatus?ClaimFormStatusCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ClaimFormStatus.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ClaimFormStatusViewModel>(ClaimFormStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ClaimFormStatus and Return Updated ClaimFormStatus
        /// </summary>
        /// <param name="ClaimFormStatus">ClaimFormStatus to Create</param>
        /// <returns>Updated ClaimFormStatus</returns>
        public ClaimFormStatusViewModel Create(ClaimFormStatusViewModel ClaimFormStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddClaimFormStatus", ClaimFormStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimFormStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ClaimFormStatus and Return Updated ClaimFormStatus
        /// </summary>
        /// <param name="ClaimFormStatus">ClaimFormStatus to Update</param>
        /// <returns>Updated ClaimFormStatus</returns>
        public ClaimFormStatusViewModel Update(ClaimFormStatusViewModel ClaimFormStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateClaimFormStatus", ClaimFormStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimFormStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}