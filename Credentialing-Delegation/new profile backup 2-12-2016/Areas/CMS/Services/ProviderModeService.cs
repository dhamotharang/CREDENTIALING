using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ProviderModeService : IProviderModeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ProviderModeService constructor For ServiceUtility
        /// </summary>
        public ProviderModeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ProviderMode
        /// </summary>
        /// <returns>List of ProviderMode</returns>
        public List<ProviderModeViewModel> GetAll()
        {
            List<ProviderModeViewModel> ProviderModeList = new List<ProviderModeViewModel>();
            Task<string> ProviderMode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllProviderModes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ProviderMode.Result != null)
                {
                    ProviderModeList = JsonConvert.DeserializeObject<List<ProviderModeViewModel>>(ProviderMode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProviderModeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ProviderModeCode">ProviderMode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ProviderModeViewModel GetByUniqueCode(string Code)
        {
            ProviderModeViewModel _object = new ProviderModeViewModel();
            Task<string> ProviderMode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetProviderMode?ProviderModeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ProviderMode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ProviderModeViewModel>(ProviderMode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ProviderMode and Return Updated ProviderMode
        /// </summary>
        /// <param name="ProviderMode">ProviderMode to Create</param>
        /// <returns>Updated ProviderMode</returns>
        public ProviderModeViewModel Create(ProviderModeViewModel ProviderMode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddProviderMode", ProviderMode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderModeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ProviderMode and Return Updated ProviderMode
        /// </summary>
        /// <param name="ProviderMode">ProviderMode to Update</param>
        /// <returns>Updated ProviderMode</returns>
        public ProviderModeViewModel Update(ProviderModeViewModel ProviderMode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateProviderMode", ProviderMode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderModeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}