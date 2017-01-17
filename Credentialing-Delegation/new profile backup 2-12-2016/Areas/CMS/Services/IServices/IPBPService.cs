using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPBPService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<PBPViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        PBPViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=PBP>Object to Create</param>
        /// <returns>Updated Object</returns>
        PBPViewModel Create(PBPViewModel PBP);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=PBP>Object to Update</param>
        /// <returns>Updated Object</returns>
        PBPViewModel Update(PBPViewModel PBP);

    }
}