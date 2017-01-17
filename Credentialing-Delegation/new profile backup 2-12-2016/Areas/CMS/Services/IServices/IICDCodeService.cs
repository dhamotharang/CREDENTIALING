using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IICDCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ICDCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ICDCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ICDCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        ICDCodeViewModel Create(ICDCodeViewModel ICDCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ICDCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        ICDCodeViewModel Update(ICDCodeViewModel ICDCode);

    }
}