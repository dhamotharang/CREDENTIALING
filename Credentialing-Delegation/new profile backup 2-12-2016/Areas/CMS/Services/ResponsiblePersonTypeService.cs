using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ResponsiblePersonTypeService : IResponsiblePersonTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ResponsiblePersonTypeService constructor For ServiceUtility
        /// </summary>
        public ResponsiblePersonTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ResponsiblePersonType
        /// </summary>
        /// <returns>List of ResponsiblePersonType</returns>
        public List<ResponsiblePersonTypeViewModel> GetAll()
        {
            List<ResponsiblePersonTypeViewModel> ResponsiblePersonTypeList = new List<ResponsiblePersonTypeViewModel>();
            Task<string> ResponsiblePersonType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllResponsiblePersonTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ResponsiblePersonType.Result != null)
                {
                    ResponsiblePersonTypeList = JsonConvert.DeserializeObject<List<ResponsiblePersonTypeViewModel>>(ResponsiblePersonType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ResponsiblePersonTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ResponsiblePersonTypeCode">ResponsiblePersonType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ResponsiblePersonTypeViewModel GetByUniqueCode(string Code)
        {
            ResponsiblePersonTypeViewModel _object = new ResponsiblePersonTypeViewModel();
            Task<string> ResponsiblePersonType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetResponsiblePersonType?ResponsiblePersonTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ResponsiblePersonType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ResponsiblePersonTypeViewModel>(ResponsiblePersonType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ResponsiblePersonType and Return Updated ResponsiblePersonType
        /// </summary>
        /// <param name="ResponsiblePersonType">ResponsiblePersonType to Create</param>
        /// <returns>Updated ResponsiblePersonType</returns>
        public ResponsiblePersonTypeViewModel Create(ResponsiblePersonTypeViewModel ResponsiblePersonType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddResponsiblePersonType", ResponsiblePersonType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ResponsiblePersonTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ResponsiblePersonType and Return Updated ResponsiblePersonType
        /// </summary>
        /// <param name="ResponsiblePersonType">ResponsiblePersonType to Update</param>
        /// <returns>Updated ResponsiblePersonType</returns>
        public ResponsiblePersonTypeViewModel Update(ResponsiblePersonTypeViewModel ResponsiblePersonType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateResponsiblePersonType", ResponsiblePersonType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ResponsiblePersonTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}