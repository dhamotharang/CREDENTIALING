using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface INoteTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<NoteTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        NoteTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=NoteType>Object to Create</param>
        /// <returns>Updated Object</returns>
        NoteTypeViewModel Create(NoteTypeViewModel NoteType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=NoteType>Object to Update</param>
        /// <returns>Updated Object</returns>
        NoteTypeViewModel Update(NoteTypeViewModel NoteType);

    }
}