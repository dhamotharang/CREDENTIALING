using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IClaimFrequencyCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ClaimFrequencyCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ClaimFrequencyCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimFrequencyCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        ClaimFrequencyCodeViewModel Create(ClaimFrequencyCodeViewModel ClaimFrequencyCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimFrequencyCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        ClaimFrequencyCodeViewModel Update(ClaimFrequencyCodeViewModel ClaimFrequencyCode);

    }
}