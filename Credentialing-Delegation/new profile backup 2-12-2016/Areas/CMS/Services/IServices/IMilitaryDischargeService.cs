using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IMilitaryDischargeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<MilitaryDischargeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        MilitaryDischargeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryDischarge>Object to Create</param>
        /// <returns>Updated Object</returns>
        MilitaryDischargeViewModel Create(MilitaryDischargeViewModel MilitaryDischarge);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryDischarge>Object to Update</param>
        /// <returns>Updated Object</returns>
        MilitaryDischargeViewModel Update(MilitaryDischargeViewModel MilitaryDischarge);

    }
}