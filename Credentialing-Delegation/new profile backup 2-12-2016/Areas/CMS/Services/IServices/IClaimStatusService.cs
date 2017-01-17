using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IClaimStatusService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ClaimStatusViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ClaimStatusViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimStatus>Object to Create</param>
        /// <returns>Updated Object</returns>
        ClaimStatusViewModel Create(ClaimStatusViewModel ClaimStatus);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimStatus>Object to Update</param>
        /// <returns>Updated Object</returns>
        ClaimStatusViewModel Update(ClaimStatusViewModel ClaimStatus);

    }
}