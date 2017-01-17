using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class NDCQualifierService : INDCQualifierService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// NDCQualifierService constructor For ServiceUtility
        /// </summary>
        public NDCQualifierService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of NDCQualifier
        /// </summary>
        /// <returns>List of NDCQualifier</returns>
        public List<NDCQualifierViewModel> GetAll()
        {
            List<NDCQualifierViewModel> NDCQualifierList = new List<NDCQualifierViewModel>();
            Task<string> NDCQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllNDCQualifiers?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (NDCQualifier.Result != null)
                {
                    NDCQualifierList = JsonConvert.DeserializeObject<List<NDCQualifierViewModel>>(NDCQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NDCQualifierList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="NDCQualifierCode">NDCQualifier's Code Parameter</param>
        /// <returns>Object Type</returns>
        public NDCQualifierViewModel GetByUniqueCode(string Code)
        {
            NDCQualifierViewModel _object = new NDCQualifierViewModel();
            Task<string> NDCQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetNDCQualifier?NDCQualifierCode=" + Code + "");
                return msg;
            });
            try
            {
                if (NDCQualifier.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<NDCQualifierViewModel>(NDCQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New NDCQualifier and Return Updated NDCQualifier
        /// </summary>
        /// <param name="NDCQualifier">NDCQualifier to Create</param>
        /// <returns>Updated NDCQualifier</returns>
        public NDCQualifierViewModel Create(NDCQualifierViewModel NDCQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddNDCQualifier", NDCQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NDCQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update NDCQualifier and Return Updated NDCQualifier
        /// </summary>
        /// <param name="NDCQualifier">NDCQualifier to Update</param>
        /// <returns>Updated NDCQualifier</returns>
        public NDCQualifierViewModel Update(NDCQualifierViewModel NDCQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateNDCQualifier", NDCQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NDCQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}