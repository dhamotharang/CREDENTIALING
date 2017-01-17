using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface INotesTemplateService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<NotesTemplateViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        NotesTemplateViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=NotesTemplate>Object to Create</param>
        /// <returns>Updated Object</returns>
        NotesTemplateViewModel Create(NotesTemplateViewModel NotesTemplate);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=NotesTemplate>Object to Update</param>
        /// <returns>Updated Object</returns>
        NotesTemplateViewModel Update(NotesTemplateViewModel NotesTemplate);

    }
}