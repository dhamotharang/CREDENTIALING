using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ILineOfBusinessService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<LineOfBusinessViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        LineOfBusinessViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=LineOfBusiness>Object to Create</param>
        /// <returns>Updated Object</returns>
        LineOfBusinessViewModel Create(LineOfBusinessViewModel LineOfBusiness);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=LineOfBusiness>Object to Update</param>
        /// <returns>Updated Object</returns>
        LineOfBusinessViewModel Update(LineOfBusinessViewModel LineOfBusiness);

    }
}