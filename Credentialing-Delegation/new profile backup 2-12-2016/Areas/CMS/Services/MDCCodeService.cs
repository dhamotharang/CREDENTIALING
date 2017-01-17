using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class MDCCodeService : IMDCCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// MDCCodeService constructor For ServiceUtility
        /// </summary>
        public MDCCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of MDCCode
        /// </summary>
        /// <returns>List of MDCCode</returns>
        public List<MDCCodeViewModel> GetAll()
        {
            List<MDCCodeViewModel> MDCCodeList = new List<MDCCodeViewModel>();
            Task<string> MDCCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllMDCCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (MDCCode.Result != null)
                {
                    MDCCodeList = JsonConvert.DeserializeObject<List<MDCCodeViewModel>>(MDCCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return MDCCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="MDCCodeCode">MDCCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public MDCCodeViewModel GetByUniqueCode(string Code)
        {
            MDCCodeViewModel _object = new MDCCodeViewModel();
            Task<string> MDCCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetMDCCode?MDCCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (MDCCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<MDCCodeViewModel>(MDCCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New MDCCode and Return Updated MDCCode
        /// </summary>
        /// <param name="MDCCode">MDCCode to Create</param>
        /// <returns>Updated MDCCode</returns>
        public MDCCodeViewModel Create(MDCCodeViewModel MDCCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddMDCCode", MDCCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MDCCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update MDCCode and Return Updated MDCCode
        /// </summary>
        /// <param name="MDCCode">MDCCode to Update</param>
        /// <returns>Updated MDCCode</returns>
        public MDCCodeViewModel Update(MDCCodeViewModel MDCCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateMDCCode", MDCCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MDCCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}