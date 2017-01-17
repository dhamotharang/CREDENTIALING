using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DepartmentService : IDepartmentService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DepartmentService constructor For ServiceUtility
        /// </summary>
        public DepartmentService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Department
        /// </summary>
        /// <returns>List of Department</returns>
        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> DepartmentList = new List<DepartmentViewModel>();
            Task<string> Department = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDepartments?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Department.Result != null)
                {
                    DepartmentList = JsonConvert.DeserializeObject<List<DepartmentViewModel>>(Department.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DepartmentList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DepartmentCode">Department's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DepartmentViewModel GetByUniqueCode(string Code)
        {
            DepartmentViewModel _object = new DepartmentViewModel();
            Task<string> Department = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDepartment?DepartmentCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Department.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DepartmentViewModel>(Department.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Department and Return Updated Department
        /// </summary>
        /// <param name="Department">Department to Create</param>
        /// <returns>Updated Department</returns>
        public DepartmentViewModel Create(DepartmentViewModel Department)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDepartment", Department);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DepartmentViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Department and Return Updated Department
        /// </summary>
        /// <param name="Department">Department to Update</param>
        /// <returns>Updated Department</returns>
        public DepartmentViewModel Update(DepartmentViewModel Department)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDepartment", Department);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DepartmentViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}