using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ICountryService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<CountryViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        CountryViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Country>Object to Create</param>
        /// <returns>Updated Object</returns>
        CountryViewModel Create(CountryViewModel Country);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Country>Object to Update</param>
        /// <returns>Updated Object</returns>
        CountryViewModel Update(CountryViewModel Country);

    }
}