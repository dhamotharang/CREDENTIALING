using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DesignationService : IDesignationService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DesignationService constructor For ServiceUtility
        /// </summary>
        public DesignationService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Designation
        /// </summary>
        /// <returns>List of Designation</returns>
        public List<DesignationViewModel> GetAll()
        {
            List<DesignationViewModel> DesignationList = new List<DesignationViewModel>();
            Task<string> Designation = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDesignations?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Designation.Result != null)
                {
                    DesignationList = JsonConvert.DeserializeObject<List<DesignationViewModel>>(Designation.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DesignationList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DesignationCode">Designation's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DesignationViewModel GetByUniqueCode(string Code)
        {
            DesignationViewModel _object = new DesignationViewModel();
            Task<string> Designation = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDesignation?DesignationCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Designation.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DesignationViewModel>(Designation.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Designation and Return Updated Designation
        /// </summary>
        /// <param name="Designation">Designation to Create</param>
        /// <returns>Updated Designation</returns>
        public DesignationViewModel Create(DesignationViewModel Designation)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDesignation", Designation);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DesignationViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Designation and Return Updated Designation
        /// </summary>
        /// <param name="Designation">Designation to Update</param>
        /// <returns>Updated Designation</returns>
        public DesignationViewModel Update(DesignationViewModel Designation)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDesignation", Designation);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DesignationViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}