using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ProviderLevelService : IProviderLevelService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ProviderLevelService constructor For ServiceUtility
        /// </summary>
        public ProviderLevelService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ProviderLevel
        /// </summary>
        /// <returns>List of ProviderLevel</returns>
        public List<ProviderLevelViewModel> GetAll()
        {
            List<ProviderLevelViewModel> ProviderLevelList = new List<ProviderLevelViewModel>();
            Task<string> ProviderLevel = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllProviderLevels?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ProviderLevel.Result != null)
                {
                    ProviderLevelList = JsonConvert.DeserializeObject<List<ProviderLevelViewModel>>(ProviderLevel.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProviderLevelList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ProviderLevelCode">ProviderLevel's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ProviderLevelViewModel GetByUniqueCode(string Code)
        {
            ProviderLevelViewModel _object = new ProviderLevelViewModel();
            Task<string> ProviderLevel = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetProviderLevel?ProviderLevelCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ProviderLevel.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ProviderLevelViewModel>(ProviderLevel.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ProviderLevel and Return Updated ProviderLevel
        /// </summary>
        /// <param name="ProviderLevel">ProviderLevel to Create</param>
        /// <returns>Updated ProviderLevel</returns>
        public ProviderLevelViewModel Create(ProviderLevelViewModel ProviderLevel)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddProviderLevel", ProviderLevel);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderLevelViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ProviderLevel and Return Updated ProviderLevel
        /// </summary>
        /// <param name="ProviderLevel">ProviderLevel to Update</param>
        /// <returns>Updated ProviderLevel</returns>
        public ProviderLevelViewModel Update(ProviderLevelViewModel ProviderLevel)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateProviderLevel", ProviderLevel);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderLevelViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}