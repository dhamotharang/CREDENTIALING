using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IAuthorizationTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<AuthorizationTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        AuthorizationTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=AuthorizationType>Object to Create</param>
        /// <returns>Updated Object</returns>
        AuthorizationTypeViewModel Create(AuthorizationTypeViewModel AuthorizationType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=AuthorizationType>Object to Update</param>
        /// <returns>Updated Object</returns>
        AuthorizationTypeViewModel Update(AuthorizationTypeViewModel AuthorizationType);

    }
}