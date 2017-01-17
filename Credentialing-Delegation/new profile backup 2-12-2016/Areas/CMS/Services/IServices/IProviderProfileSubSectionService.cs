using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IProviderProfileSubSectionService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ProviderProfileSubSectionViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ProviderProfileSubSectionViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderProfileSubSection>Object to Create</param>
        /// <returns>Updated Object</returns>
        ProviderProfileSubSectionViewModel Create(ProviderProfileSubSectionViewModel ProviderProfileSubSection);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ProviderProfileSubSection>Object to Update</param>
        /// <returns>Updated Object</returns>
        ProviderProfileSubSectionViewModel Update(ProviderProfileSubSectionViewModel ProviderProfileSubSection);

    }
}