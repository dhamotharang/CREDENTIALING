using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDocumentTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DocumentTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DocumentTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DocumentType>Object to Create</param>
        /// <returns>Updated Object</returns>
        DocumentTypeViewModel Create(DocumentTypeViewModel DocumentType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DocumentType>Object to Update</param>
        /// <returns>Updated Object</returns>
        DocumentTypeViewModel Update(DocumentTypeViewModel DocumentType);

    }
}