using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IIPAService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<IPAViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        IPAViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=IPA>Object to Create</param>
        /// <returns>Updated Object</returns>
        IPAViewModel Create(IPAViewModel IPA);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=IPA>Object to Update</param>
        /// <returns>Updated Object</returns>
        IPAViewModel Update(IPAViewModel IPA);

    }
}