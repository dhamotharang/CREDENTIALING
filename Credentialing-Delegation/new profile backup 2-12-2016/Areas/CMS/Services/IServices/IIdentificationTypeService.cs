using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IIdentificationTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<IdentificationTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        IdentificationTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=IdentificationType>Object to Create</param>
        /// <returns>Updated Object</returns>
        IdentificationTypeViewModel Create(IdentificationTypeViewModel IdentificationType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=IdentificationType>Object to Update</param>
        /// <returns>Updated Object</returns>
        IdentificationTypeViewModel Update(IdentificationTypeViewModel IdentificationType);

    }
}