using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DiseaseCategoryService : IDiseaseCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DiseaseCategoryService constructor For ServiceUtility
        /// </summary>
        public DiseaseCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DiseaseCategory
        /// </summary>
        /// <returns>List of DiseaseCategory</returns>
        public List<DiseaseCategoryViewModel> GetAll()
        {
            List<DiseaseCategoryViewModel> DiseaseCategoryList = new List<DiseaseCategoryViewModel>();
            Task<string> DiseaseCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDiseaseCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DiseaseCategory.Result != null)
                {
                    DiseaseCategoryList = JsonConvert.DeserializeObject<List<DiseaseCategoryViewModel>>(DiseaseCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DiseaseCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DiseaseCategoryCode">DiseaseCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DiseaseCategoryViewModel GetByUniqueCode(string Code)
        {
            DiseaseCategoryViewModel _object = new DiseaseCategoryViewModel();
            Task<string> DiseaseCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDiseaseCategory?DiseaseCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DiseaseCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DiseaseCategoryViewModel>(DiseaseCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DiseaseCategory and Return Updated DiseaseCategory
        /// </summary>
        /// <param name="DiseaseCategory">DiseaseCategory to Create</param>
        /// <returns>Updated DiseaseCategory</returns>
        public DiseaseCategoryViewModel Create(DiseaseCategoryViewModel DiseaseCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDiseaseCategory", DiseaseCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DiseaseCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DiseaseCategory and Return Updated DiseaseCategory
        /// </summary>
        /// <param name="DiseaseCategory">DiseaseCategory to Update</param>
        /// <returns>Updated DiseaseCategory</returns>
        public DiseaseCategoryViewModel Update(DiseaseCategoryViewModel DiseaseCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDiseaseCategory", DiseaseCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DiseaseCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}