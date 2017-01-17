using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IComplaintCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ComplaintCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ComplaintCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ComplaintCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        ComplaintCategoryViewModel Create(ComplaintCategoryViewModel ComplaintCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ComplaintCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        ComplaintCategoryViewModel Update(ComplaintCategoryViewModel ComplaintCategory);

    }
}