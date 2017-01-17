using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ProviderProfileSubSectionService : IProviderProfileSubSectionService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ProviderProfileSubSectionService constructor For ServiceUtility
        /// </summary>
        public ProviderProfileSubSectionService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ProviderProfileSubSection
        /// </summary>
        /// <returns>List of ProviderProfileSubSection</returns>
        public List<ProviderProfileSubSectionViewModel> GetAll()
        {
            List<ProviderProfileSubSectionViewModel> ProviderProfileSubSectionList = new List<ProviderProfileSubSectionViewModel>();
            Task<string> ProviderProfileSubSection = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllProviderProfileSubSections?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ProviderProfileSubSection.Result != null)
                {
                    ProviderProfileSubSectionList = JsonConvert.DeserializeObject<List<ProviderProfileSubSectionViewModel>>(ProviderProfileSubSection.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProviderProfileSubSectionList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ProviderProfileSubSectionCode">ProviderProfileSubSection's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ProviderProfileSubSectionViewModel GetByUniqueCode(string Code)
        {
            ProviderProfileSubSectionViewModel _object = new ProviderProfileSubSectionViewModel();
            Task<string> ProviderProfileSubSection = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetProviderProfileSubSection?ProviderProfileSubSectionCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ProviderProfileSubSection.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ProviderProfileSubSectionViewModel>(ProviderProfileSubSection.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ProviderProfileSubSection and Return Updated ProviderProfileSubSection
        /// </summary>
        /// <param name="ProviderProfileSubSection">ProviderProfileSubSection to Create</param>
        /// <returns>Updated ProviderProfileSubSection</returns>
        public ProviderProfileSubSectionViewModel Create(ProviderProfileSubSectionViewModel ProviderProfileSubSection)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddProviderProfileSubSection", ProviderProfileSubSection);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderProfileSubSectionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ProviderProfileSubSection and Return Updated ProviderProfileSubSection
        /// </summary>
        /// <param name="ProviderProfileSubSection">ProviderProfileSubSection to Update</param>
        /// <returns>Updated ProviderProfileSubSection</returns>
        public ProviderProfileSubSectionViewModel Update(ProviderProfileSubSectionViewModel ProviderProfileSubSection)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateProviderProfileSubSection", ProviderProfileSubSection);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderProfileSubSectionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}