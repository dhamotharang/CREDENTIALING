using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class CityService : ICityService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// CityService constructor For ServiceUtility
        /// </summary>
        public CityService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of City
        /// </summary>
        /// <returns>List of City</returns>
        public List<CityViewModel> GetAll()
        {
            List<CityViewModel> CityList = new List<CityViewModel>();
            Task<string> City = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllCitys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (City.Result != null)
                {
                    CityList = JsonConvert.DeserializeObject<List<CityViewModel>>(City.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return CityList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="CityCode">City's Code Parameter</param>
        /// <returns>Object Type</returns>
        public CityViewModel GetByUniqueCode(string Code)
        {
            CityViewModel _object = new CityViewModel();
            Task<string> City = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetCity?CityCode=" + Code + "");
                return msg;
            });
            try
            {
                if (City.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<CityViewModel>(City.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New City and Return Updated City
        /// </summary>
        /// <param name="City">City to Create</param>
        /// <returns>Updated City</returns>
        public CityViewModel Create(CityViewModel City)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddCity", City);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update City and Return Updated City
        /// </summary>
        /// <param name="City">City to Update</param>
        /// <returns>Updated City</returns>
        public CityViewModel Update(CityViewModel City)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateCity", City);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}