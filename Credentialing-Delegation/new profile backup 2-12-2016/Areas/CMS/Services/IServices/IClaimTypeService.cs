using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IClaimTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ClaimTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ClaimTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ClaimTypeViewModel Create(ClaimTypeViewModel ClaimType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ClaimTypeViewModel Update(ClaimTypeViewModel ClaimType);

    }
}