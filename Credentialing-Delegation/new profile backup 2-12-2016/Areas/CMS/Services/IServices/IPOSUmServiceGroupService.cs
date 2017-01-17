using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPOSUmServiceGroupService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<POSUmServiceGroupViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        POSUmServiceGroupViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=POSUmServiceGroup>Object to Create</param>
        /// <returns>Updated Object</returns>
        POSUmServiceGroupViewModel Create(POSUmServiceGroupViewModel POSUmServiceGroup);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=POSUmServiceGroup>Object to Update</param>
        /// <returns>Updated Object</returns>
        POSUmServiceGroupViewModel Update(POSUmServiceGroupViewModel POSUmServiceGroup);

    }
}