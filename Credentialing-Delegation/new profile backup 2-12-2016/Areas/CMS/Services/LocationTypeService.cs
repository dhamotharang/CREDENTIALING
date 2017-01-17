using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class LocationTypeService : ILocationTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// LocationTypeService constructor For ServiceUtility
        /// </summary>
        public LocationTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of LocationType
        /// </summary>
        /// <returns>List of LocationType</returns>
        public List<LocationTypeViewModel> GetAll()
        {
            List<LocationTypeViewModel> LocationTypeList = new List<LocationTypeViewModel>();
            Task<string> LocationType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllLocationTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (LocationType.Result != null)
                {
                    LocationTypeList = JsonConvert.DeserializeObject<List<LocationTypeViewModel>>(LocationType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return LocationTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="LocationTypeCode">LocationType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public LocationTypeViewModel GetByUniqueCode(string Code)
        {
            LocationTypeViewModel _object = new LocationTypeViewModel();
            Task<string> LocationType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetLocationType?LocationTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (LocationType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<LocationTypeViewModel>(LocationType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New LocationType and Return Updated LocationType
        /// </summary>
        /// <param name="LocationType">LocationType to Create</param>
        /// <returns>Updated LocationType</returns>
        public LocationTypeViewModel Create(LocationTypeViewModel LocationType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddLocationType", LocationType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LocationTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update LocationType and Return Updated LocationType
        /// </summary>
        /// <param name="LocationType">LocationType to Update</param>
        /// <returns>Updated LocationType</returns>
        public LocationTypeViewModel Update(LocationTypeViewModel LocationType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateLocationType", LocationType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LocationTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}