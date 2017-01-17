using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface INDCQuantityQualifierService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<NDCQuantityQualifierViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        NDCQuantityQualifierViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=NDCQuantityQualifier>Object to Create</param>
        /// <returns>Updated Object</returns>
        NDCQuantityQualifierViewModel Create(NDCQuantityQualifierViewModel NDCQuantityQualifier);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=NDCQuantityQualifier>Object to Update</param>
        /// <returns>Updated Object</returns>
        NDCQuantityQualifierViewModel Update(NDCQuantityQualifierViewModel NDCQuantityQualifier);

    }
}