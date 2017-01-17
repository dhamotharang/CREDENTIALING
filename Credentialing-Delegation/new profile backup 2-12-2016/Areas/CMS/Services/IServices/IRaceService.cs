using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IRaceService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<RaceViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        RaceViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Race>Object to Create</param>
        /// <returns>Updated Object</returns>
        RaceViewModel Create(RaceViewModel Race);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Race>Object to Update</param>
        /// <returns>Updated Object</returns>
        RaceViewModel Update(RaceViewModel Race);

    }
}