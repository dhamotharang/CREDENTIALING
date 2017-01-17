using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IClaimFormStatusService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ClaimFormStatusViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ClaimFormStatusViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimFormStatus>Object to Create</param>
        /// <returns>Updated Object</returns>
        ClaimFormStatusViewModel Create(ClaimFormStatusViewModel ClaimFormStatus);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ClaimFormStatus>Object to Update</param>
        /// <returns>Updated Object</returns>
        ClaimFormStatusViewModel Update(ClaimFormStatusViewModel ClaimFormStatus);

    }
}