using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ICD_HCCService : IICD_HCCService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ICD_HCCService constructor For ServiceUtility
        /// </summary>
        public ICD_HCCService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ICD_HCC
        /// </summary>
        /// <returns>List of ICD_HCC</returns>
        public List<ICD_HCCViewModel> GetAll()
        {
            List<ICD_HCCViewModel> ICD_HCCList = new List<ICD_HCCViewModel>();
            Task<string> ICD_HCC = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllICD_HCCs?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ICD_HCC.Result != null)
                {
                    ICD_HCCList = JsonConvert.DeserializeObject<List<ICD_HCCViewModel>>(ICD_HCC.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ICD_HCCList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ICD_HCCCode">ICD_HCC's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ICD_HCCViewModel GetByUniqueCode(string Code)
        {
            ICD_HCCViewModel _object = new ICD_HCCViewModel();
            Task<string> ICD_HCC = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetICD_HCC?ICD_HCCCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ICD_HCC.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ICD_HCCViewModel>(ICD_HCC.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ICD_HCC and Return Updated ICD_HCC
        /// </summary>
        /// <param name="ICD_HCC">ICD_HCC to Create</param>
        /// <returns>Updated ICD_HCC</returns>
        public ICD_HCCViewModel Create(ICD_HCCViewModel ICD_HCC)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddICD_HCC", ICD_HCC);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ICD_HCCViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ICD_HCC and Return Updated ICD_HCC
        /// </summary>
        /// <param name="ICD_HCC">ICD_HCC to Update</param>
        /// <returns>Updated ICD_HCC</returns>
        public ICD_HCCViewModel Update(ICD_HCCViewModel ICD_HCC)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateICD_HCC", ICD_HCC);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ICD_HCCViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}