using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DeductionTypeService : IDeductionTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DeductionTypeService constructor For ServiceUtility
        /// </summary>
        public DeductionTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DeductionType
        /// </summary>
        /// <returns>List of DeductionType</returns>
        public List<DeductionTypeViewModel> GetAll()
        {
            List<DeductionTypeViewModel> DeductionTypeList = new List<DeductionTypeViewModel>();
            Task<string> DeductionType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDeductionTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DeductionType.Result != null)
                {
                    DeductionTypeList = JsonConvert.DeserializeObject<List<DeductionTypeViewModel>>(DeductionType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DeductionTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DeductionTypeCode">DeductionType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DeductionTypeViewModel GetByUniqueCode(string Code)
        {
            DeductionTypeViewModel _object = new DeductionTypeViewModel();
            Task<string> DeductionType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDeductionType?DeductionTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DeductionType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DeductionTypeViewModel>(DeductionType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DeductionType and Return Updated DeductionType
        /// </summary>
        /// <param name="DeductionType">DeductionType to Create</param>
        /// <returns>Updated DeductionType</returns>
        public DeductionTypeViewModel Create(DeductionTypeViewModel DeductionType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDeductionType", DeductionType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DeductionTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DeductionType and Return Updated DeductionType
        /// </summary>
        /// <param name="DeductionType">DeductionType to Update</param>
        /// <returns>Updated DeductionType</returns>
        public DeductionTypeViewModel Update(DeductionTypeViewModel DeductionType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDeductionType", DeductionType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DeductionTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}