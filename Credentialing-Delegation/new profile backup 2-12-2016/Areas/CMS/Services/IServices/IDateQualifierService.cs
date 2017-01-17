using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDateQualifierService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DateQualifierViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DateQualifierViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DateQualifier>Object to Create</param>
        /// <returns>Updated Object</returns>
        DateQualifierViewModel Create(DateQualifierViewModel DateQualifier);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DateQualifier>Object to Update</param>
        /// <returns>Updated Object</returns>
        DateQualifierViewModel Update(DateQualifierViewModel DateQualifier);

    }
}