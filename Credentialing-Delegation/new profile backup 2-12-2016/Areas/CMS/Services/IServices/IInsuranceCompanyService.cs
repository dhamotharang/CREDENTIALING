using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IInsuranceCompanyService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<InsuranceCompanyViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        InsuranceCompanyViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=InsuranceCompany>Object to Create</param>
        /// <returns>Updated Object</returns>
        InsuranceCompanyViewModel Create(InsuranceCompanyViewModel InsuranceCompany);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=InsuranceCompany>Object to Update</param>
        /// <returns>Updated Object</returns>
        InsuranceCompanyViewModel Update(InsuranceCompanyViewModel InsuranceCompany);

    }
}