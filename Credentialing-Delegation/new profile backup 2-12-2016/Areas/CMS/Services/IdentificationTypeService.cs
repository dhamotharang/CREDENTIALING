using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class IdentificationTypeService : IIdentificationTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// IdentificationTypeService constructor For ServiceUtility
        /// </summary>
        public IdentificationTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of IdentificationType
        /// </summary>
        /// <returns>List of IdentificationType</returns>
        public List<IdentificationTypeViewModel> GetAll()
        {
            List<IdentificationTypeViewModel> IdentificationTypeList = new List<IdentificationTypeViewModel>();
            Task<string> IdentificationType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllIdentificationTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (IdentificationType.Result != null)
                {
                    IdentificationTypeList = JsonConvert.DeserializeObject<List<IdentificationTypeViewModel>>(IdentificationType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return IdentificationTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="IdentificationTypeCode">IdentificationType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public IdentificationTypeViewModel GetByUniqueCode(string Code)
        {
            IdentificationTypeViewModel _object = new IdentificationTypeViewModel();
            Task<string> IdentificationType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetIdentificationType?IdentificationTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (IdentificationType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<IdentificationTypeViewModel>(IdentificationType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New IdentificationType and Return Updated IdentificationType
        /// </summary>
        /// <param name="IdentificationType">IdentificationType to Create</param>
        /// <returns>Updated IdentificationType</returns>
        public IdentificationTypeViewModel Create(IdentificationTypeViewModel IdentificationType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddIdentificationType", IdentificationType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<IdentificationTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update IdentificationType and Return Updated IdentificationType
        /// </summary>
        /// <param name="IdentificationType">IdentificationType to Update</param>
        /// <returns>Updated IdentificationType</returns>
        public IdentificationTypeViewModel Update(IdentificationTypeViewModel IdentificationType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateIdentificationType", IdentificationType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<IdentificationTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}