using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class SalutationService : ISalutationService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// SalutationService constructor For ServiceUtility
        /// </summary>
        public SalutationService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Salutation
        /// </summary>
        /// <returns>List of Salutation</returns>
        public List<SalutationViewModel> GetAll()
        {
            List<SalutationViewModel> SalutationList = new List<SalutationViewModel>();
            Task<string> Salutation = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllSalutations?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Salutation.Result != null)
                {
                    SalutationList = JsonConvert.DeserializeObject<List<SalutationViewModel>>(Salutation.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return SalutationList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="SalutationCode">Salutation's Code Parameter</param>
        /// <returns>Object Type</returns>
        public SalutationViewModel GetByUniqueCode(string Code)
        {
            SalutationViewModel _object = new SalutationViewModel();
            Task<string> Salutation = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetSalutation?SalutationCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Salutation.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<SalutationViewModel>(Salutation.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Salutation and Return Updated Salutation
        /// </summary>
        /// <param name="Salutation">Salutation to Create</param>
        /// <returns>Updated Salutation</returns>
        public SalutationViewModel Create(SalutationViewModel Salutation)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddSalutation", Salutation);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SalutationViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Salutation and Return Updated Salutation
        /// </summary>
        /// <param name="Salutation">Salutation to Update</param>
        /// <returns>Updated Salutation</returns>
        public SalutationViewModel Update(SalutationViewModel Salutation)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateSalutation", Salutation);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SalutationViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}