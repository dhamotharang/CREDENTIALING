using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class RaceService : IRaceService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// RaceService constructor For ServiceUtility
        /// </summary>
        public RaceService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Race
        /// </summary>
        /// <returns>List of Race</returns>
        public List<RaceViewModel> GetAll()
        {
            List<RaceViewModel> RaceList = new List<RaceViewModel>();
            Task<string> Race = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllRaces?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Race.Result != null)
                {
                    RaceList = JsonConvert.DeserializeObject<List<RaceViewModel>>(Race.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RaceList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="RaceCode">Race's Code Parameter</param>
        /// <returns>Object Type</returns>
        public RaceViewModel GetByUniqueCode(string Code)
        {
            RaceViewModel _object = new RaceViewModel();
            Task<string> Race = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetRace?RaceCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Race.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<RaceViewModel>(Race.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Race and Return Updated Race
        /// </summary>
        /// <param name="Race">Race to Create</param>
        /// <returns>Updated Race</returns>
        public RaceViewModel Create(RaceViewModel Race)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddRace", Race);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RaceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Race and Return Updated Race
        /// </summary>
        /// <param name="Race">Race to Update</param>
        /// <returns>Updated Race</returns>
        public RaceViewModel Update(RaceViewModel Race)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateRace", Race);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RaceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}