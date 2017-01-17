using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IReasonService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ReasonViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ReasonViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Reason>Object to Create</param>
        /// <returns>Updated Object</returns>
        ReasonViewModel Create(ReasonViewModel Reason);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Reason>Object to Update</param>
        /// <returns>Updated Object</returns>
        ReasonViewModel Update(ReasonViewModel Reason);

    }
}