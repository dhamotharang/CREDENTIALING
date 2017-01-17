using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IQuestionCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<QuestionCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        QuestionCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=QuestionCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        QuestionCategoryViewModel Create(QuestionCategoryViewModel QuestionCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=QuestionCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        QuestionCategoryViewModel Update(QuestionCategoryViewModel QuestionCategory);

    }
}