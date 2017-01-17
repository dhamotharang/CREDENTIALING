using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IContactEntityTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ContactEntityTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ContactEntityTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ContactEntityType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ContactEntityTypeViewModel Create(ContactEntityTypeViewModel ContactEntityType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ContactEntityType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ContactEntityTypeViewModel Update(ContactEntityTypeViewModel ContactEntityType);

    }
}