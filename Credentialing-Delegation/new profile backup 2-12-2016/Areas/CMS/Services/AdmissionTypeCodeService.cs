using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class AdmissionTypeCodeService : IAdmissionTypeCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// AdmissionTypeCodeService constructor For ServiceUtility
        /// </summary>
        public AdmissionTypeCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of AdmissionTypeCode
        /// </summary>
        /// <returns>List of AdmissionTypeCode</returns>
        public List<AdmissionTypeCodeViewModel> GetAll()
        {
            List<AdmissionTypeCodeViewModel> AdmissionTypeCodeList = new List<AdmissionTypeCodeViewModel>();
            Task<string> AdmissionTypeCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllAdmissionTypeCodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (AdmissionTypeCode.Result != null)
                {
                    AdmissionTypeCodeList = JsonConvert.DeserializeObject<List<AdmissionTypeCodeViewModel>>(AdmissionTypeCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return AdmissionTypeCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="AdmissionTypeCodeCode">AdmissionTypeCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public AdmissionTypeCodeViewModel GetByUniqueCode(string Code)
        {
            AdmissionTypeCodeViewModel _object = new AdmissionTypeCodeViewModel();
            Task<string> AdmissionTypeCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAdmissionTypeCode?AdmissionTypeCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (AdmissionTypeCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<AdmissionTypeCodeViewModel>(AdmissionTypeCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New AdmissionTypeCode and Return Updated AdmissionTypeCode
        /// </summary>
        /// <param name="AdmissionTypeCode">AdmissionTypeCode to Create</param>
        /// <returns>Updated AdmissionTypeCode</returns>
        public AdmissionTypeCodeViewModel Create(AdmissionTypeCodeViewModel AdmissionTypeCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddAdmissionTypeCode", AdmissionTypeCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdmissionTypeCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update AdmissionTypeCode and Return Updated AdmissionTypeCode
        /// </summary>
        /// <param name="AdmissionTypeCode">AdmissionTypeCode to Update</param>
        /// <returns>Updated AdmissionTypeCode</returns>
        public AdmissionTypeCodeViewModel Update(AdmissionTypeCodeViewModel AdmissionTypeCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateAdmissionTypeCode", AdmissionTypeCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdmissionTypeCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}