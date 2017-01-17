using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DiseaseNameService : IDiseaseNameService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DiseaseNameService constructor For ServiceUtility
        /// </summary>
        public DiseaseNameService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DiseaseName
        /// </summary>
        /// <returns>List of DiseaseName</returns>
        public List<DiseaseNameViewModel> GetAll()
        {
            List<DiseaseNameViewModel> DiseaseNameList = new List<DiseaseNameViewModel>();
            Task<string> DiseaseName = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDiseaseNames?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DiseaseName.Result != null)
                {
                    DiseaseNameList = JsonConvert.DeserializeObject<List<DiseaseNameViewModel>>(DiseaseName.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DiseaseNameList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DiseaseNameCode">DiseaseName's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DiseaseNameViewModel GetByUniqueCode(string Code)
        {
            DiseaseNameViewModel _object = new DiseaseNameViewModel();
            Task<string> DiseaseName = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDiseaseName?DiseaseNameCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DiseaseName.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DiseaseNameViewModel>(DiseaseName.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DiseaseName and Return Updated DiseaseName
        /// </summary>
        /// <param name="DiseaseName">DiseaseName to Create</param>
        /// <returns>Updated DiseaseName</returns>
        public DiseaseNameViewModel Create(DiseaseNameViewModel DiseaseName)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDiseaseName", DiseaseName);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DiseaseNameViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DiseaseName and Return Updated DiseaseName
        /// </summary>
        /// <param name="DiseaseName">DiseaseName to Update</param>
        /// <returns>Updated DiseaseName</returns>
        public DiseaseNameViewModel Update(DiseaseNameViewModel DiseaseName)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDiseaseName", DiseaseName);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DiseaseNameViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}