using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IClaimQueryCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ClaimQueryCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ClaimQueryCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimQueryCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        ClaimQueryCodeViewModel Create(ClaimQueryCodeViewModel ClaimQueryCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimQueryCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        ClaimQueryCodeViewModel Update(ClaimQueryCodeViewModel ClaimQueryCode);

    }
}