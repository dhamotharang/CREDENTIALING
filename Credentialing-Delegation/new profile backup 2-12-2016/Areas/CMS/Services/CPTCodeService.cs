using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class CPTCodeService : ICPTCodeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// CPTCodeService constructor For ServiceUtility
        /// </summary>
        public CPTCodeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of CPTCode
        /// </summary>
        /// <returns>List of CPTCode</returns>
        public List<CPTCodeViewModel> GetAll()
        {
            List<CPTCodeViewModel> CPTCodeList = new List<CPTCodeViewModel>();
            Task<string> CPTCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllCPTCodesBySearchString?searchString=369&IncludedInactive=true");
                return msg;
            });
            try
            {
                if (CPTCode.Result != null)
                {
                    CPTCodeList = JsonConvert.DeserializeObject<List<CPTCodeViewModel>>(CPTCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return CPTCodeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="CPTCodeCode">CPTCode's Code Parameter</param>
        /// <returns>Object Type</returns>
        public CPTCodeViewModel GetByUniqueCode(string Code)
        {
            CPTCodeViewModel _object = new CPTCodeViewModel();
            Task<string> CPTCode = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetCPTCode?CPTCodeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (CPTCode.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<CPTCodeViewModel>(CPTCode.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New CPTCode and Return Updated CPTCode
        /// </summary>
        /// <param name="CPTCode">CPTCode to Create</param>
        /// <returns>Updated CPTCode</returns>
        public CPTCodeViewModel Create(CPTCodeViewModel CPTCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddCPTCode", CPTCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CPTCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update CPTCode and Return Updated CPTCode
        /// </summary>
        /// <param name="CPTCode">CPTCode to Update</param>
        /// <returns>Updated CPTCode</returns>
        public CPTCodeViewModel Update(CPTCodeViewModel CPTCode)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateCPTCode", CPTCode);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<CPTCodeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}