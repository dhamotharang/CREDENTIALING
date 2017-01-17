using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDecredentialingReasonService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DecredentialingReasonViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DecredentialingReasonViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DecredentialingReason>Object to Create</param>
        /// <returns>Updated Object</returns>
        DecredentialingReasonViewModel Create(DecredentialingReasonViewModel DecredentialingReason);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DecredentialingReason>Object to Update</param>
        /// <returns>Updated Object</returns>
        DecredentialingReasonViewModel Update(DecredentialingReasonViewModel DecredentialingReason);

    }
}