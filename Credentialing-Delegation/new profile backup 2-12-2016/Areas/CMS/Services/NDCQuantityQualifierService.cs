using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class NDCQuantityQualifierService : INDCQuantityQualifierService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// NDCQuantityQualifierService constructor For ServiceUtility
        /// </summary>
        public NDCQuantityQualifierService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of NDCQuantityQualifier
        /// </summary>
        /// <returns>List of NDCQuantityQualifier</returns>
        public List<NDCQuantityQualifierViewModel> GetAll()
        {
            List<NDCQuantityQualifierViewModel> NDCQuantityQualifierList = new List<NDCQuantityQualifierViewModel>();
            Task<string> NDCQuantityQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllNDCQuantityQualifiers?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (NDCQuantityQualifier.Result != null)
                {
                    NDCQuantityQualifierList = JsonConvert.DeserializeObject<List<NDCQuantityQualifierViewModel>>(NDCQuantityQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NDCQuantityQualifierList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="NDCQuantityQualifierCode">NDCQuantityQualifier's Code Parameter</param>
        /// <returns>Object Type</returns>
        public NDCQuantityQualifierViewModel GetByUniqueCode(string Code)
        {
            NDCQuantityQualifierViewModel _object = new NDCQuantityQualifierViewModel();
            Task<string> NDCQuantityQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetNDCQuantityQualifier?NDCQuantityQualifierCode=" + Code + "");
                return msg;
            });
            try
            {
                if (NDCQuantityQualifier.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<NDCQuantityQualifierViewModel>(NDCQuantityQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New NDCQuantityQualifier and Return Updated NDCQuantityQualifier
        /// </summary>
        /// <param name="NDCQuantityQualifier">NDCQuantityQualifier to Create</param>
        /// <returns>Updated NDCQuantityQualifier</returns>
        public NDCQuantityQualifierViewModel Create(NDCQuantityQualifierViewModel NDCQuantityQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddNDCQuantityQualifier", NDCQuantityQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NDCQuantityQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update NDCQuantityQualifier and Return Updated NDCQuantityQualifier
        /// </summary>
        /// <param name="NDCQuantityQualifier">NDCQuantityQualifier to Update</param>
        /// <returns>Updated NDCQuantityQualifier</returns>
        public NDCQuantityQualifierViewModel Update(NDCQuantityQualifierViewModel NDCQuantityQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateNDCQuantityQualifier", NDCQuantityQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NDCQuantityQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}