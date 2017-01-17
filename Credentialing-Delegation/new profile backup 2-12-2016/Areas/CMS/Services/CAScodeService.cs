using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class CAScodeService : ICAScodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// CAScodeService constructor For ServiceUtility
        /// </summary>
        public CAScodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of CAScode
        /// </summary>
        /// <returns>List of CAScode</returns>
        public List<CAScodeViewModel> GetAll()
        {
            List<CAScodeViewModel> CAScodeList = new List<CAScodeViewModel>();
            Task<string> CAScode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllCAScodes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (CAScode.Result != null)
                {
                    CAScodeList = JsonConvert.DeserializeObject<List<CAScodeViewModel>>(CAScode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return CAScodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="CAScodeCode">CAScode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public CAScodeViewModel GetByUniqueCode(string Code)
        {
            CAScodeViewModel _object = new CAScodeViewModel();
            Task<string> CAScode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetCAScode?CAScodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (CAScode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<CAScodeViewModel>(CAScode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New CAScode and Return Updated CAScode
        /// </summary>
        /// <param name="CAScode">CAScode to Create</param>
        /// <returns>Updated CAScode</returns>
        public CAScodeViewModel Create(CAScodeViewModel CAScode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddCAScode", CAScode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CAScodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update CAScode and Return Updated CAScode
        /// </summary>
        /// <param name="CAScode">CAScode to Update</param>
        /// <returns>Updated CAScode</returns>
        public CAScodeViewModel Update(CAScodeViewModel CAScode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateCAScode", CAScode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CAScodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}