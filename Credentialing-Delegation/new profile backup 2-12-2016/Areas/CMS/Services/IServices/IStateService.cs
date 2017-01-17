using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IStateService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<StateViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        StateViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=State>Object to Create</param>
        /// <returns>Updated Object</returns>
        StateViewModel Create(StateViewModel State);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=State>Object to Update</param>
        /// <returns>Updated Object</returns>
        StateViewModel Update(StateViewModel State);

    }
}