using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IClaimValueCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ClaimValueCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ClaimValueCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimValueCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        ClaimValueCodeViewModel Create(ClaimValueCodeViewModel ClaimValueCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimValueCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        ClaimValueCodeViewModel Update(ClaimValueCodeViewModel ClaimValueCode);

    }
}