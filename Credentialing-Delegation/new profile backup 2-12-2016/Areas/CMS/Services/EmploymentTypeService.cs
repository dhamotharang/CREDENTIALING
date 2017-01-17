using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class EmploymentTypeService : IEmploymentTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// EmploymentTypeService constructor For ServiceUtility
        /// </summary>
        public EmploymentTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of EmploymentType
        /// </summary>
        /// <returns>List of EmploymentType</returns>
        public List<EmploymentTypeViewModel> GetAll()
        {
            List<EmploymentTypeViewModel> EmploymentTypeList = new List<EmploymentTypeViewModel>();
            Task<string> EmploymentType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllEmploymentTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (EmploymentType.Result != null)
                {
                    EmploymentTypeList = JsonConvert.DeserializeObject<List<EmploymentTypeViewModel>>(EmploymentType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return EmploymentTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="EmploymentTypeCode">EmploymentType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public EmploymentTypeViewModel GetByUniqueCode(string Code)
        {
            EmploymentTypeViewModel _object = new EmploymentTypeViewModel();
            Task<string> EmploymentType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetEmploymentType?EmploymentTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (EmploymentType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<EmploymentTypeViewModel>(EmploymentType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New EmploymentType and Return Updated EmploymentType
        /// </summary>
        /// <param name="EmploymentType">EmploymentType to Create</param>
        /// <returns>Updated EmploymentType</returns>
        public EmploymentTypeViewModel Create(EmploymentTypeViewModel EmploymentType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddEmploymentType", EmploymentType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EmploymentTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update EmploymentType and Return Updated EmploymentType
        /// </summary>
        /// <param name="EmploymentType">EmploymentType to Update</param>
        /// <returns>Updated EmploymentType</returns>
        public EmploymentTypeViewModel Update(EmploymentTypeViewModel EmploymentType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateEmploymentType", EmploymentType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EmploymentTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}