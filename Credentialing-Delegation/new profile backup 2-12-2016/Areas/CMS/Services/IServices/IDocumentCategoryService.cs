using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDocumentCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DocumentCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DocumentCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DocumentCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        DocumentCategoryViewModel Create(DocumentCategoryViewModel DocumentCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DocumentCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        DocumentCategoryViewModel Update(DocumentCategoryViewModel DocumentCategory);

    }
}