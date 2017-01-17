using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class VisaStatusService : IVisaStatusService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// VisaStatusService constructor For ServiceUtility
        /// </summary>
        public VisaStatusService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of VisaStatus
        /// </summary>
        /// <returns>List of VisaStatus</returns>
        public List<VisaStatusViewModel> GetAll()
        {
            List<VisaStatusViewModel> VisaStatusList = new List<VisaStatusViewModel>();
            Task<string> VisaStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllVisaStatuss?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (VisaStatus.Result != null)
                {
                    VisaStatusList = JsonConvert.DeserializeObject<List<VisaStatusViewModel>>(VisaStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return VisaStatusList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="VisaStatusCode">VisaStatus's Code Parameter</param>
        /// <returns>Object Type</returns>
        public VisaStatusViewModel GetByUniqueCode(string Code)
        {
            VisaStatusViewModel _object = new VisaStatusViewModel();
            Task<string> VisaStatus = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetVisaStatus?VisaStatusCode=" + Code + "");
                return msg;
            });
            try
            {
                if (VisaStatus.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<VisaStatusViewModel>(VisaStatus.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New VisaStatus and Return Updated VisaStatus
        /// </summary>
        /// <param name="VisaStatus">VisaStatus to Create</param>
        /// <returns>Updated VisaStatus</returns>
        public VisaStatusViewModel Create(VisaStatusViewModel VisaStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddVisaStatus", VisaStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VisaStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update VisaStatus and Return Updated VisaStatus
        /// </summary>
        /// <param name="VisaStatus">VisaStatus to Update</param>
        /// <returns>Updated VisaStatus</returns>
        public VisaStatusViewModel Update(VisaStatusViewModel VisaStatus)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateVisaStatus", VisaStatus);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VisaStatusViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}