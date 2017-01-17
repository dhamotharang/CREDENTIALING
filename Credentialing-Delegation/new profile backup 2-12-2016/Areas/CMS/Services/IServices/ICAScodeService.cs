using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ICAScodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<CAScodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        CAScodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=CAScode>Object to Create</param>
        /// <returns>Updated Object</returns>
        CAScodeViewModel Create(CAScodeViewModel CAScode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=CAScode>Object to Update</param>
        /// <returns>Updated Object</returns>
        CAScodeViewModel Update(CAScodeViewModel CAScode);

    }
}