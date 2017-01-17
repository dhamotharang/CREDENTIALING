using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ProviderProfileSectionService : IProviderProfileSectionService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ProviderProfileSectionService constructor For ServiceUtility
        /// </summary>
        public ProviderProfileSectionService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ProviderProfileSection
        /// </summary>
        /// <returns>List of ProviderProfileSection</returns>
        public List<ProviderProfileSectionViewModel> GetAll()
        {
            List<ProviderProfileSectionViewModel> ProviderProfileSectionList = new List<ProviderProfileSectionViewModel>();
            Task<string> ProviderProfileSection = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllProviderProfileSections?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ProviderProfileSection.Result != null)
                {
                    ProviderProfileSectionList = JsonConvert.DeserializeObject<List<ProviderProfileSectionViewModel>>(ProviderProfileSection.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProviderProfileSectionList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ProviderProfileSectionCode">ProviderProfileSection's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ProviderProfileSectionViewModel GetByUniqueCode(string Code)
        {
            ProviderProfileSectionViewModel _object = new ProviderProfileSectionViewModel();
            Task<string> ProviderProfileSection = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetProviderProfileSection?ProviderProfileSectionCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ProviderProfileSection.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ProviderProfileSectionViewModel>(ProviderProfileSection.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ProviderProfileSection and Return Updated ProviderProfileSection
        /// </summary>
        /// <param name="ProviderProfileSection">ProviderProfileSection to Create</param>
        /// <returns>Updated ProviderProfileSection</returns>
        public ProviderProfileSectionViewModel Create(ProviderProfileSectionViewModel ProviderProfileSection)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddProviderProfileSection", ProviderProfileSection);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderProfileSectionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ProviderProfileSection and Return Updated ProviderProfileSection
        /// </summary>
        /// <param name="ProviderProfileSection">ProviderProfileSection to Update</param>
        /// <returns>Updated ProviderProfileSection</returns>
        public ProviderProfileSectionViewModel Update(ProviderProfileSectionViewModel ProviderProfileSection)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateProviderProfileSection", ProviderProfileSection);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderProfileSectionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}