using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IReligionService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ReligionViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ReligionViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Religion>Object to Create</param>
        /// <returns>Updated Object</returns>
        ReligionViewModel Create(ReligionViewModel Religion);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Religion>Object to Update</param>
        /// <returns>Updated Object</returns>
        ReligionViewModel Update(ReligionViewModel Religion);

    }
}