using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IRequestTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<RequestTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        RequestTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=RequestType>Object to Create</param>
        /// <returns>Updated Object</returns>
        RequestTypeViewModel Create(RequestTypeViewModel RequestType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=RequestType>Object to Update</param>
        /// <returns>Updated Object</returns>
        RequestTypeViewModel Update(RequestTypeViewModel RequestType);

    }
}