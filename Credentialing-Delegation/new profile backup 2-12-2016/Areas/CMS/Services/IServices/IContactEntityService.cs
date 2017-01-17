using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IContactEntityService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ContactEntityViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ContactEntityViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ContactEntity>Object to Create</param>
        /// <returns>Updated Object</returns>
        ContactEntityViewModel Create(ContactEntityViewModel ContactEntity);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ContactEntity>Object to Update</param>
        /// <returns>Updated Object</returns>
        ContactEntityViewModel Update(ContactEntityViewModel ContactEntity);

    }
}