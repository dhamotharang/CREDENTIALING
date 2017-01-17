using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IQuestionService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<QuestionViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        QuestionViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Question>Object to Create</param>
        /// <returns>Updated Object</returns>
        QuestionViewModel Create(QuestionViewModel Question);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Question>Object to Update</param>
        /// <returns>Updated Object</returns>
        QuestionViewModel Update(QuestionViewModel Question);

    }
}