using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class SuffixService : ISuffixService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// SuffixService constructor For ServiceUtility
        /// </summary>
        public SuffixService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Suffix
        /// </summary>
        /// <returns>List of Suffix</returns>
        public List<SuffixViewModel> GetAll()
        {
            List<SuffixViewModel> SuffixList = new List<SuffixViewModel>();
            Task<string> Suffix = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllSuffixs?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Suffix.Result != null)
                {
                    SuffixList = JsonConvert.DeserializeObject<List<SuffixViewModel>>(Suffix.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return SuffixList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="SuffixCode">Suffix's Code Parameter</param>
        /// <returns>Object Type</returns>
        public SuffixViewModel GetByUniqueCode(string Code)
        {
            SuffixViewModel _object = new SuffixViewModel();
            Task<string> Suffix = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetSuffix?SuffixCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Suffix.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<SuffixViewModel>(Suffix.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Suffix and Return Updated Suffix
        /// </summary>
        /// <param name="Suffix">Suffix to Create</param>
        /// <returns>Updated Suffix</returns>
        public SuffixViewModel Create(SuffixViewModel Suffix)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddSuffix", Suffix);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SuffixViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Suffix and Return Updated Suffix
        /// </summary>
        /// <param name="Suffix">Suffix to Update</param>
        /// <returns>Updated Suffix</returns>
        public SuffixViewModel Update(SuffixViewModel Suffix)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateSuffix", Suffix);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SuffixViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}