using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class AdjustmentReasonCodeService : IAdjustmentReasonCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// AdjustmentReasonCodeService constructor For ServiceUtility
        /// </summary>
        public AdjustmentReasonCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of AdjustmentReasonCode
        /// </summary>
        /// <returns>List of AdjustmentReasonCode</returns>
        public List<AdjustmentReasonCodeViewModel> GetAll()
        {
            List<AdjustmentReasonCodeViewModel> AdjustmentReasonCodeList = new List<AdjustmentReasonCodeViewModel>();
            Task<string> AdjustmentReasonCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllAdjustmentReasonCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (AdjustmentReasonCode.Result != null)
                {
                    AdjustmentReasonCodeList = JsonConvert.DeserializeObject<List<AdjustmentReasonCodeViewModel>>(AdjustmentReasonCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return AdjustmentReasonCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="AdjustmentReasonCodeCode">AdjustmentReasonCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public AdjustmentReasonCodeViewModel GetByUniqueCode(string Code)
        {
            AdjustmentReasonCodeViewModel _object = new AdjustmentReasonCodeViewModel();
            Task<string> AdjustmentReasonCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAdjustmentReasonCode?AdjustmentReasonCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (AdjustmentReasonCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<AdjustmentReasonCodeViewModel>(AdjustmentReasonCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New AdjustmentReasonCode and Return Updated AdjustmentReasonCode
        /// </summary>
        /// <param name="AdjustmentReasonCode">AdjustmentReasonCode to Create</param>
        /// <returns>Updated AdjustmentReasonCode</returns>
        public AdjustmentReasonCodeViewModel Create(AdjustmentReasonCodeViewModel AdjustmentReasonCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddAdjustmentReasonCode", AdjustmentReasonCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdjustmentReasonCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update AdjustmentReasonCode and Return Updated AdjustmentReasonCode
        /// </summary>
        /// <param name="AdjustmentReasonCode">AdjustmentReasonCode to Update</param>
        /// <returns>Updated AdjustmentReasonCode</returns>
        public AdjustmentReasonCodeViewModel Update(AdjustmentReasonCodeViewModel AdjustmentReasonCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateAdjustmentReasonCode", AdjustmentReasonCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdjustmentReasonCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}