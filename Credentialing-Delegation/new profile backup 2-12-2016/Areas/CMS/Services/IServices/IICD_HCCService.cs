using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IICD_HCCService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ICD_HCCViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ICD_HCCViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ICD_HCC>Object to Create</param>
        /// <returns>Updated Object</returns>
        ICD_HCCViewModel Create(ICD_HCCViewModel ICD_HCC);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ICD_HCC>Object to Update</param>
        /// <returns>Updated Object</returns>
        ICD_HCCViewModel Update(ICD_HCCViewModel ICD_HCC);

    }
}