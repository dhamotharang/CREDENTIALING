using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class SeverityService : ISeverityService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// SeverityService constructor For ServiceUtility
        /// </summary>
        public SeverityService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Severity
        /// </summary>
        /// <returns>List of Severity</returns>
        public List<SeverityViewModel> GetAll()
        {
            List<SeverityViewModel> SeverityList = new List<SeverityViewModel>();
            Task<string> Severity = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllSeveritys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Severity.Result != null)
                {
                    SeverityList = JsonConvert.DeserializeObject<List<SeverityViewModel>>(Severity.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return SeverityList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="SeverityCode">Severity's Code Parameter</param>
        /// <returns>Object Type</returns>
        public SeverityViewModel GetByUniqueCode(string Code)
        {
            SeverityViewModel _object = new SeverityViewModel();
            Task<string> Severity = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetSeverity?SeverityCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Severity.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<SeverityViewModel>(Severity.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Severity and Return Updated Severity
        /// </summary>
        /// <param name="Severity">Severity to Create</param>
        /// <returns>Updated Severity</returns>
        public SeverityViewModel Create(SeverityViewModel Severity)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddSeverity", Severity);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SeverityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Severity and Return Updated Severity
        /// </summary>
        /// <param name="Severity">Severity to Update</param>
        /// <returns>Updated Severity</returns>
        public SeverityViewModel Update(SeverityViewModel Severity)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateSeverity", Severity);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SeverityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}