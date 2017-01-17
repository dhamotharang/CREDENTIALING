using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IProviderTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ProviderTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ProviderTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ProviderTypeViewModel Create(ProviderTypeViewModel ProviderType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ProviderTypeViewModel Update(ProviderTypeViewModel ProviderType);

    }
}