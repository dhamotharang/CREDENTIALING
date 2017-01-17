using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ReasonService : IReasonService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ReasonService constructor For ServiceUtility
        /// </summary>
        public ReasonService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Reason
        /// </summary>
        /// <returns>List of Reason</returns>
        public List<ReasonViewModel> GetAll()
        {
            List<ReasonViewModel> ReasonList = new List<ReasonViewModel>();
            Task<string> Reason = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllReasons?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Reason.Result != null)
                {
                    ReasonList = JsonConvert.DeserializeObject<List<ReasonViewModel>>(Reason.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReasonList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ReasonCode">Reason's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ReasonViewModel GetByUniqueCode(string Code)
        {
            ReasonViewModel _object = new ReasonViewModel();
            Task<string> Reason = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetReason?ReasonCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Reason.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ReasonViewModel>(Reason.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Reason and Return Updated Reason
        /// </summary>
        /// <param name="Reason">Reason to Create</param>
        /// <returns>Updated Reason</returns>
        public ReasonViewModel Create(ReasonViewModel Reason)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddReason", Reason);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReasonViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Reason and Return Updated Reason
        /// </summary>
        /// <param name="Reason">Reason to Update</param>
        /// <returns>Updated Reason</returns>
        public ReasonViewModel Update(ReasonViewModel Reason)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateReason", Reason);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReasonViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}