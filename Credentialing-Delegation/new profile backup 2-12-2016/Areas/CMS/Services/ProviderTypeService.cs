using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ProviderTypeService : IProviderTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ProviderTypeService constructor For ServiceUtility
        /// </summary>
        public ProviderTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ProviderType
        /// </summary>
        /// <returns>List of ProviderType</returns>
        public List<ProviderTypeViewModel> GetAll()
        {
            List<ProviderTypeViewModel> ProviderTypeList = new List<ProviderTypeViewModel>();
            Task<string> ProviderType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllProviderTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ProviderType.Result != null)
                {
                    ProviderTypeList = JsonConvert.DeserializeObject<List<ProviderTypeViewModel>>(ProviderType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProviderTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ProviderTypeCode">ProviderType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ProviderTypeViewModel GetByUniqueCode(string Code)
        {
            ProviderTypeViewModel _object = new ProviderTypeViewModel();
            Task<string> ProviderType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetProviderType?ProviderTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ProviderType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ProviderTypeViewModel>(ProviderType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ProviderType and Return Updated ProviderType
        /// </summary>
        /// <param name="ProviderType">ProviderType to Create</param>
        /// <returns>Updated ProviderType</returns>
        public ProviderTypeViewModel Create(ProviderTypeViewModel ProviderType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddProviderType", ProviderType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ProviderType and Return Updated ProviderType
        /// </summary>
        /// <param name="ProviderType">ProviderType to Update</param>
        /// <returns>Updated ProviderType</returns>
        public ProviderTypeViewModel Update(ProviderTypeViewModel ProviderType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateProviderType", ProviderType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}