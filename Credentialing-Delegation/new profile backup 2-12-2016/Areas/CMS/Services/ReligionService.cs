using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ReligionService : IReligionService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ReligionService constructor For ServiceUtility
        /// </summary>
        public ReligionService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Religion
        /// </summary>
        /// <returns>List of Religion</returns>
        public List<ReligionViewModel> GetAll()
        {
            List<ReligionViewModel> ReligionList = new List<ReligionViewModel>();
            Task<string> Religion = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllReligions?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Religion.Result != null)
                {
                    ReligionList = JsonConvert.DeserializeObject<List<ReligionViewModel>>(Religion.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReligionList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ReligionCode">Religion's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ReligionViewModel GetByUniqueCode(string Code)
        {
            ReligionViewModel _object = new ReligionViewModel();
            Task<string> Religion = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetReligion?ReligionCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Religion.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ReligionViewModel>(Religion.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Religion and Return Updated Religion
        /// </summary>
        /// <param name="Religion">Religion to Create</param>
        /// <returns>Updated Religion</returns>
        public ReligionViewModel Create(ReligionViewModel Religion)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddReligion", Religion);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReligionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Religion and Return Updated Religion
        /// </summary>
        /// <param name="Religion">Religion to Update</param>
        /// <returns>Updated Religion</returns>
        public ReligionViewModel Update(ReligionViewModel Religion)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateReligion", Religion);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReligionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}