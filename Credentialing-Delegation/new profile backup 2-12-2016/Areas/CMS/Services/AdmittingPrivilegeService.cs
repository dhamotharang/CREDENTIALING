using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class AdmittingPrivilegeService : IAdmittingPrivilegeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// AdmittingPrivilegeService constructor For ServiceUtility
        /// </summary>
        public AdmittingPrivilegeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of AdmittingPrivilege
        /// </summary>
        /// <returns>List of AdmittingPrivilege</returns>
        public List<AdmittingPrivilegeViewModel> GetAll()
        {
            List<AdmittingPrivilegeViewModel> AdmittingPrivilegeList = new List<AdmittingPrivilegeViewModel>();
            Task<string> AdmittingPrivilege = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllAdmittingPrivileges?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (AdmittingPrivilege.Result != null)
                {
                    AdmittingPrivilegeList = JsonConvert.DeserializeObject<List<AdmittingPrivilegeViewModel>>(AdmittingPrivilege.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return AdmittingPrivilegeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="AdmittingPrivilegeCode">AdmittingPrivilege's Code Parameter</param>
        /// <returns>Object Type</returns>
        public AdmittingPrivilegeViewModel GetByUniqueCode(string Code)
        {
            AdmittingPrivilegeViewModel _object = new AdmittingPrivilegeViewModel();
            Task<string> AdmittingPrivilege = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAdmittingPrivilege?AdmittingPrivilegeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (AdmittingPrivilege.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<AdmittingPrivilegeViewModel>(AdmittingPrivilege.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New AdmittingPrivilege and Return Updated AdmittingPrivilege
        /// </summary>
        /// <param name="AdmittingPrivilege">AdmittingPrivilege to Create</param>
        /// <returns>Updated AdmittingPrivilege</returns>
        public AdmittingPrivilegeViewModel Create(AdmittingPrivilegeViewModel AdmittingPrivilege)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddAdmittingPrivilege", AdmittingPrivilege);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdmittingPrivilegeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update AdmittingPrivilege and Return Updated AdmittingPrivilege
        /// </summary>
        /// <param name="AdmittingPrivilege">AdmittingPrivilege to Update</param>
        /// <returns>Updated AdmittingPrivilege</returns>
        public AdmittingPrivilegeViewModel Update(AdmittingPrivilegeViewModel AdmittingPrivilege)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateAdmittingPrivilege", AdmittingPrivilege);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<AdmittingPrivilegeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}