using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DisabilitiesService : IDisabilitiesService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DisabilitiesService constructor For ServiceUtility
        /// </summary>
        public DisabilitiesService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Disabilities
        /// </summary>
        /// <returns>List of Disabilities</returns>
        public List<DisabilitiesViewModel> GetAll()
        {
            List<DisabilitiesViewModel> DisabilitiesList = new List<DisabilitiesViewModel>();
            Task<string> Disabilities = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDisabilitiess?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Disabilities.Result != null)
                {
                    DisabilitiesList = JsonConvert.DeserializeObject<List<DisabilitiesViewModel>>(Disabilities.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DisabilitiesList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DisabilitiesCode">Disabilities's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DisabilitiesViewModel GetByUniqueCode(string Code)
        {
            DisabilitiesViewModel _object = new DisabilitiesViewModel();
            Task<string> Disabilities = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDisabilities?DisabilitiesCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Disabilities.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DisabilitiesViewModel>(Disabilities.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Disabilities and Return Updated Disabilities
        /// </summary>
        /// <param name="Disabilities">Disabilities to Create</param>
        /// <returns>Updated Disabilities</returns>
        public DisabilitiesViewModel Create(DisabilitiesViewModel Disabilities)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDisabilities", Disabilities);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DisabilitiesViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Disabilities and Return Updated Disabilities
        /// </summary>
        /// <param name="Disabilities">Disabilities to Update</param>
        /// <returns>Updated Disabilities</returns>
        public DisabilitiesViewModel Update(DisabilitiesViewModel Disabilities)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDisabilities", Disabilities);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DisabilitiesViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}