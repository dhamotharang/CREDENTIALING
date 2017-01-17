using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class PlainLanguageService : IPlainLanguageService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// PlainLanguageService constructor For ServiceUtility
        /// </summary>
        public PlainLanguageService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of PlainLanguage
        /// </summary>
        /// <returns>List of PlainLanguage</returns>
        public List<PlainLanguageViewModel> GetAll()
        {
            List<PlainLanguageViewModel> PlainLanguageList = new List<PlainLanguageViewModel>();
            Task<string> PlainLanguage = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPlainLanguages?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (PlainLanguage.Result != null)
                {
                    PlainLanguageList = JsonConvert.DeserializeObject<List<PlainLanguageViewModel>>(PlainLanguage.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PlainLanguageList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="PlainLanguageCode">PlainLanguage's Code Parameter</param>
        /// <returns>Object Type</returns>
        public PlainLanguageViewModel GetByUniqueCode(string Code)
        {
            PlainLanguageViewModel _object = new PlainLanguageViewModel();
            Task<string> PlainLanguage = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPlainLanguage?PlainLanguageCode=" + Code + "");
                return msg;
            });
            try
            {
                if (PlainLanguage.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<PlainLanguageViewModel>(PlainLanguage.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New PlainLanguage and Return Updated PlainLanguage
        /// </summary>
        /// <param name="PlainLanguage">PlainLanguage to Create</param>
        /// <returns>Updated PlainLanguage</returns>
        public PlainLanguageViewModel Create(PlainLanguageViewModel PlainLanguage)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPlainLanguage", PlainLanguage);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PlainLanguageViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update PlainLanguage and Return Updated PlainLanguage
        /// </summary>
        /// <param name="PlainLanguage">PlainLanguage to Update</param>
        /// <returns>Updated PlainLanguage</returns>
        public PlainLanguageViewModel Update(PlainLanguageViewModel PlainLanguage)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePlainLanguage", PlainLanguage);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PlainLanguageViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}