using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface INoteDecisionTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<NoteDecisionTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        NoteDecisionTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=NoteDecisionType>Object to Create</param>
        /// <returns>Updated Object</returns>
        NoteDecisionTypeViewModel Create(NoteDecisionTypeViewModel NoteDecisionType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=NoteDecisionType>Object to Update</param>
        /// <returns>Updated Object</returns>
        NoteDecisionTypeViewModel Update(NoteDecisionTypeViewModel NoteDecisionType);

    }
}