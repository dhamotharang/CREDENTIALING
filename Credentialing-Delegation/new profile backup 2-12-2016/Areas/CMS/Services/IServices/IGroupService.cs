using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IGroupService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<GroupViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        GroupViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Group>Object to Create</param>
        /// <returns>Updated Object</returns>
        GroupViewModel Create(GroupViewModel Group);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Group>Object to Update</param>
        /// <returns>Updated Object</returns>
        GroupViewModel Update(GroupViewModel Group);

    }
}