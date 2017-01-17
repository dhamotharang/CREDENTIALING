using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class VerificationLinkService : IVerificationLinkService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// VerificationLinkService constructor For ServiceUtility
        /// </summary>
        public VerificationLinkService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of VerificationLink
        /// </summary>
        /// <returns>List of VerificationLink</returns>
        public List<VerificationLinkViewModel> GetAll()
        {
            List<VerificationLinkViewModel> VerificationLinkList = new List<VerificationLinkViewModel>();
            Task<string> VerificationLink = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllVerificationLinks?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (VerificationLink.Result != null)
                {
                    VerificationLinkList = JsonConvert.DeserializeObject<List<VerificationLinkViewModel>>(VerificationLink.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return VerificationLinkList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="VerificationLinkCode">VerificationLink's Code Parameter</param>
        /// <returns>Object Type</returns>
        public VerificationLinkViewModel GetByUniqueCode(string Code)
        {
            VerificationLinkViewModel _object = new VerificationLinkViewModel();
            Task<string> VerificationLink = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetVerificationLink?VerificationLinkCode=" + Code + "");
                return msg;
            });
            try
            {
                if (VerificationLink.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<VerificationLinkViewModel>(VerificationLink.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New VerificationLink and Return Updated VerificationLink
        /// </summary>
        /// <param name="VerificationLink">VerificationLink to Create</param>
        /// <returns>Updated VerificationLink</returns>
        public VerificationLinkViewModel Create(VerificationLinkViewModel VerificationLink)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddVerificationLink", VerificationLink);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VerificationLinkViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update VerificationLink and Return Updated VerificationLink
        /// </summary>
        /// <param name="VerificationLink">VerificationLink to Update</param>
        /// <returns>Updated VerificationLink</returns>
        public VerificationLinkViewModel Update(VerificationLinkViewModel VerificationLink)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateVerificationLink", VerificationLink);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<VerificationLinkViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}