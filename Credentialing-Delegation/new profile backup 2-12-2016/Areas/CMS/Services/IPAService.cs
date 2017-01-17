using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class IPAService : IIPAService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// IPAService constructor For ServiceUtility
        /// </summary>
        public IPAService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of IPA
        /// </summary>
        /// <returns>List of IPA</returns>
        public List<IPAViewModel> GetAll()
        {
            List<IPAViewModel> IPAList = new List<IPAViewModel>();
            Task<string> IPA = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllIPAs?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (IPA.Result != null)
                {
                    IPAList = JsonConvert.DeserializeObject<List<IPAViewModel>>(IPA.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return IPAList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="IPACode">IPA's Code Parameter</param>
        /// <returns>Object Type</returns>
        public IPAViewModel GetByUniqueCode(string Code)
        {
            IPAViewModel _object = new IPAViewModel();
            Task<string> IPA = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetIPA?IPACode=" + Code + "");
                return msg;
            });
            try
            {
                if (IPA.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<IPAViewModel>(IPA.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New IPA and Return Updated IPA
        /// </summary>
        /// <param name="IPA">IPA to Create</param>
        /// <returns>Updated IPA</returns>
        public IPAViewModel Create(IPAViewModel IPA)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddIPA", IPA);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<IPAViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update IPA and Return Updated IPA
        /// </summary>
        /// <param name="IPA">IPA to Update</param>
        /// <returns>Updated IPA</returns>
        public IPAViewModel Update(IPAViewModel IPA)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateIPA", IPA);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<IPAViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}