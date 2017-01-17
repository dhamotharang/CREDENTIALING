using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPlainLanguageService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<PlainLanguageViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        PlainLanguageViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=PlainLanguage>Object to Create</param>
        /// <returns>Updated Object</returns>
        PlainLanguageViewModel Create(PlainLanguageViewModel PlainLanguage);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=PlainLanguage>Object to Update</param>
        /// <returns>Updated Object</returns>
        PlainLanguageViewModel Update(PlainLanguageViewModel PlainLanguage);

    }
}