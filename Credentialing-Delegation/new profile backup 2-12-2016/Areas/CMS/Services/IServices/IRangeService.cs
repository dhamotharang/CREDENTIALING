using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IRangeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<RangeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        RangeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Range>Object to Create</param>
        /// <returns>Updated Object</returns>
        RangeViewModel Create(RangeViewModel Range);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Range>Object to Update</param>
        /// <returns>Updated Object</returns>
        RangeViewModel Update(RangeViewModel Range);

    }
}