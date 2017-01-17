using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class POSRoomTypeService : IPOSRoomTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// POSRoomTypeService constructor For ServiceUtility
        /// </summary>
        public POSRoomTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of POSRoomType
        /// </summary>
        /// <returns>List of POSRoomType</returns>
        public List<POSRoomTypeViewModel> GetAll()
        {
            List<POSRoomTypeViewModel> POSRoomTypeList = new List<POSRoomTypeViewModel>();
            Task<string> POSRoomType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPOSRoomTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (POSRoomType.Result != null)
                {
                    POSRoomTypeList = JsonConvert.DeserializeObject<List<POSRoomTypeViewModel>>(POSRoomType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return POSRoomTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="POSRoomTypeCode">POSRoomType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public POSRoomTypeViewModel GetByUniqueCode(string Code)
        {
            POSRoomTypeViewModel _object = new POSRoomTypeViewModel();
            Task<string> POSRoomType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPOSRoomType?POSRoomTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (POSRoomType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<POSRoomTypeViewModel>(POSRoomType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New POSRoomType and Return Updated POSRoomType
        /// </summary>
        /// <param name="POSRoomType">POSRoomType to Create</param>
        /// <returns>Updated POSRoomType</returns>
        public POSRoomTypeViewModel Create(POSRoomTypeViewModel POSRoomType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPOSRoomType", POSRoomType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<POSRoomTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update POSRoomType and Return Updated POSRoomType
        /// </summary>
        /// <param name="POSRoomType">POSRoomType to Update</param>
        /// <returns>Updated POSRoomType</returns>
        public POSRoomTypeViewModel Update(POSRoomTypeViewModel POSRoomType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePOSRoomType", POSRoomType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<POSRoomTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}