using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class CountryService : ICountryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// CountryService constructor For ServiceUtility
        /// </summary>
        public CountryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Country
        /// </summary>
        /// <returns>List of Country</returns>
        public List<CountryViewModel> GetAll()
        {
            List<CountryViewModel> CountryList = new List<CountryViewModel>();
            Task<string> Country = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllCountrys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Country.Result != null)
                {
                    CountryList = JsonConvert.DeserializeObject<List<CountryViewModel>>(Country.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return CountryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="CountryCode">Country's Code Parameter</param>
        /// <returns>Object Type</returns>
        public CountryViewModel GetByUniqueCode(string Code)
        {
            CountryViewModel _object = new CountryViewModel();
            Task<string> Country = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetCountry?CountryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Country.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<CountryViewModel>(Country.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Country and Return Updated Country
        /// </summary>
        /// <param name="Country">Country to Create</param>
        /// <returns>Updated Country</returns>
        public CountryViewModel Create(CountryViewModel Country)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddCountry", Country);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CountryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Country and Return Updated Country
        /// </summary>
        /// <param name="Country">Country to Update</param>
        /// <returns>Updated Country</returns>
        public CountryViewModel Update(CountryViewModel Country)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateCountry", Country);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CountryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}