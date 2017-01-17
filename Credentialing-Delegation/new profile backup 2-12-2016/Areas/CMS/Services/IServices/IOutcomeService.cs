using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IOutcomeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<OutcomeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        OutcomeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Outcome>Object to Create</param>
        /// <returns>Updated Object</returns>
        OutcomeViewModel Create(OutcomeViewModel Outcome);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Outcome>Object to Update</param>
        /// <returns>Updated Object</returns>
        OutcomeViewModel Update(OutcomeViewModel Outcome);

    }
}