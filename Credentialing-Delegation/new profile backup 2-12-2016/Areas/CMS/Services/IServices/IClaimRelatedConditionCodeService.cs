using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IClaimRelatedConditionCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ClaimRelatedConditionCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ClaimRelatedConditionCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimRelatedConditionCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        ClaimRelatedConditionCodeViewModel Create(ClaimRelatedConditionCodeViewModel ClaimRelatedConditionCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimRelatedConditionCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        ClaimRelatedConditionCodeViewModel Update(ClaimRelatedConditionCodeViewModel ClaimRelatedConditionCode);

    }
}