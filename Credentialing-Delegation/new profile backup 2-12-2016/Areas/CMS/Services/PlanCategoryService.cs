using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class PlanCategoryService : IPlanCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// PlanCategoryService constructor For ServiceUtility
        /// </summary>
        public PlanCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of PlanCategory
        /// </summary>
        /// <returns>List of PlanCategory</returns>
        public List<PlanCategoryViewModel> GetAll()
        {
            List<PlanCategoryViewModel> PlanCategoryList = new List<PlanCategoryViewModel>();
            Task<string> PlanCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPlanCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (PlanCategory.Result != null)
                {
                    PlanCategoryList = JsonConvert.DeserializeObject<List<PlanCategoryViewModel>>(PlanCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PlanCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="PlanCategoryCode">PlanCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public PlanCategoryViewModel GetByUniqueCode(string Code)
        {
            PlanCategoryViewModel _object = new PlanCategoryViewModel();
            Task<string> PlanCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPlanCategory?PlanCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (PlanCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<PlanCategoryViewModel>(PlanCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New PlanCategory and Return Updated PlanCategory
        /// </summary>
        /// <param name="PlanCategory">PlanCategory to Create</param>
        /// <returns>Updated PlanCategory</returns>
        public PlanCategoryViewModel Create(PlanCategoryViewModel PlanCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPlanCategory", PlanCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PlanCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update PlanCategory and Return Updated PlanCategory
        /// </summary>
        /// <param name="PlanCategory">PlanCategory to Update</param>
        /// <returns>Updated PlanCategory</returns>
        public PlanCategoryViewModel Update(PlanCategoryViewModel PlanCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePlanCategory", PlanCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PlanCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}