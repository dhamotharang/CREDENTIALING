using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class LanguageService : ILanguageService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// LanguageService constructor For ServiceUtility
        /// </summary>
        public LanguageService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Language
        /// </summary>
        /// <returns>List of Language</returns>
        public List<LanguageViewModel> GetAll()
        {
            List<LanguageViewModel> LanguageList = new List<LanguageViewModel>();
            Task<string> Language = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllLanguages?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Language.Result != null)
                {
                    LanguageList = JsonConvert.DeserializeObject<List<LanguageViewModel>>(Language.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return LanguageList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="LanguageCode">Language's Code Parameter</param>
        /// <returns>Object Type</returns>
        public LanguageViewModel GetByUniqueCode(string Code)
        {
            LanguageViewModel _object = new LanguageViewModel();
            Task<string> Language = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetLanguage?LanguageCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Language.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<LanguageViewModel>(Language.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Language and Return Updated Language
        /// </summary>
        /// <param name="Language">Language to Create</param>
        /// <returns>Updated Language</returns>
        public LanguageViewModel Create(LanguageViewModel Language)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddLanguage", Language);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LanguageViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Language and Return Updated Language
        /// </summary>
        /// <param name="Language">Language to Update</param>
        /// <returns>Updated Language</returns>
        public LanguageViewModel Update(LanguageViewModel Language)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateLanguage", Language);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LanguageViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}