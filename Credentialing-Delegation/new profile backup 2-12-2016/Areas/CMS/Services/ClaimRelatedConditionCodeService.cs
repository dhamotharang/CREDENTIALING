using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ClaimRelatedConditionCodeService : IClaimRelatedConditionCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ClaimRelatedConditionCodeService constructor For ServiceUtility
        /// </summary>
        public ClaimRelatedConditionCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ClaimRelatedConditionCode
        /// </summary>
        /// <returns>List of ClaimRelatedConditionCode</returns>
        public List<ClaimRelatedConditionCodeViewModel> GetAll()
        {
            List<ClaimRelatedConditionCodeViewModel> ClaimRelatedConditionCodeList = new List<ClaimRelatedConditionCodeViewModel>();
            Task<string> ClaimRelatedConditionCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetAllClaimRelatedConditionCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ClaimRelatedConditionCode.Result != null)
                {
                    ClaimRelatedConditionCodeList = JsonConvert.DeserializeObject<List<ClaimRelatedConditionCodeViewModel>>(ClaimRelatedConditionCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimRelatedConditionCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ClaimRelatedConditionCodeCode">ClaimRelatedConditionCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ClaimRelatedConditionCodeViewModel GetByUniqueCode(string Code)
        {
            ClaimRelatedConditionCodeViewModel _object = new ClaimRelatedConditionCodeViewModel();
            Task<string> ClaimRelatedConditionCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetClaimRelatedConditionCode?ClaimRelatedConditionCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ClaimRelatedConditionCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ClaimRelatedConditionCodeViewModel>(ClaimRelatedConditionCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ClaimRelatedConditionCode and Return Updated ClaimRelatedConditionCode
        /// </summary>
        /// <param name="ClaimRelatedConditionCode">ClaimRelatedConditionCode to Create</param>
        /// <returns>Updated ClaimRelatedConditionCode</returns>
        public ClaimRelatedConditionCodeViewModel Create(ClaimRelatedConditionCodeViewModel ClaimRelatedConditionCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementAdd/AddClaimRelatedConditionCode", ClaimRelatedConditionCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimRelatedConditionCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ClaimRelatedConditionCode and Return Updated ClaimRelatedConditionCode
        /// </summary>
        /// <param name="ClaimRelatedConditionCode">ClaimRelatedConditionCode to Update</param>
        /// <returns>Updated ClaimRelatedConditionCode</returns>
        public ClaimRelatedConditionCodeViewModel Update(ClaimRelatedConditionCodeViewModel ClaimRelatedConditionCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementUpdate/UpdateClaimRelatedConditionCode", ClaimRelatedConditionCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimRelatedConditionCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}