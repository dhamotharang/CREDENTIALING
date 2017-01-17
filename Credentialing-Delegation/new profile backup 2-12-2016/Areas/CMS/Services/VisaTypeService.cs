using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class VisaTypeService : IVisaTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// VisaTypeService constructor For ServiceUtility
        /// </summary>
        public VisaTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of VisaType
        /// </summary>
        /// <returns>List of VisaType</returns>
        public List<VisaTypeViewModel> GetAll()
        {
            List<VisaTypeViewModel> VisaTypeList = new List<VisaTypeViewModel>();
            Task<string> VisaType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllVisaTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (VisaType.Result != null)
                {
                    VisaTypeList = JsonConvert.DeserializeObject<List<VisaTypeViewModel>>(VisaType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return VisaTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="VisaTypeCode">VisaType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public VisaTypeViewModel GetByUniqueCode(string Code)
        {
            VisaTypeViewModel _object = new VisaTypeViewModel();
            Task<string> VisaType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetVisaType?VisaTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (VisaType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<VisaTypeViewModel>(VisaType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New VisaType and Return Updated VisaType
        /// </summary>
        /// <param name="VisaType">VisaType to Create</param>
        /// <returns>Updated VisaType</returns>
        public VisaTypeViewModel Create(VisaTypeViewModel VisaType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddVisaType", VisaType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VisaTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update VisaType and Return Updated VisaType
        /// </summary>
        /// <param name="VisaType">VisaType to Update</param>
        /// <returns>Updated VisaType</returns>
        public VisaTypeViewModel Update(VisaTypeViewModel VisaType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateVisaType", VisaType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VisaTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}