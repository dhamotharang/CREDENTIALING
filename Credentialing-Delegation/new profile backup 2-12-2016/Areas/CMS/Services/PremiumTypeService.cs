using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class PremiumTypeService : IPremiumTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// PremiumTypeService constructor For ServiceUtility
        /// </summary>
        public PremiumTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of PremiumType
        /// </summary>
        /// <returns>List of PremiumType</returns>
        public List<PremiumTypeViewModel> GetAll()
        {
            List<PremiumTypeViewModel> PremiumTypeList = new List<PremiumTypeViewModel>();
            Task<string> PremiumType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPremiumTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (PremiumType.Result != null)
                {
                    PremiumTypeList = JsonConvert.DeserializeObject<List<PremiumTypeViewModel>>(PremiumType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PremiumTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="PremiumTypeCode">PremiumType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public PremiumTypeViewModel GetByUniqueCode(string Code)
        {
            PremiumTypeViewModel _object = new PremiumTypeViewModel();
            Task<string> PremiumType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPremiumType?PremiumTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (PremiumType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<PremiumTypeViewModel>(PremiumType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New PremiumType and Return Updated PremiumType
        /// </summary>
        /// <param name="PremiumType">PremiumType to Create</param>
        /// <returns>Updated PremiumType</returns>
        public PremiumTypeViewModel Create(PremiumTypeViewModel PremiumType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPremiumType", PremiumType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PremiumTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update PremiumType and Return Updated PremiumType
        /// </summary>
        /// <param name="PremiumType">PremiumType to Update</param>
        /// <returns>Updated PremiumType</returns>
        public PremiumTypeViewModel Update(PremiumTypeViewModel PremiumType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePremiumType", PremiumType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PremiumTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}