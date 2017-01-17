using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ReasonTypeService : IReasonTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ReasonTypeService constructor For ServiceUtility
        /// </summary>
        public ReasonTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ReasonType
        /// </summary>
        /// <returns>List of ReasonType</returns>
        public List<ReasonTypeViewModel> GetAll()
        {
            List<ReasonTypeViewModel> ReasonTypeList = new List<ReasonTypeViewModel>();
            Task<string> ReasonType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllReasonTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ReasonType.Result != null)
                {
                    ReasonTypeList = JsonConvert.DeserializeObject<List<ReasonTypeViewModel>>(ReasonType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReasonTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ReasonTypeCode">ReasonType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ReasonTypeViewModel GetByUniqueCode(string Code)
        {
            ReasonTypeViewModel _object = new ReasonTypeViewModel();
            Task<string> ReasonType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetReasonType?ReasonTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ReasonType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ReasonTypeViewModel>(ReasonType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ReasonType and Return Updated ReasonType
        /// </summary>
        /// <param name="ReasonType">ReasonType to Create</param>
        /// <returns>Updated ReasonType</returns>
        public ReasonTypeViewModel Create(ReasonTypeViewModel ReasonType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddReasonType", ReasonType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReasonTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ReasonType and Return Updated ReasonType
        /// </summary>
        /// <param name="ReasonType">ReasonType to Update</param>
        /// <returns>Updated ReasonType</returns>
        public ReasonTypeViewModel Update(ReasonTypeViewModel ReasonType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateReasonType", ReasonType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReasonTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}