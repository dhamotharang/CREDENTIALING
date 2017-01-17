using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class EthnicityService : IEthnicityService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// EthnicityService constructor For ServiceUtility
        /// </summary>
        public EthnicityService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Ethnicity
        /// </summary>
        /// <returns>List of Ethnicity</returns>
        public List<EthnicityViewModel> GetAll()
        {
            List<EthnicityViewModel> EthnicityList = new List<EthnicityViewModel>();
            Task<string> Ethnicity = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllEthnicitys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Ethnicity.Result != null)
                {
                    EthnicityList = JsonConvert.DeserializeObject<List<EthnicityViewModel>>(Ethnicity.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return EthnicityList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="EthnicityCode">Ethnicity's Code Parameter</param>
        /// <returns>Object Type</returns>
        public EthnicityViewModel GetByUniqueCode(string Code)
        {
            EthnicityViewModel _object = new EthnicityViewModel();
            Task<string> Ethnicity = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetEthnicity?EthnicityCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Ethnicity.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<EthnicityViewModel>(Ethnicity.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Ethnicity and Return Updated Ethnicity
        /// </summary>
        /// <param name="Ethnicity">Ethnicity to Create</param>
        /// <returns>Updated Ethnicity</returns>
        public EthnicityViewModel Create(EthnicityViewModel Ethnicity)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddEthnicity", Ethnicity);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EthnicityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Ethnicity and Return Updated Ethnicity
        /// </summary>
        /// <param name="Ethnicity">Ethnicity to Update</param>
        /// <returns>Updated Ethnicity</returns>
        public EthnicityViewModel Update(EthnicityViewModel Ethnicity)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateEthnicity", Ethnicity);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EthnicityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}