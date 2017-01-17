using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ILanguageService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<LanguageViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        LanguageViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Language>Object to Create</param>
        /// <returns>Updated Object</returns>
        LanguageViewModel Create(LanguageViewModel Language);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Language>Object to Update</param>
        /// <returns>Updated Object</returns>
        LanguageViewModel Update(LanguageViewModel Language);

    }
}