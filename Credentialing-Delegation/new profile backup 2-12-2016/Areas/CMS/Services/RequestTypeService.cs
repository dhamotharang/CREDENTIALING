using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class RequestTypeService : IRequestTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// RequestTypeService constructor For ServiceUtility
        /// </summary>
        public RequestTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of RequestType
        /// </summary>
        /// <returns>List of RequestType</returns>
        public List<RequestTypeViewModel> GetAll()
        {
            List<RequestTypeViewModel> RequestTypeList = new List<RequestTypeViewModel>();
            Task<string> RequestType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllRequestTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (RequestType.Result != null)
                {
                    RequestTypeList = JsonConvert.DeserializeObject<List<RequestTypeViewModel>>(RequestType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RequestTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="RequestTypeCode">RequestType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public RequestTypeViewModel GetByUniqueCode(string Code)
        {
            RequestTypeViewModel _object = new RequestTypeViewModel();
            Task<string> RequestType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetRequestType?RequestTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (RequestType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<RequestTypeViewModel>(RequestType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New RequestType and Return Updated RequestType
        /// </summary>
        /// <param name="RequestType">RequestType to Create</param>
        /// <returns>Updated RequestType</returns>
        public RequestTypeViewModel Create(RequestTypeViewModel RequestType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddRequestType", RequestType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RequestTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update RequestType and Return Updated RequestType
        /// </summary>
        /// <param name="RequestType">RequestType to Update</param>
        /// <returns>Updated RequestType</returns>
        public RequestTypeViewModel Update(RequestTypeViewModel RequestType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateRequestType", RequestType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RequestTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}