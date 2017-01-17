using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IProviderLevelService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ProviderLevelViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ProviderLevelViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderLevel>Object to Create</param>
        /// <returns>Updated Object</returns>
        ProviderLevelViewModel Create(ProviderLevelViewModel ProviderLevel);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderLevel>Object to Update</param>
        /// <returns>Updated Object</returns>
        ProviderLevelViewModel Update(ProviderLevelViewModel ProviderLevel);

    }
}