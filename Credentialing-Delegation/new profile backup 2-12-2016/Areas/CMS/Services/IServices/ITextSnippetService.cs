using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ITextSnippetService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<TextSnippetViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        TextSnippetViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=TextSnippet>Object to Create</param>
        /// <returns>Updated Object</returns>
        TextSnippetViewModel Create(TextSnippetViewModel TextSnippet);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=TextSnippet>Object to Update</param>
        /// <returns>Updated Object</returns>
        TextSnippetViewModel Update(TextSnippetViewModel TextSnippet);

    }
}