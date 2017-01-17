using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class VisitTypeService : IVisitTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// VisitTypeService constructor For ServiceUtility
        /// </summary>
        public VisitTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of VisitType
        /// </summary>
        /// <returns>List of VisitType</returns>
        public List<VisitTypeViewModel> GetAll()
        {
            List<VisitTypeViewModel> VisitTypeList = new List<VisitTypeViewModel>();
            Task<string> VisitType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllVisitTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (VisitType.Result != null)
                {
                    VisitTypeList = JsonConvert.DeserializeObject<List<VisitTypeViewModel>>(VisitType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return VisitTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="VisitTypeCode">VisitType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public VisitTypeViewModel GetByUniqueCode(string Code)
        {
            VisitTypeViewModel _object = new VisitTypeViewModel();
            Task<string> VisitType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetVisitType?VisitTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (VisitType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<VisitTypeViewModel>(VisitType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New VisitType and Return Updated VisitType
        /// </summary>
        /// <param name="VisitType">VisitType to Create</param>
        /// <returns>Updated VisitType</returns>
        public VisitTypeViewModel Create(VisitTypeViewModel VisitType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddVisitType", VisitType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VisitTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update VisitType and Return Updated VisitType
        /// </summary>
        /// <param name="VisitType">VisitType to Update</param>
        /// <returns>Updated VisitType</returns>
        public VisitTypeViewModel Update(VisitTypeViewModel VisitType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateVisitType", VisitType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VisitTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}