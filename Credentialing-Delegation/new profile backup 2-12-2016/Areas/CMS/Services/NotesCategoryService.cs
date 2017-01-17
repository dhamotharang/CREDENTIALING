using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class NotesCategoryService : INotesCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// NotesCategoryService constructor For ServiceUtility
        /// </summary>
        public NotesCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of NotesCategory
        /// </summary>
        /// <returns>List of NotesCategory</returns>
        public List<NotesCategoryViewModel> GetAll()
        {
            List<NotesCategoryViewModel> NotesCategoryList = new List<NotesCategoryViewModel>();
            Task<string> NotesCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllNotesCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (NotesCategory.Result != null)
                {
                    NotesCategoryList = JsonConvert.DeserializeObject<List<NotesCategoryViewModel>>(NotesCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NotesCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="NotesCategoryCode">NotesCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public NotesCategoryViewModel GetByUniqueCode(string Code)
        {
            NotesCategoryViewModel _object = new NotesCategoryViewModel();
            Task<string> NotesCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetNotesCategory?NotesCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (NotesCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<NotesCategoryViewModel>(NotesCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New NotesCategory and Return Updated NotesCategory
        /// </summary>
        /// <param name="NotesCategory">NotesCategory to Create</param>
        /// <returns>Updated NotesCategory</returns>
        public NotesCategoryViewModel Create(NotesCategoryViewModel NotesCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddNotesCategory", NotesCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NotesCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update NotesCategory and Return Updated NotesCategory
        /// </summary>
        /// <param name="NotesCategory">NotesCategory to Update</param>
        /// <returns>Updated NotesCategory</returns>
        public NotesCategoryViewModel Update(NotesCategoryViewModel NotesCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateNotesCategory", NotesCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NotesCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}