using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface INDCQualifierService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<NDCQualifierViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        NDCQualifierViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=NDCQualifier>Object to Create</param>
        /// <returns>Updated Object</returns>
        NDCQualifierViewModel Create(NDCQualifierViewModel NDCQualifier);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=NDCQualifier>Object to Update</param>
        /// <returns>Updated Object</returns>
        NDCQualifierViewModel Update(NDCQualifierViewModel NDCQualifier);

    }
}