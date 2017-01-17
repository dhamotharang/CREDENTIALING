using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class NoteDecisionTypeService : INoteDecisionTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// NoteDecisionTypeService constructor For ServiceUtility
        /// </summary>
        public NoteDecisionTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of NoteDecisionType
        /// </summary>
        /// <returns>List of NoteDecisionType</returns>
        public List<NoteDecisionTypeViewModel> GetAll()
        {
            List<NoteDecisionTypeViewModel> NoteDecisionTypeList = new List<NoteDecisionTypeViewModel>();
            Task<string> NoteDecisionType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllNoteDecisionTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (NoteDecisionType.Result != null)
                {
                    NoteDecisionTypeList = JsonConvert.DeserializeObject<List<NoteDecisionTypeViewModel>>(NoteDecisionType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return NoteDecisionTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="NoteDecisionTypeCode">NoteDecisionType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public NoteDecisionTypeViewModel GetByUniqueCode(string Code)
        {
            NoteDecisionTypeViewModel _object = new NoteDecisionTypeViewModel();
            Task<string> NoteDecisionType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetNoteDecisionType?NoteDecisionTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (NoteDecisionType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<NoteDecisionTypeViewModel>(NoteDecisionType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New NoteDecisionType and Return Updated NoteDecisionType
        /// </summary>
        /// <param name="NoteDecisionType">NoteDecisionType to Create</param>
        /// <returns>Updated NoteDecisionType</returns>
        public NoteDecisionTypeViewModel Create(NoteDecisionTypeViewModel NoteDecisionType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddNoteDecisionType", NoteDecisionType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NoteDecisionTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update NoteDecisionType and Return Updated NoteDecisionType
        /// </summary>
        /// <param name="NoteDecisionType">NoteDecisionType to Update</param>
        /// <returns>Updated NoteDecisionType</returns>
        public NoteDecisionTypeViewModel Update(NoteDecisionTypeViewModel NoteDecisionType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateNoteDecisionType", NoteDecisionType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<NoteDecisionTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}