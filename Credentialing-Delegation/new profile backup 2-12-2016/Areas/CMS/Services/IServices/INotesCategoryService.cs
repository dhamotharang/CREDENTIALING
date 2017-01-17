using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface INotesCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<NotesCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        NotesCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=NotesCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        NotesCategoryViewModel Create(NotesCategoryViewModel NotesCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=NotesCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        NotesCategoryViewModel Update(NotesCategoryViewModel NotesCategory);

    }
}