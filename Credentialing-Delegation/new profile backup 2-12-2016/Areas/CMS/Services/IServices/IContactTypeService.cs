using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IContactTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ContactTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ContactTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ContactType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ContactTypeViewModel Create(ContactTypeViewModel ContactType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ContactType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ContactTypeViewModel Update(ContactTypeViewModel ContactType);

    }
}