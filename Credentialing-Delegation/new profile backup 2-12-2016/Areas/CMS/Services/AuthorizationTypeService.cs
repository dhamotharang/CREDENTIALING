using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class AuthorizationTypeService : IAuthorizationTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// AuthorizationTypeService constructor For ServiceUtility
        /// </summary>
        public AuthorizationTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of AuthorizationType
        /// </summary>
        /// <returns>List of AuthorizationType</returns>
        public List<AuthorizationTypeViewModel> GetAll()
        {
            List<AuthorizationTypeViewModel> AuthorizationTypeList = new List<AuthorizationTypeViewModel>();
            Task<string> AuthorizationType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllAuthorizationTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (AuthorizationType.Result != null)
                {
                    AuthorizationTypeList = JsonConvert.DeserializeObject<List<AuthorizationTypeViewModel>>(AuthorizationType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return AuthorizationTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="AuthorizationTypeCode">AuthorizationType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public AuthorizationTypeViewModel GetByUniqueCode(string Code)
        {
            AuthorizationTypeViewModel _object = new AuthorizationTypeViewModel();
            Task<string> AuthorizationType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAuthorizationType?AuthorizationTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (AuthorizationType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<AuthorizationTypeViewModel>(AuthorizationType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New AuthorizationType and Return Updated AuthorizationType
        /// </summary>
        /// <param name="AuthorizationType">AuthorizationType to Create</param>
        /// <returns>Updated AuthorizationType</returns>
        public AuthorizationTypeViewModel Create(AuthorizationTypeViewModel AuthorizationType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddAuthorizationType", AuthorizationType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AuthorizationTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update AuthorizationType and Return Updated AuthorizationType
        /// </summary>
        /// <param name="AuthorizationType">AuthorizationType to Update</param>
        /// <returns>Updated AuthorizationType</returns>
        public AuthorizationTypeViewModel Update(AuthorizationTypeViewModel AuthorizationType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateAuthorizationType", AuthorizationType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AuthorizationTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}