using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ReferenceQualifierService : IReferenceQualifierService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ReferenceQualifierService constructor For ServiceUtility
        /// </summary>
        public ReferenceQualifierService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ReferenceQualifier
        /// </summary>
        /// <returns>List of ReferenceQualifier</returns>
        public List<ReferenceQualifierViewModel> GetAll()
        {
            List<ReferenceQualifierViewModel> ReferenceQualifierList = new List<ReferenceQualifierViewModel>();
            Task<string> ReferenceQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllReferenceQualifiers?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ReferenceQualifier.Result != null)
                {
                    ReferenceQualifierList = JsonConvert.DeserializeObject<List<ReferenceQualifierViewModel>>(ReferenceQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReferenceQualifierList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ReferenceQualifierCode">ReferenceQualifier's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ReferenceQualifierViewModel GetByUniqueCode(string Code)
        {
            ReferenceQualifierViewModel _object = new ReferenceQualifierViewModel();
            Task<string> ReferenceQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetReferenceQualifier?ReferenceQualifierCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ReferenceQualifier.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ReferenceQualifierViewModel>(ReferenceQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ReferenceQualifier and Return Updated ReferenceQualifier
        /// </summary>
        /// <param name="ReferenceQualifier">ReferenceQualifier to Create</param>
        /// <returns>Updated ReferenceQualifier</returns>
        public ReferenceQualifierViewModel Create(ReferenceQualifierViewModel ReferenceQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddReferenceQualifier", ReferenceQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReferenceQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ReferenceQualifier and Return Updated ReferenceQualifier
        /// </summary>
        /// <param name="ReferenceQualifier">ReferenceQualifier to Update</param>
        /// <returns>Updated ReferenceQualifier</returns>
        public ReferenceQualifierViewModel Update(ReferenceQualifierViewModel ReferenceQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateReferenceQualifier", ReferenceQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReferenceQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}