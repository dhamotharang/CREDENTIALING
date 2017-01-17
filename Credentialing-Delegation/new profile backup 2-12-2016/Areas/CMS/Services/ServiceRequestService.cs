using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ServiceRequestService constructor For ServiceUtility
        /// </summary>
        public ServiceRequestService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ServiceRequest
        /// </summary>
        /// <returns>List of ServiceRequest</returns>
        public List<ServiceRequestViewModel> GetAll()
        {
            List<ServiceRequestViewModel> ServiceRequestList = new List<ServiceRequestViewModel>();
            Task<string> ServiceRequest = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllServiceRequests?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ServiceRequest.Result != null)
                {
                    ServiceRequestList = JsonConvert.DeserializeObject<List<ServiceRequestViewModel>>(ServiceRequest.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ServiceRequestList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ServiceRequestCode">ServiceRequest's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ServiceRequestViewModel GetByUniqueCode(string Code)
        {
            ServiceRequestViewModel _object = new ServiceRequestViewModel();
            Task<string> ServiceRequest = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetServiceRequest?ServiceRequestCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ServiceRequest.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ServiceRequestViewModel>(ServiceRequest.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ServiceRequest and Return Updated ServiceRequest
        /// </summary>
        /// <param name="ServiceRequest">ServiceRequest to Create</param>
        /// <returns>Updated ServiceRequest</returns>
        public ServiceRequestViewModel Create(ServiceRequestViewModel ServiceRequest)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddServiceRequest", ServiceRequest);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ServiceRequestViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ServiceRequest and Return Updated ServiceRequest
        /// </summary>
        /// <param name="ServiceRequest">ServiceRequest to Update</param>
        /// <returns>Updated ServiceRequest</returns>
        public ServiceRequestViewModel Update(ServiceRequestViewModel ServiceRequest)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateServiceRequest", ServiceRequest);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ServiceRequestViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}