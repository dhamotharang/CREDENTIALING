using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IMaritalStatusService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<MaritalStatusViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        MaritalStatusViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=MaritalStatus>Object to Create</param>
        /// <returns>Updated Object</returns>
        MaritalStatusViewModel Create(MaritalStatusViewModel MaritalStatus);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=MaritalStatus>Object to Update</param>
        /// <returns>Updated Object</returns>
        MaritalStatusViewModel Update(MaritalStatusViewModel MaritalStatus);

    }
}