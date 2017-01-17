using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IOutcomeTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<OutcomeTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        OutcomeTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=OutcomeType>Object to Create</param>
        /// <returns>Updated Object</returns>
        OutcomeTypeViewModel Create(OutcomeTypeViewModel OutcomeType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=OutcomeType>Object to Update</param>
        /// <returns>Updated Object</returns>
        OutcomeTypeViewModel Update(OutcomeTypeViewModel OutcomeType);

    }
}