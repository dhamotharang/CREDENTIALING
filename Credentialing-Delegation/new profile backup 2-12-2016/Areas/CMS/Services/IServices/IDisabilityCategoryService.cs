using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDisabilityCategoryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DisabilityCategoryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DisabilityCategoryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DisabilityCategory>Object to Create</param>
        /// <returns>Updated Object</returns>
        DisabilityCategoryViewModel Create(DisabilityCategoryViewModel DisabilityCategory);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DisabilityCategory>Object to Update</param>
        /// <returns>Updated Object</returns>
        DisabilityCategoryViewModel Update(DisabilityCategoryViewModel DisabilityCategory);

    }
}