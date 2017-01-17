using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// RoomTypeService constructor For ServiceUtility
        /// </summary>
        public RoomTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of RoomType
        /// </summary>
        /// <returns>List of RoomType</returns>
        public List<RoomTypeViewModel> GetAll()
        {
            List<RoomTypeViewModel> RoomTypeList = new List<RoomTypeViewModel>();
            Task<string> RoomType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllRoomTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (RoomType.Result != null)
                {
                    RoomTypeList = JsonConvert.DeserializeObject<List<RoomTypeViewModel>>(RoomType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RoomTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="RoomTypeCode">RoomType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public RoomTypeViewModel GetByUniqueCode(string Code)
        {
            RoomTypeViewModel _object = new RoomTypeViewModel();
            Task<string> RoomType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetRoomType?RoomTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (RoomType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<RoomTypeViewModel>(RoomType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New RoomType and Return Updated RoomType
        /// </summary>
        /// <param name="RoomType">RoomType to Create</param>
        /// <returns>Updated RoomType</returns>
        public RoomTypeViewModel Create(RoomTypeViewModel RoomType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddRoomType", RoomType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RoomTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update RoomType and Return Updated RoomType
        /// </summary>
        /// <param name="RoomType">RoomType to Update</param>
        /// <returns>Updated RoomType</returns>
        public RoomTypeViewModel Update(RoomTypeViewModel RoomType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateRoomType", RoomType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RoomTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}