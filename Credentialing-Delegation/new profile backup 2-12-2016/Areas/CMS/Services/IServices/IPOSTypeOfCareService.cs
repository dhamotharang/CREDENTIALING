using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPOSTypeOfCareService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<POSTypeOfCareViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        POSTypeOfCareViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=POSTypeOfCare>Object to Create</param>
        /// <returns>Updated Object</returns>
        POSTypeOfCareViewModel Create(POSTypeOfCareViewModel POSTypeOfCare);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=POSTypeOfCare>Object to Update</param>
        /// <returns>Updated Object</returns>
        POSTypeOfCareViewModel Update(POSTypeOfCareViewModel POSTypeOfCare);

    }
}