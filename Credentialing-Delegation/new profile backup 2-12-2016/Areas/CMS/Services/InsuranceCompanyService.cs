using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class InsuranceCompanyService : IInsuranceCompanyService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// InsuranceCompanyService constructor For ServiceUtility
        /// </summary>
        public InsuranceCompanyService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of InsuranceCompany
        /// </summary>
        /// <returns>List of InsuranceCompany</returns>
        public List<InsuranceCompanyViewModel> GetAll()
        {
            List<InsuranceCompanyViewModel> InsuranceCompanyList = new List<InsuranceCompanyViewModel>();
            Task<string> InsuranceCompany = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllInsuranceCompanys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (InsuranceCompany.Result != null)
                {
                    InsuranceCompanyList = JsonConvert.DeserializeObject<List<InsuranceCompanyViewModel>>(InsuranceCompany.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return InsuranceCompanyList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="InsuranceCompanyCode">InsuranceCompany's Code Parameter</param>
        /// <returns>Object Type</returns>
        public InsuranceCompanyViewModel GetByUniqueCode(string Code)
        {
            InsuranceCompanyViewModel _object = new InsuranceCompanyViewModel();
            Task<string> InsuranceCompany = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetInsuranceCompany?InsuranceCompanyCode=" + Code + "");
                return msg;
            });
            try
            {
                if (InsuranceCompany.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<InsuranceCompanyViewModel>(InsuranceCompany.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New InsuranceCompany and Return Updated InsuranceCompany
        /// </summary>
        /// <param name="InsuranceCompany">InsuranceCompany to Create</param>
        /// <returns>Updated InsuranceCompany</returns>
        public InsuranceCompanyViewModel Create(InsuranceCompanyViewModel InsuranceCompany)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddInsuranceCompany", InsuranceCompany);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<InsuranceCompanyViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update InsuranceCompany and Return Updated InsuranceCompany
        /// </summary>
        /// <param name="InsuranceCompany">InsuranceCompany to Update</param>
        /// <returns>Updated InsuranceCompany</returns>
        public InsuranceCompanyViewModel Update(InsuranceCompanyViewModel InsuranceCompany)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateInsuranceCompany", InsuranceCompany);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<InsuranceCompanyViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}