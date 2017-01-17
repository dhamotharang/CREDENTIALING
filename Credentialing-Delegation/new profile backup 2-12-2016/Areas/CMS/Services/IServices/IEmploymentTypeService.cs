using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IEmploymentTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<EmploymentTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        EmploymentTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=EmploymentType>Object to Create</param>
        /// <returns>Updated Object</returns>
        EmploymentTypeViewModel Create(EmploymentTypeViewModel EmploymentType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=EmploymentType>Object to Update</param>
        /// <returns>Updated Object</returns>
        EmploymentTypeViewModel Update(EmploymentTypeViewModel EmploymentType);

    }
}