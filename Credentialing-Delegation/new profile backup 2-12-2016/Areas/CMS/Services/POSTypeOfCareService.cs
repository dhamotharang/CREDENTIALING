using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class POSTypeOfCareService : IPOSTypeOfCareService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// POSTypeOfCareService constructor For ServiceUtility
        /// </summary>
        public POSTypeOfCareService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of POSTypeOfCare
        /// </summary>
        /// <returns>List of POSTypeOfCare</returns>
        public List<POSTypeOfCareViewModel> GetAll()
        {
            List<POSTypeOfCareViewModel> POSTypeOfCareList = new List<POSTypeOfCareViewModel>();
            Task<string> POSTypeOfCare = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPOSTypeOfCares?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (POSTypeOfCare.Result != null)
                {
                    POSTypeOfCareList = JsonConvert.DeserializeObject<List<POSTypeOfCareViewModel>>(POSTypeOfCare.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return POSTypeOfCareList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="POSTypeOfCareCode">POSTypeOfCare's Code Parameter</param>
        /// <returns>Object Type</returns>
        public POSTypeOfCareViewModel GetByUniqueCode(string Code)
        {
            POSTypeOfCareViewModel _object = new POSTypeOfCareViewModel();
            Task<string> POSTypeOfCare = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPOSTypeOfCare?POSTypeOfCareCode=" + Code + "");
                return msg;
            });
            try
            {
                if (POSTypeOfCare.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<POSTypeOfCareViewModel>(POSTypeOfCare.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New POSTypeOfCare and Return Updated POSTypeOfCare
        /// </summary>
        /// <param name="POSTypeOfCare">POSTypeOfCare to Create</param>
        /// <returns>Updated POSTypeOfCare</returns>
        public POSTypeOfCareViewModel Create(POSTypeOfCareViewModel POSTypeOfCare)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPOSTypeOfCare", POSTypeOfCare);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<POSTypeOfCareViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update POSTypeOfCare and Return Updated POSTypeOfCare
        /// </summary>
        /// <param name="POSTypeOfCare">POSTypeOfCare to Update</param>
        /// <returns>Updated POSTypeOfCare</returns>
        public POSTypeOfCareViewModel Update(POSTypeOfCareViewModel POSTypeOfCare)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePOSTypeOfCare", POSTypeOfCare);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<POSTypeOfCareViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}