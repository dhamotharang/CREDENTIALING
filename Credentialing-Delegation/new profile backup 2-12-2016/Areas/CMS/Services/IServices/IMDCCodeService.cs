using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IMDCCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<MDCCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        MDCCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=MDCCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        MDCCodeViewModel Create(MDCCodeViewModel MDCCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=MDCCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        MDCCodeViewModel Update(MDCCodeViewModel MDCCode);

    }
}