using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ClaimFrequencyCodeService : IClaimFrequencyCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ClaimFrequencyCodeService constructor For ServiceUtility
        /// </summary>
        public ClaimFrequencyCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ClaimFrequencyCode
        /// </summary>
        /// <returns>List of ClaimFrequencyCode</returns>
        public List<ClaimFrequencyCodeViewModel> GetAll()
        {
            List<ClaimFrequencyCodeViewModel> ClaimFrequencyCodeList = new List<ClaimFrequencyCodeViewModel>();
            Task<string> ClaimFrequencyCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetAllClaimFrequencyCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ClaimFrequencyCode.Result != null)
                {
                    ClaimFrequencyCodeList = JsonConvert.DeserializeObject<List<ClaimFrequencyCodeViewModel>>(ClaimFrequencyCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimFrequencyCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ClaimFrequencyCodeCode">ClaimFrequencyCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ClaimFrequencyCodeViewModel GetByUniqueCode(string Code)
        {
            ClaimFrequencyCodeViewModel _object = new ClaimFrequencyCodeViewModel();
            Task<string> ClaimFrequencyCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetClaimFrequencyCode?ClaimFrequencyCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ClaimFrequencyCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ClaimFrequencyCodeViewModel>(ClaimFrequencyCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ClaimFrequencyCode and Return Updated ClaimFrequencyCode
        /// </summary>
        /// <param name="ClaimFrequencyCode">ClaimFrequencyCode to Create</param>
        /// <returns>Updated ClaimFrequencyCode</returns>
        public ClaimFrequencyCodeViewModel Create(ClaimFrequencyCodeViewModel ClaimFrequencyCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementAdd/AddClaimFrequencyCode", ClaimFrequencyCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimFrequencyCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ClaimFrequencyCode and Return Updated ClaimFrequencyCode
        /// </summary>
        /// <param name="ClaimFrequencyCode">ClaimFrequencyCode to Update</param>
        /// <returns>Updated ClaimFrequencyCode</returns>
        public ClaimFrequencyCodeViewModel Update(ClaimFrequencyCodeViewModel ClaimFrequencyCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementUpdate/UpdateClaimFrequencyCode", ClaimFrequencyCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimFrequencyCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}