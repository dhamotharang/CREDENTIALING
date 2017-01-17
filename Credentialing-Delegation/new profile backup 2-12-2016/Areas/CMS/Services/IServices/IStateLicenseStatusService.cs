using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IStateLicenseStatusService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<StateLicenseStatusViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        StateLicenseStatusViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=StateLicenseStatus>Object to Create</param>
        /// <returns>Updated Object</returns>
        StateLicenseStatusViewModel Create(StateLicenseStatusViewModel StateLicenseStatus);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=StateLicenseStatus>Object to Update</param>
        /// <returns>Updated Object</returns>
        StateLicenseStatusViewModel Update(StateLicenseStatusViewModel StateLicenseStatus);

    }
}