using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDepartmentService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DepartmentViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DepartmentViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Department>Object to Create</param>
        /// <returns>Updated Object</returns>
        DepartmentViewModel Create(DepartmentViewModel Department);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Department>Object to Update</param>
        /// <returns>Updated Object</returns>
        DepartmentViewModel Update(DepartmentViewModel Department);

    }
}