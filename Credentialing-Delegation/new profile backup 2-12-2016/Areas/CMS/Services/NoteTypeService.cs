using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class NoteTypeService : INoteTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// NoteTypeService constructor For ServiceUtility
        /// </summary>
        public NoteTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of NoteType
        /// </summary>
        /// <returns>List of NoteType</returns>
        public List<NoteTypeViewModel> GetAll()
        {
            List<NoteTypeViewModel> NoteTypeList = new List<NoteTypeViewModel>();
            Task<string> NoteType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllNoteTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (NoteType.Result != null)
                {
                    NoteTypeList = JsonConvert.DeserializeObject<List<NoteTypeViewModel>>(NoteType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NoteTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="NoteTypeCode">NoteType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public NoteTypeViewModel GetByUniqueCode(string Code)
        {
            NoteTypeViewModel _object = new NoteTypeViewModel();
            Task<string> NoteType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetNoteType?NoteTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (NoteType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<NoteTypeViewModel>(NoteType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New NoteType and Return Updated NoteType
        /// </summary>
        /// <param name="NoteType">NoteType to Create</param>
        /// <returns>Updated NoteType</returns>
        public NoteTypeViewModel Create(NoteTypeViewModel NoteType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddNoteType", NoteType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NoteTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update NoteType and Return Updated NoteType
        /// </summary>
        /// <param name="NoteType">NoteType to Update</param>
        /// <returns>Updated NoteType</returns>
        public NoteTypeViewModel Update(NoteTypeViewModel NoteType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateNoteType", NoteType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NoteTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}