using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class AdjustmentGroupCodeService : IAdjustmentGroupCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// AdjustmentGroupCodeService constructor For ServiceUtility
        /// </summary>
        public AdjustmentGroupCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of AdjustmentGroupCode
        /// </summary>
        /// <returns>List of AdjustmentGroupCode</returns>
        public List<AdjustmentGroupCodeViewModel> GetAll()
        {
            List<AdjustmentGroupCodeViewModel> AdjustmentGroupCodeList = new List<AdjustmentGroupCodeViewModel>();
            Task<string> AdjustmentGroupCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllAdjustmentGroupCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (AdjustmentGroupCode.Result != null)
                {
                    AdjustmentGroupCodeList = JsonConvert.DeserializeObject<List<AdjustmentGroupCodeViewModel>>(AdjustmentGroupCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return AdjustmentGroupCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="AdjustmentGroupCodeCode">AdjustmentGroupCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public AdjustmentGroupCodeViewModel GetByUniqueCode(string Code)
        {
            AdjustmentGroupCodeViewModel _object = new AdjustmentGroupCodeViewModel();
            Task<string> AdjustmentGroupCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAdjustmentGroupCode?AdjustmentGroupCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (AdjustmentGroupCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<AdjustmentGroupCodeViewModel>(AdjustmentGroupCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New AdjustmentGroupCode and Return Updated AdjustmentGroupCode
        /// </summary>
        /// <param name="AdjustmentGroupCode">AdjustmentGroupCode to Create</param>
        /// <returns>Updated AdjustmentGroupCode</returns>
        public AdjustmentGroupCodeViewModel Create(AdjustmentGroupCodeViewModel AdjustmentGroupCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddAdjustmentGroupCode", AdjustmentGroupCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdjustmentGroupCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update AdjustmentGroupCode and Return Updated AdjustmentGroupCode
        /// </summary>
        /// <param name="AdjustmentGroupCode">AdjustmentGroupCode to Update</param>
        /// <returns>Updated AdjustmentGroupCode</returns>
        public AdjustmentGroupCodeViewModel Update(AdjustmentGroupCodeViewModel AdjustmentGroupCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateAdjustmentGroupCode", AdjustmentGroupCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdjustmentGroupCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}