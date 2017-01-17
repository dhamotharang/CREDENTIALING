using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IReferenceQualifierService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ReferenceQualifierViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ReferenceQualifierViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ReferenceQualifier>Object to Create</param>
        /// <returns>Updated Object</returns>
        ReferenceQualifierViewModel Create(ReferenceQualifierViewModel ReferenceQualifier);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ReferenceQualifier>Object to Update</param>
        /// <returns>Updated Object</returns>
        ReferenceQualifierViewModel Update(ReferenceQualifierViewModel ReferenceQualifier);

    }
}