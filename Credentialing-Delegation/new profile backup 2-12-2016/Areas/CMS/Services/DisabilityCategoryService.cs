using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DisabilityCategoryService : IDisabilityCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DisabilityCategoryService constructor For ServiceUtility
        /// </summary>
        public DisabilityCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DisabilityCategory
        /// </summary>
        /// <returns>List of DisabilityCategory</returns>
        public List<DisabilityCategoryViewModel> GetAll()
        {
            List<DisabilityCategoryViewModel> DisabilityCategoryList = new List<DisabilityCategoryViewModel>();
            Task<string> DisabilityCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDisabilityCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DisabilityCategory.Result != null)
                {
                    DisabilityCategoryList = JsonConvert.DeserializeObject<List<DisabilityCategoryViewModel>>(DisabilityCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DisabilityCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DisabilityCategoryCode">DisabilityCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DisabilityCategoryViewModel GetByUniqueCode(string Code)
        {
            DisabilityCategoryViewModel _object = new DisabilityCategoryViewModel();
            Task<string> DisabilityCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDisabilityCategory?DisabilityCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DisabilityCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DisabilityCategoryViewModel>(DisabilityCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DisabilityCategory and Return Updated DisabilityCategory
        /// </summary>
        /// <param name="DisabilityCategory">DisabilityCategory to Create</param>
        /// <returns>Updated DisabilityCategory</returns>
        public DisabilityCategoryViewModel Create(DisabilityCategoryViewModel DisabilityCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDisabilityCategory", DisabilityCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DisabilityCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DisabilityCategory and Return Updated DisabilityCategory
        /// </summary>
        /// <param name="DisabilityCategory">DisabilityCategory to Update</param>
        /// <returns>Updated DisabilityCategory</returns>
        public DisabilityCategoryViewModel Update(DisabilityCategoryViewModel DisabilityCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDisabilityCategory", DisabilityCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DisabilityCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}