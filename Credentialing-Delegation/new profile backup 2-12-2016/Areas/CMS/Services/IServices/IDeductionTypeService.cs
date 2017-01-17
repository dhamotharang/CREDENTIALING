using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDeductionTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DeductionTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DeductionTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DeductionType>Object to Create</param>
        /// <returns>Updated Object</returns>
        DeductionTypeViewModel Create(DeductionTypeViewModel DeductionType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DeductionType>Object to Update</param>
        /// <returns>Updated Object</returns>
        DeductionTypeViewModel Update(DeductionTypeViewModel DeductionType);

    }
}