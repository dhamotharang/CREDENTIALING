using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class CountyService : ICountyService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// CountyService constructor For ServiceUtility
        /// </summary>
        public CountyService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of County
        /// </summary>
        /// <returns>List of County</returns>
        public List<CountyViewModel> GetAll()
        {
            List<CountyViewModel> CountyList = new List<CountyViewModel>();
            Task<string> County = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllCountys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (County.Result != null)
                {
                    CountyList = JsonConvert.DeserializeObject<List<CountyViewModel>>(County.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return CountyList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="CountyCode">County's Code Parameter</param>
        /// <returns>Object Type</returns>
        public CountyViewModel GetByUniqueCode(string Code)
        {
            CountyViewModel _object = new CountyViewModel();
            Task<string> County = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetCounty?CountyCode=" + Code + "");
                return msg;
            });
            try
            {
                if (County.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<CountyViewModel>(County.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New County and Return Updated County
        /// </summary>
        /// <param name="County">County to Create</param>
        /// <returns>Updated County</returns>
        public CountyViewModel Create(CountyViewModel County)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddCounty", County);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CountyViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update County and Return Updated County
        /// </summary>
        /// <param name="County">County to Update</param>
        /// <returns>Updated County</returns>
        public CountyViewModel Update(CountyViewModel County)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateCounty", County);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CountyViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}