using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class RangeService : IRangeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// RangeService constructor For ServiceUtility
        /// </summary>
        public RangeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Range
        /// </summary>
        /// <returns>List of Range</returns>
        public List<RangeViewModel> GetAll()
        {
            List<RangeViewModel> RangeList = new List<RangeViewModel>();
            Task<string> Range = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllRanges?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Range.Result != null)
                {
                    RangeList = JsonConvert.DeserializeObject<List<RangeViewModel>>(Range.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RangeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="RangeCode">Range's Code Parameter</param>
        /// <returns>Object Type</returns>
        public RangeViewModel GetByUniqueCode(string Code)
        {
            RangeViewModel _object = new RangeViewModel();
            Task<string> Range = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetRange?RangeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Range.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<RangeViewModel>(Range.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Range and Return Updated Range
        /// </summary>
        /// <param name="Range">Range to Create</param>
        /// <returns>Updated Range</returns>
        public RangeViewModel Create(RangeViewModel Range)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddRange", Range);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RangeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Range and Return Updated Range
        /// </summary>
        /// <param name="Range">Range to Update</param>
        /// <returns>Updated Range</returns>
        public RangeViewModel Update(RangeViewModel Range)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateRange", Range);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RangeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}