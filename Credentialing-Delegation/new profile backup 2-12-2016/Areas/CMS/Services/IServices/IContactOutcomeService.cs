using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IContactOutcomeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ContactOutcomeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ContactOutcomeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ContactOutcome>Object to Create</param>
        /// <returns>Updated Object</returns>
        ContactOutcomeViewModel Create(ContactOutcomeViewModel ContactOutcome);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ContactOutcome>Object to Update</param>
        /// <returns>Updated Object</returns>
        ContactOutcomeViewModel Update(ContactOutcomeViewModel ContactOutcome);

    }
}