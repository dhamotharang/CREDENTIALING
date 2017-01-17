using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ICountyService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<CountyViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        CountyViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=County>Object to Create</param>
        /// <returns>Updated Object</returns>
        CountyViewModel Create(CountyViewModel County);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=County>Object to Update</param>
        /// <returns>Updated Object</returns>
        CountyViewModel Update(CountyViewModel County);

    }
}