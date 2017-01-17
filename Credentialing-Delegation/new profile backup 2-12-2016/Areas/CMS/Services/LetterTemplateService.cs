using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class LetterTemplateService : ILetterTemplateService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// LetterTemplateService constructor For ServiceUtility
        /// </summary>
        public LetterTemplateService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of LetterTemplate
        /// </summary>
        /// <returns>List of LetterTemplate</returns>
        public List<LetterTemplateViewModel> GetAll()
        {
            List<LetterTemplateViewModel> LetterTemplateList = new List<LetterTemplateViewModel>();
            Task<string> LetterTemplate = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllLetterTemplates?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (LetterTemplate.Result != null)
                {
                    LetterTemplateList = JsonConvert.DeserializeObject<List<LetterTemplateViewModel>>(LetterTemplate.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return LetterTemplateList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="LetterTemplateCode">LetterTemplate's Code Parameter</param>
        /// <returns>Object Type</returns>
        public LetterTemplateViewModel GetByUniqueCode(string Code)
        {
            LetterTemplateViewModel _object = new LetterTemplateViewModel();
            Task<string> LetterTemplate = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetLetterTemplate?LetterTemplateCode=" + Code + "");
                return msg;
            });
            try
            {
                if (LetterTemplate.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<LetterTemplateViewModel>(LetterTemplate.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New LetterTemplate and Return Updated LetterTemplate
        /// </summary>
        /// <param name="LetterTemplate">LetterTemplate to Create</param>
        /// <returns>Updated LetterTemplate</returns>
        public LetterTemplateViewModel Create(LetterTemplateViewModel LetterTemplate)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddLetterTemplate", LetterTemplate);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LetterTemplateViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update LetterTemplate and Return Updated LetterTemplate
        /// </summary>
        /// <param name="LetterTemplate">LetterTemplate to Update</param>
        /// <returns>Updated LetterTemplate</returns>
        public LetterTemplateViewModel Update(LetterTemplateViewModel LetterTemplate)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateLetterTemplate", LetterTemplate);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LetterTemplateViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}