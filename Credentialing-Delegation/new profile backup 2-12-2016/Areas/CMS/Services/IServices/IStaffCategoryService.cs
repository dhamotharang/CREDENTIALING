using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IStaffCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<StaffCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        StaffCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=StaffCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        StaffCategoryViewModel Create(StaffCategoryViewModel StaffCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=StaffCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        StaffCategoryViewModel Update(StaffCategoryViewModel StaffCategory);

    }
}