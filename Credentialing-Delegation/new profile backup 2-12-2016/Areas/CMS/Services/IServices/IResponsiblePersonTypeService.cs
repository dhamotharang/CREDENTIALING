using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IResponsiblePersonTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ResponsiblePersonTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ResponsiblePersonTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ResponsiblePersonType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ResponsiblePersonTypeViewModel Create(ResponsiblePersonTypeViewModel ResponsiblePersonType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ResponsiblePersonType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ResponsiblePersonTypeViewModel Update(ResponsiblePersonTypeViewModel ResponsiblePersonType);

    }
}