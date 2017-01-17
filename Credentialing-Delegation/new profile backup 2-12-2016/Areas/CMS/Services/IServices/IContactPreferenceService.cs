using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IContactPreferenceService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ContactPreferenceViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ContactPreferenceViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ContactPreference>Object to Create</param>
        /// <returns>Updated Object</returns>
        ContactPreferenceViewModel Create(ContactPreferenceViewModel ContactPreference);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ContactPreference>Object to Update</param>
        /// <returns>Updated Object</returns>
        ContactPreferenceViewModel Update(ContactPreferenceViewModel ContactPreference);

    }
}