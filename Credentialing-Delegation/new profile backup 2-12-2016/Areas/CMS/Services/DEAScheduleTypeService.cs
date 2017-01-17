using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DEAScheduleTypeService : IDEAScheduleTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DEAScheduleTypeService constructor For ServiceUtility
        /// </summary>
        public DEAScheduleTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DEAScheduleType
        /// </summary>
        /// <returns>List of DEAScheduleType</returns>
        public List<DEAScheduleTypeViewModel> GetAll()
        {
            List<DEAScheduleTypeViewModel> DEAScheduleTypeList = new List<DEAScheduleTypeViewModel>();
            Task<string> DEAScheduleType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDEAScheduleTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DEAScheduleType.Result != null)
                {
                    DEAScheduleTypeList = JsonConvert.DeserializeObject<List<DEAScheduleTypeViewModel>>(DEAScheduleType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DEAScheduleTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DEAScheduleTypeCode">DEAScheduleType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DEAScheduleTypeViewModel GetByUniqueCode(string Code)
        {
            DEAScheduleTypeViewModel _object = new DEAScheduleTypeViewModel();
            Task<string> DEAScheduleType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDEAScheduleType?DEAScheduleTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DEAScheduleType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DEAScheduleTypeViewModel>(DEAScheduleType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DEAScheduleType and Return Updated DEAScheduleType
        /// </summary>
        /// <param name="DEAScheduleType">DEAScheduleType to Create</param>
        /// <returns>Updated DEAScheduleType</returns>
        public DEAScheduleTypeViewModel Create(DEAScheduleTypeViewModel DEAScheduleType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDEAScheduleType", DEAScheduleType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DEAScheduleTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DEAScheduleType and Return Updated DEAScheduleType
        /// </summary>
        /// <param name="DEAScheduleType">DEAScheduleType to Update</param>
        /// <returns>Updated DEAScheduleType</returns>
        public DEAScheduleTypeViewModel Update(DEAScheduleTypeViewModel DEAScheduleType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDEAScheduleType", DEAScheduleType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DEAScheduleTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}