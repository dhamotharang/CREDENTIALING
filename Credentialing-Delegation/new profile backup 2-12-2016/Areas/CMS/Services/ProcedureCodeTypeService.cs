using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ProcedureCodeTypeService : IProcedureCodeTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ProcedureCodeTypeService constructor For ServiceUtility
        /// </summary>
        public ProcedureCodeTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ProcedureCodeType
        /// </summary>
        /// <returns>List of ProcedureCodeType</returns>
        public List<ProcedureCodeTypeViewModel> GetAll()
        {
            List<ProcedureCodeTypeViewModel> ProcedureCodeTypeList = new List<ProcedureCodeTypeViewModel>();
            Task<string> ProcedureCodeType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllProcedureCodeTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ProcedureCodeType.Result != null)
                {
                    ProcedureCodeTypeList = JsonConvert.DeserializeObject<List<ProcedureCodeTypeViewModel>>(ProcedureCodeType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ProcedureCodeTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ProcedureCodeTypeCode">ProcedureCodeType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ProcedureCodeTypeViewModel GetByUniqueCode(string Code)
        {
            ProcedureCodeTypeViewModel _object = new ProcedureCodeTypeViewModel();
            Task<string> ProcedureCodeType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetProcedureCodeType?ProcedureCodeTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ProcedureCodeType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ProcedureCodeTypeViewModel>(ProcedureCodeType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ProcedureCodeType and Return Updated ProcedureCodeType
        /// </summary>
        /// <param name="ProcedureCodeType">ProcedureCodeType to Create</param>
        /// <returns>Updated ProcedureCodeType</returns>
        public ProcedureCodeTypeViewModel Create(ProcedureCodeTypeViewModel ProcedureCodeType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddProcedureCodeType", ProcedureCodeType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProcedureCodeTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ProcedureCodeType and Return Updated ProcedureCodeType
        /// </summary>
        /// <param name="ProcedureCodeType">ProcedureCodeType to Update</param>
        /// <returns>Updated ProcedureCodeType</returns>
        public ProcedureCodeTypeViewModel Update(ProcedureCodeTypeViewModel ProcedureCodeType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateProcedureCodeType", ProcedureCodeType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ProcedureCodeTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}