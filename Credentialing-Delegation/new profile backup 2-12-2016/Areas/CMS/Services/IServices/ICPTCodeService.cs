using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ICPTCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<CPTCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        CPTCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=CPTCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        CPTCodeViewModel Create(CPTCodeViewModel CPTCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=CPTCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        CPTCodeViewModel Update(CPTCodeViewModel CPTCode);

    }
}