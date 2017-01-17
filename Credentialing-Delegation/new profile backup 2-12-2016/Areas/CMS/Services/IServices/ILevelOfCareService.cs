using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ILevelOfCareService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<LevelOfCareViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        LevelOfCareViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=LevelOfCare>Object to Create</param>
        /// <returns>Updated Object</returns>
        LevelOfCareViewModel Create(LevelOfCareViewModel LevelOfCare);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=LevelOfCare>Object to Update</param>
        /// <returns>Updated Object</returns>
        LevelOfCareViewModel Update(LevelOfCareViewModel LevelOfCare);

    }
}