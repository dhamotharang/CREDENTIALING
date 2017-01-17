using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class OutcomeTypeService : IOutcomeTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// OutcomeTypeService constructor For ServiceUtility
        /// </summary>
        public OutcomeTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of OutcomeType
        /// </summary>
        /// <returns>List of OutcomeType</returns>
        public List<OutcomeTypeViewModel> GetAll()
        {
            List<OutcomeTypeViewModel> OutcomeTypeList = new List<OutcomeTypeViewModel>();
            Task<string> OutcomeType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllOutcomeTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (OutcomeType.Result != null)
                {
                    OutcomeTypeList = JsonConvert.DeserializeObject<List<OutcomeTypeViewModel>>(OutcomeType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OutcomeTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="OutcomeTypeCode">OutcomeType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public OutcomeTypeViewModel GetByUniqueCode(string Code)
        {
            OutcomeTypeViewModel _object = new OutcomeTypeViewModel();
            Task<string> OutcomeType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetOutcomeType?OutcomeTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (OutcomeType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<OutcomeTypeViewModel>(OutcomeType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New OutcomeType and Return Updated OutcomeType
        /// </summary>
        /// <param name="OutcomeType">OutcomeType to Create</param>
        /// <returns>Updated OutcomeType</returns>
        public OutcomeTypeViewModel Create(OutcomeTypeViewModel OutcomeType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddOutcomeType", OutcomeType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<OutcomeTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update OutcomeType and Return Updated OutcomeType
        /// </summary>
        /// <param name="OutcomeType">OutcomeType to Update</param>
        /// <returns>Updated OutcomeType</returns>
        public OutcomeTypeViewModel Update(OutcomeTypeViewModel OutcomeType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateOutcomeType", OutcomeType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<OutcomeTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}