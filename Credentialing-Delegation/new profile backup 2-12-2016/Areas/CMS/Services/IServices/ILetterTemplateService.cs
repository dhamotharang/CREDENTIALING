using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ILetterTemplateService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<LetterTemplateViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        LetterTemplateViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=LetterTemplate>Object to Create</param>
        /// <returns>Updated Object</returns>
        LetterTemplateViewModel Create(LetterTemplateViewModel LetterTemplate);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=LetterTemplate>Object to Update</param>
        /// <returns>Updated Object</returns>
        LetterTemplateViewModel Update(LetterTemplateViewModel LetterTemplate);

    }
}