using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IQuestionTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<QuestionTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        QuestionTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=QuestionType>Object to Create</param>
        /// <returns>Updated Object</returns>
        QuestionTypeViewModel Create(QuestionTypeViewModel QuestionType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=QuestionType>Object to Update</param>
        /// <returns>Updated Object</returns>
        QuestionTypeViewModel Update(QuestionTypeViewModel QuestionType);

    }
}