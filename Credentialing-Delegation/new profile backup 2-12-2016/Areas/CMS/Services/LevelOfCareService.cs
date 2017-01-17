using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class LevelOfCareService : ILevelOfCareService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// LevelOfCareService constructor For ServiceUtility
        /// </summary>
        public LevelOfCareService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of LevelOfCare
        /// </summary>
        /// <returns>List of LevelOfCare</returns>
        public List<LevelOfCareViewModel> GetAll()
        {
            List<LevelOfCareViewModel> LevelOfCareList = new List<LevelOfCareViewModel>();
            Task<string> LevelOfCare = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllLevelOfCares?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (LevelOfCare.Result != null)
                {
                    LevelOfCareList = JsonConvert.DeserializeObject<List<LevelOfCareViewModel>>(LevelOfCare.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return LevelOfCareList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="LevelOfCareCode">LevelOfCare's Code Parameter</param>
        /// <returns>Object Type</returns>
        public LevelOfCareViewModel GetByUniqueCode(string Code)
        {
            LevelOfCareViewModel _object = new LevelOfCareViewModel();
            Task<string> LevelOfCare = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetLevelOfCare?LevelOfCareCode=" + Code + "");
                return msg;
            });
            try
            {
                if (LevelOfCare.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<LevelOfCareViewModel>(LevelOfCare.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New LevelOfCare and Return Updated LevelOfCare
        /// </summary>
        /// <param name="LevelOfCare">LevelOfCare to Create</param>
        /// <returns>Updated LevelOfCare</returns>
        public LevelOfCareViewModel Create(LevelOfCareViewModel LevelOfCare)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddLevelOfCare", LevelOfCare);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LevelOfCareViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update LevelOfCare and Return Updated LevelOfCare
        /// </summary>
        /// <param name="LevelOfCare">LevelOfCare to Update</param>
        /// <returns>Updated LevelOfCare</returns>
        public LevelOfCareViewModel Update(LevelOfCareViewModel LevelOfCare)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateLevelOfCare", LevelOfCare);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LevelOfCareViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}