using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ICDCodeService : IICDCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ICDCodeService constructor For ServiceUtility
        /// </summary>
        public ICDCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ICDCode
        /// </summary>
        /// <returns>List of ICDCode</returns>
        public List<ICDCodeViewModel> GetAll()
        {
            List<ICDCodeViewModel> ICDCodeList = new List<ICDCodeViewModel>();
            Task<string> ICDCode = Task.Run(async () =>
            {
                //string msg = await utility.GetDataFromService("api/Common/GetAllICDCodes?IncludedInactive=true");
                //string msg = await utility.GetDataFromService("api/Common/GetAllICDCodesByVersion?Version=ICD9&IncludedInactive=true");
                //string msg = await utility.GetDataFromService("api/Common/GetAllICDCodesByVersion?Version=ICD10&IncludedInactive=true");
                //string msg = await utility.GetDataFromService("api/Common/GetAllICDCodes?IncludedInactive=true");
                string msg = await utility.GetDataFromService("api/Common/GetAllICDCodesByCode?ICDCode=S20.21&IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ICDCode.Result != null)
                {
                    ICDCodeList = JsonConvert.DeserializeObject<List<ICDCodeViewModel>>(ICDCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ICDCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ICDCodeCode">ICDCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ICDCodeViewModel GetByUniqueCode(string Code)
        {
            ICDCodeViewModel _object = new ICDCodeViewModel();
            Task<string> ICDCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetICDCode?ICDCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ICDCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ICDCodeViewModel>(ICDCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ICDCode and Return Updated ICDCode
        /// </summary>
        /// <param name="ICDCode">ICDCode to Create</param>
        /// <returns>Updated ICDCode</returns>
        public ICDCodeViewModel Create(ICDCodeViewModel ICDCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddICDCode", ICDCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ICDCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ICDCode and Return Updated ICDCode
        /// </summary>
        /// <param name="ICDCode">ICDCode to Update</param>
        /// <returns>Updated ICDCode</returns>
        public ICDCodeViewModel Update(ICDCodeViewModel ICDCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateICDCode", ICDCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ICDCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}