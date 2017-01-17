using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ClaimTypeService : IClaimTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ClaimTypeService constructor For ServiceUtility
        /// </summary>
        public ClaimTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ClaimType
        /// </summary>
        /// <returns>List of ClaimType</returns>
        public List<ClaimTypeViewModel> GetAll()
        {
            List<ClaimTypeViewModel> ClaimTypeList = new List<ClaimTypeViewModel>();
            Task<string> ClaimType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllClaimTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ClaimType.Result != null)
                {
                    ClaimTypeList = JsonConvert.DeserializeObject<List<ClaimTypeViewModel>>(ClaimType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ClaimTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ClaimTypeCode">ClaimType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ClaimTypeViewModel GetByUniqueCode(string Code)
        {
            ClaimTypeViewModel _object = new ClaimTypeViewModel();
            Task<string> ClaimType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetClaimType?ClaimTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ClaimType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ClaimTypeViewModel>(ClaimType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ClaimType and Return Updated ClaimType
        /// </summary>
        /// <param name="ClaimType">ClaimType to Create</param>
        /// <returns>Updated ClaimType</returns>
        public ClaimTypeViewModel Create(ClaimTypeViewModel ClaimType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddClaimType", ClaimType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ClaimType and Return Updated ClaimType
        /// </summary>
        /// <param name="ClaimType">ClaimType to Update</param>
        /// <returns>Updated ClaimType</returns>
        public ClaimTypeViewModel Update(ClaimTypeViewModel ClaimType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateClaimType", ClaimType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ClaimTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}