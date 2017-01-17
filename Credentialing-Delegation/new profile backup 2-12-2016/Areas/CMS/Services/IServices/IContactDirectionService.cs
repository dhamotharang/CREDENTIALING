using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IContactDirectionService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ContactDirectionViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ContactDirectionViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ContactDirection>Object to Create</param>
        /// <returns>Updated Object</returns>
        ContactDirectionViewModel Create(ContactDirectionViewModel ContactDirection);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ContactDirection>Object to Update</param>
        /// <returns>Updated Object</returns>
        ContactDirectionViewModel Update(ContactDirectionViewModel ContactDirection);

    }
}