using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DecredentialingReasonService : IDecredentialingReasonService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DecredentialingReasonService constructor For ServiceUtility
        /// </summary>
        public DecredentialingReasonService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DecredentialingReason
        /// </summary>
        /// <returns>List of DecredentialingReason</returns>
        public List<DecredentialingReasonViewModel> GetAll()
        {
            List<DecredentialingReasonViewModel> DecredentialingReasonList = new List<DecredentialingReasonViewModel>();
            Task<string> DecredentialingReason = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDecredentialingReasons?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DecredentialingReason.Result != null)
                {
                    DecredentialingReasonList = JsonConvert.DeserializeObject<List<DecredentialingReasonViewModel>>(DecredentialingReason.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DecredentialingReasonList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DecredentialingReasonCode">DecredentialingReason's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DecredentialingReasonViewModel GetByUniqueCode(string Code)
        {
            DecredentialingReasonViewModel _object = new DecredentialingReasonViewModel();
            Task<string> DecredentialingReason = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDecredentialingReason?DecredentialingReasonCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DecredentialingReason.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DecredentialingReasonViewModel>(DecredentialingReason.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DecredentialingReason and Return Updated DecredentialingReason
        /// </summary>
        /// <param name="DecredentialingReason">DecredentialingReason to Create</param>
        /// <returns>Updated DecredentialingReason</returns>
        public DecredentialingReasonViewModel Create(DecredentialingReasonViewModel DecredentialingReason)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDecredentialingReason", DecredentialingReason);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DecredentialingReasonViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DecredentialingReason and Return Updated DecredentialingReason
        /// </summary>
        /// <param name="DecredentialingReason">DecredentialingReason to Update</param>
        /// <returns>Updated DecredentialingReason</returns>
        public DecredentialingReasonViewModel Update(DecredentialingReasonViewModel DecredentialingReason)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDecredentialingReason", DecredentialingReason);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DecredentialingReasonViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}