using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ICityService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<CityViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        CityViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=City>Object to Create</param>
        /// <returns>Updated Object</returns>
        CityViewModel Create(CityViewModel City);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=City>Object to Update</param>
        /// <returns>Updated Object</returns>
        CityViewModel Update(CityViewModel City);

    }
}