using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPlanCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<PlanCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        PlanCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=PlanCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        PlanCategoryViewModel Create(PlanCategoryViewModel PlanCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=PlanCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        PlanCategoryViewModel Update(PlanCategoryViewModel PlanCategory);

    }
}