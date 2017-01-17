using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ISeverityService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<SeverityViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        SeverityViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Severity>Object to Create</param>
        /// <returns>Updated Object</returns>
        SeverityViewModel Create(SeverityViewModel Severity);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Severity>Object to Update</param>
        /// <returns>Updated Object</returns>
        SeverityViewModel Update(SeverityViewModel Severity);

    }
}