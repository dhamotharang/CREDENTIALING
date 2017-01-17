using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IReasonTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ReasonTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ReasonTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ReasonType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ReasonTypeViewModel Create(ReasonTypeViewModel ReasonType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ReasonType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ReasonTypeViewModel Update(ReasonTypeViewModel ReasonType);

    }
}