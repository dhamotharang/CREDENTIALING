using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class NotesTemplateService : INotesTemplateService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// NotesTemplateService constructor For ServiceUtility
        /// </summary>
        public NotesTemplateService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of NotesTemplate
        /// </summary>
        /// <returns>List of NotesTemplate</returns>
        public List<NotesTemplateViewModel> GetAll()
        {
            List<NotesTemplateViewModel> NotesTemplateList = new List<NotesTemplateViewModel>();
            Task<string> NotesTemplate = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllNotesTemplates?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (NotesTemplate.Result != null)
                {
                    NotesTemplateList = JsonConvert.DeserializeObject<List<NotesTemplateViewModel>>(NotesTemplate.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NotesTemplateList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="NotesTemplateCode">NotesTemplate's Code Parameter</param>
        /// <returns>Object Type</returns>
        public NotesTemplateViewModel GetByUniqueCode(string Code)
        {
            NotesTemplateViewModel _object = new NotesTemplateViewModel();
            Task<string> NotesTemplate = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetNotesTemplate?NotesTemplateCode=" + Code + "");
                return msg;
            });
            try
            {
                if (NotesTemplate.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<NotesTemplateViewModel>(NotesTemplate.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New NotesTemplate and Return Updated NotesTemplate
        /// </summary>
        /// <param name="NotesTemplate">NotesTemplate to Create</param>
        /// <returns>Updated NotesTemplate</returns>
        public NotesTemplateViewModel Create(NotesTemplateViewModel NotesTemplate)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddNotesTemplate", NotesTemplate);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NotesTemplateViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update NotesTemplate and Return Updated NotesTemplate
        /// </summary>
        /// <param name="NotesTemplate">NotesTemplate to Update</param>
        /// <returns>Updated NotesTemplate</returns>
        public NotesTemplateViewModel Update(NotesTemplateViewModel NotesTemplate)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateNotesTemplate", NotesTemplate);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NotesTemplateViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}