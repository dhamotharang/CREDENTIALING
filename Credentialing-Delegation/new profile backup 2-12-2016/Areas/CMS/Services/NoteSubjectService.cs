using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class NoteSubjectService : INoteSubjectService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// NoteSubjectService constructor For ServiceUtility
        /// </summary>
        public NoteSubjectService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of NoteSubject
        /// </summary>
        /// <returns>List of NoteSubject</returns>
        public List<NoteSubjectViewModel> GetAll()
        {
            List<NoteSubjectViewModel> NoteSubjectList = new List<NoteSubjectViewModel>();
            Task<string> NoteSubject = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllNoteSubjects?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (NoteSubject.Result != null)
                {
                    NoteSubjectList = JsonConvert.DeserializeObject<List<NoteSubjectViewModel>>(NoteSubject.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NoteSubjectList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="NoteSubjectCode">NoteSubject's Code Parameter</param>
        /// <returns>Object Type</returns>
        public NoteSubjectViewModel GetByUniqueCode(string Code)
        {
            NoteSubjectViewModel _object = new NoteSubjectViewModel();
            Task<string> NoteSubject = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetNoteSubject?NoteSubjectCode=" + Code + "");
                return msg;
            });
            try
            {
                if (NoteSubject.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<NoteSubjectViewModel>(NoteSubject.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New NoteSubject and Return Updated NoteSubject
        /// </summary>
        /// <param name="NoteSubject">NoteSubject to Create</param>
        /// <returns>Updated NoteSubject</returns>
        public NoteSubjectViewModel Create(NoteSubjectViewModel NoteSubject)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddNoteSubject", NoteSubject);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NoteSubjectViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update NoteSubject and Return Updated NoteSubject
        /// </summary>
        /// <param name="NoteSubject">NoteSubject to Update</param>
        /// <returns>Updated NoteSubject</returns>
        public NoteSubjectViewModel Update(NoteSubjectViewModel NoteSubject)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateNoteSubject", NoteSubject);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NoteSubjectViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}