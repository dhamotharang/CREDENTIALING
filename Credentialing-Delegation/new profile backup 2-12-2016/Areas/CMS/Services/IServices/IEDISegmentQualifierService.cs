using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IEDISegmentQualifierService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<EDISegmentQualifierViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        EDISegmentQualifierViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=EDISegmentQualifier>Object to Create</param>
        /// <returns>Updated Object</returns>
        EDISegmentQualifierViewModel Create(EDISegmentQualifierViewModel EDISegmentQualifier);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=EDISegmentQualifier>Object to Update</param>
        /// <returns>Updated Object</returns>
        EDISegmentQualifierViewModel Update(EDISegmentQualifierViewModel EDISegmentQualifier);

    }
}