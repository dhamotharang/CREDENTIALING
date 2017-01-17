using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class StaffCategoryService : IStaffCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// StaffCategoryService constructor For ServiceUtility
        /// </summary>
        public StaffCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of StaffCategory
        /// </summary>
        /// <returns>List of StaffCategory</returns>
        public List<StaffCategoryViewModel> GetAll()
        {
            List<StaffCategoryViewModel> StaffCategoryList = new List<StaffCategoryViewModel>();
            Task<string> StaffCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllStaffCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (StaffCategory.Result != null)
                {
                    StaffCategoryList = JsonConvert.DeserializeObject<List<StaffCategoryViewModel>>(StaffCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return StaffCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="StaffCategoryCode">StaffCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public StaffCategoryViewModel GetByUniqueCode(string Code)
        {
            StaffCategoryViewModel _object = new StaffCategoryViewModel();
            Task<string> StaffCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetStaffCategory?StaffCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (StaffCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<StaffCategoryViewModel>(StaffCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New StaffCategory and Return Updated StaffCategory
        /// </summary>
        /// <param name="StaffCategory">StaffCategory to Create</param>
        /// <returns>Updated StaffCategory</returns>
        public StaffCategoryViewModel Create(StaffCategoryViewModel StaffCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddStaffCategory", StaffCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<StaffCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update StaffCategory and Return Updated StaffCategory
        /// </summary>
        /// <param name="StaffCategory">StaffCategory to Update</param>
        /// <returns>Updated StaffCategory</returns>
        public StaffCategoryViewModel Update(StaffCategoryViewModel StaffCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateStaffCategory", StaffCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<StaffCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}