using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IProviderModeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ProviderModeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ProviderModeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderMode>Object to Create</param>
        /// <returns>Updated Object</returns>
        ProviderModeViewModel Create(ProviderModeViewModel ProviderMode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderMode>Object to Update</param>
        /// <returns>Updated Object</returns>
        ProviderModeViewModel Update(ProviderModeViewModel ProviderMode);

    }
}