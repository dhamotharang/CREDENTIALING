using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ProviderRelationshipService : IProviderRelationshipService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ProviderRelationshipService constructor For ServiceUtility
        /// </summary>
        public ProviderRelationshipService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ProviderRelationship
        /// </summary>
        /// <returns>List of ProviderRelationship</returns>
        public List<ProviderRelationshipViewModel> GetAll()
        {
            List<ProviderRelationshipViewModel> ProviderRelationshipList = new List<ProviderRelationshipViewModel>();
            Task<string> ProviderRelationship = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllProviderRelationships?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ProviderRelationship.Result != null)
                {
                    ProviderRelationshipList = JsonConvert.DeserializeObject<List<ProviderRelationshipViewModel>>(ProviderRelationship.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProviderRelationshipList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ProviderRelationshipCode">ProviderRelationship's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ProviderRelationshipViewModel GetByUniqueCode(string Code)
        {
            ProviderRelationshipViewModel _object = new ProviderRelationshipViewModel();
            Task<string> ProviderRelationship = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetProviderRelationship?ProviderRelationshipCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ProviderRelationship.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ProviderRelationshipViewModel>(ProviderRelationship.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ProviderRelationship and Return Updated ProviderRelationship
        /// </summary>
        /// <param name="ProviderRelationship">ProviderRelationship to Create</param>
        /// <returns>Updated ProviderRelationship</returns>
        public ProviderRelationshipViewModel Create(ProviderRelationshipViewModel ProviderRelationship)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddProviderRelationship", ProviderRelationship);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderRelationshipViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ProviderRelationship and Return Updated ProviderRelationship
        /// </summary>
        /// <param name="ProviderRelationship">ProviderRelationship to Update</param>
        /// <returns>Updated ProviderRelationship</returns>
        public ProviderRelationshipViewModel Update(ProviderRelationshipViewModel ProviderRelationship)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateProviderRelationship", ProviderRelationship);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProviderRelationshipViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}