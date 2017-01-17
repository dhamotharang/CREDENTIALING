using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ClaimValueCodeService : IClaimValueCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ClaimValueCodeService constructor For ServiceUtility
        /// </summary>
        public ClaimValueCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ClaimValueCode
        /// </summary>
        /// <returns>List of ClaimValueCode</returns>
        public List<ClaimValueCodeViewModel> GetAll()
        {
            List<ClaimValueCodeViewModel> ClaimValueCodeList = new List<ClaimValueCodeViewModel>();
            Task<string> ClaimValueCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetAllClaimValueCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ClaimValueCode.Result != null)
                {
                    ClaimValueCodeList = JsonConvert.DeserializeObject<List<ClaimValueCodeViewModel>>(ClaimValueCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimValueCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ClaimValueCodeCode">ClaimValueCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ClaimValueCodeViewModel GetByUniqueCode(string Code)
        {
            ClaimValueCodeViewModel _object = new ClaimValueCodeViewModel();
            Task<string> ClaimValueCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetClaimValueCode?ClaimValueCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ClaimValueCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ClaimValueCodeViewModel>(ClaimValueCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ClaimValueCode and Return Updated ClaimValueCode
        /// </summary>
        /// <param name="ClaimValueCode">ClaimValueCode to Create</param>
        /// <returns>Updated ClaimValueCode</returns>
        public ClaimValueCodeViewModel Create(ClaimValueCodeViewModel ClaimValueCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementAdd/AddClaimValueCode", ClaimValueCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimValueCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ClaimValueCode and Return Updated ClaimValueCode
        /// </summary>
        /// <param name="ClaimValueCode">ClaimValueCode to Update</param>
        /// <returns>Updated ClaimValueCode</returns>
        public ClaimValueCodeViewModel Update(ClaimValueCodeViewModel ClaimValueCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementUpdate/UpdateClaimValueCode", ClaimValueCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimValueCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}