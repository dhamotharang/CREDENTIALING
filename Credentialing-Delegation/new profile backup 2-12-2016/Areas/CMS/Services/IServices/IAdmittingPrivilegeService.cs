using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IAdmittingPrivilegeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<AdmittingPrivilegeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        AdmittingPrivilegeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=AdmittingPrivilege>Object to Create</param>
        /// <returns>Updated Object</returns>
        AdmittingPrivilegeViewModel Create(AdmittingPrivilegeViewModel AdmittingPrivilege);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=AdmittingPrivilege>Object to Update</param>
        /// <returns>Updated Object</returns>
        AdmittingPrivilegeViewModel Update(AdmittingPrivilegeViewModel AdmittingPrivilege);

    }
}