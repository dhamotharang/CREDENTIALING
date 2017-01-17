using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDiseaseCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DiseaseCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DiseaseCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DiseaseCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        DiseaseCategoryViewModel Create(DiseaseCategoryViewModel DiseaseCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DiseaseCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        DiseaseCategoryViewModel Update(DiseaseCategoryViewModel DiseaseCategory);

    }
}