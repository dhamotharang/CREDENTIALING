using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class POSUmServiceGroupService : IPOSUmServiceGroupService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// POSUmServiceGroupService constructor For ServiceUtility
        /// </summary>
        public POSUmServiceGroupService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of POSUmServiceGroup
        /// </summary>
        /// <returns>List of POSUmServiceGroup</returns>
        public List<POSUmServiceGroupViewModel> GetAll()
        {
            List<POSUmServiceGroupViewModel> POSUmServiceGroupList = new List<POSUmServiceGroupViewModel>();
            Task<string> POSUmServiceGroup = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPOSUmServiceGroups?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (POSUmServiceGroup.Result != null)
                {
                    POSUmServiceGroupList = JsonConvert.DeserializeObject<List<POSUmServiceGroupViewModel>>(POSUmServiceGroup.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return POSUmServiceGroupList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="POSUmServiceGroupCode">POSUmServiceGroup's Code Parameter</param>
        /// <returns>Object Type</returns>
        public POSUmServiceGroupViewModel GetByUniqueCode(string Code)
        {
            POSUmServiceGroupViewModel _object = new POSUmServiceGroupViewModel();
            Task<string> POSUmServiceGroup = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPOSUmServiceGroup?POSUmServiceGroupCode=" + Code + "");
                return msg;
            });
            try
            {
                if (POSUmServiceGroup.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<POSUmServiceGroupViewModel>(POSUmServiceGroup.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New POSUmServiceGroup and Return Updated POSUmServiceGroup
        /// </summary>
        /// <param name="POSUmServiceGroup">POSUmServiceGroup to Create</param>
        /// <returns>Updated POSUmServiceGroup</returns>
        public POSUmServiceGroupViewModel Create(POSUmServiceGroupViewModel POSUmServiceGroup)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPOSUmServiceGroup", POSUmServiceGroup);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<POSUmServiceGroupViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update POSUmServiceGroup and Return Updated POSUmServiceGroup
        /// </summary>
        /// <param name="POSUmServiceGroup">POSUmServiceGroup to Update</param>
        /// <returns>Updated POSUmServiceGroup</returns>
        public POSUmServiceGroupViewModel Update(POSUmServiceGroupViewModel POSUmServiceGroup)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePOSUmServiceGroup", POSUmServiceGroup);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<POSUmServiceGroupViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}