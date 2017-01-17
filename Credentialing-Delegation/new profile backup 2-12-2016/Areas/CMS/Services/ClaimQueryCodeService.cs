using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ClaimQueryCodeService : IClaimQueryCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ClaimQueryCodeService constructor For ServiceUtility
        /// </summary>
        public ClaimQueryCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ClaimQueryCode
        /// </summary>
        /// <returns>List of ClaimQueryCode</returns>
        public List<ClaimQueryCodeViewModel> GetAll()
        {
            List<ClaimQueryCodeViewModel> ClaimQueryCodeList = new List<ClaimQueryCodeViewModel>();
            Task<string> ClaimQueryCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetAllClaimQueryCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ClaimQueryCode.Result != null)
                {
                    ClaimQueryCodeList = JsonConvert.DeserializeObject<List<ClaimQueryCodeViewModel>>(ClaimQueryCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimQueryCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ClaimQueryCodeCode">ClaimQueryCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ClaimQueryCodeViewModel GetByUniqueCode(string Code)
        {
            ClaimQueryCodeViewModel _object = new ClaimQueryCodeViewModel();
            Task<string> ClaimQueryCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/RevenueCycleManagement/GetClaimQueryCode?ClaimQueryCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ClaimQueryCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ClaimQueryCodeViewModel>(ClaimQueryCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ClaimQueryCode and Return Updated ClaimQueryCode
        /// </summary>
        /// <param name="ClaimQueryCode">ClaimQueryCode to Create</param>
        /// <returns>Updated ClaimQueryCode</returns>
        public ClaimQueryCodeViewModel Create(ClaimQueryCodeViewModel ClaimQueryCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementAdd/AddClaimQueryCode", ClaimQueryCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimQueryCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ClaimQueryCode and Return Updated ClaimQueryCode
        /// </summary>
        /// <param name="ClaimQueryCode">ClaimQueryCode to Update</param>
        /// <returns>Updated ClaimQueryCode</returns>
        public ClaimQueryCodeViewModel Update(ClaimQueryCodeViewModel ClaimQueryCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/RevenueCycleManagementUpdate/UpdateClaimQueryCode", ClaimQueryCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimQueryCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}