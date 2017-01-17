using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IAdjustmentGroupCodeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<AdjustmentGroupCodeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        AdjustmentGroupCodeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=AdjustmentGroupCode>Object to Create</param>
        /// <returns>Updated Object</returns>
        AdjustmentGroupCodeViewModel Create(AdjustmentGroupCodeViewModel AdjustmentGroupCode);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=AdjustmentGroupCode>Object to Update</param>
        /// <returns>Updated Object</returns>
        AdjustmentGroupCodeViewModel Update(AdjustmentGroupCodeViewModel AdjustmentGroupCode);

    }
}