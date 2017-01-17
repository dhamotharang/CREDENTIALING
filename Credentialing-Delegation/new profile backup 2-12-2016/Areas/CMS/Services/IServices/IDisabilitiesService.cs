using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDisabilitiesService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DisabilitiesViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DisabilitiesViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Disabilities>Object to Create</param>
        /// <returns>Updated Object</returns>
        DisabilitiesViewModel Create(DisabilitiesViewModel Disabilities);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Disabilities>Object to Update</param>
        /// <returns>Updated Object</returns>
        DisabilitiesViewModel Update(DisabilitiesViewModel Disabilities);

    }
}