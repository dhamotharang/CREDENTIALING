using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class EDISegmentQualifierService : IEDISegmentQualifierService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// EDISegmentQualifierService constructor For ServiceUtility
        /// </summary>
        public EDISegmentQualifierService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of EDISegmentQualifier
        /// </summary>
        /// <returns>List of EDISegmentQualifier</returns>
        public List<EDISegmentQualifierViewModel> GetAll()
        {
            List<EDISegmentQualifierViewModel> EDISegmentQualifierList = new List<EDISegmentQualifierViewModel>();
            Task<string> EDISegmentQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllEDISegmentQualifiers?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (EDISegmentQualifier.Result != null)
                {
                    EDISegmentQualifierList = JsonConvert.DeserializeObject<List<EDISegmentQualifierViewModel>>(EDISegmentQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return EDISegmentQualifierList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="EDISegmentQualifierCode">EDISegmentQualifier's Code Parameter</param>
        /// <returns>Object Type</returns>
        public EDISegmentQualifierViewModel GetByUniqueCode(string Code)
        {
            EDISegmentQualifierViewModel _object = new EDISegmentQualifierViewModel();
            Task<string> EDISegmentQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetEDISegmentQualifier?EDISegmentQualifierCode=" + Code + "");
                return msg;
            });
            try
            {
                if (EDISegmentQualifier.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<EDISegmentQualifierViewModel>(EDISegmentQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New EDISegmentQualifier and Return Updated EDISegmentQualifier
        /// </summary>
        /// <param name="EDISegmentQualifier">EDISegmentQualifier to Create</param>
        /// <returns>Updated EDISegmentQualifier</returns>
        public EDISegmentQualifierViewModel Create(EDISegmentQualifierViewModel EDISegmentQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddEDISegmentQualifier", EDISegmentQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EDISegmentQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update EDISegmentQualifier and Return Updated EDISegmentQualifier
        /// </summary>
        /// <param name="EDISegmentQualifier">EDISegmentQualifier to Update</param>
        /// <returns>Updated EDISegmentQualifier</returns>
        public EDISegmentQualifierViewModel Update(EDISegmentQualifierViewModel EDISegmentQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateEDISegmentQualifier", EDISegmentQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EDISegmentQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}