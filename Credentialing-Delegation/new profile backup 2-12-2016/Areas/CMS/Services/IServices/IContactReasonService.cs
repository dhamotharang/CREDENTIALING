using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IContactReasonService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ContactReasonViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ContactReasonViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ContactReason>Object to Create</param>
        /// <returns>Updated Object</returns>
        ContactReasonViewModel Create(ContactReasonViewModel ContactReason);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ContactReason>Object to Update</param>
        /// <returns>Updated Object</returns>
        ContactReasonViewModel Update(ContactReasonViewModel ContactReason);

    }
}