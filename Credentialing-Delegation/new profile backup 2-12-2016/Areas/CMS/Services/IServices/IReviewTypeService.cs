using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IReviewTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ReviewTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ReviewTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ReviewType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ReviewTypeViewModel Create(ReviewTypeViewModel ReviewType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ReviewType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ReviewTypeViewModel Update(ReviewTypeViewModel ReviewType);

    }
}