using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IMilitaryPresentDutyService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<MilitaryPresentDutyViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        MilitaryPresentDutyViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryPresentDuty>Object to Create</param>
        /// <returns>Updated Object</returns>
        MilitaryPresentDutyViewModel Create(MilitaryPresentDutyViewModel MilitaryPresentDuty);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryPresentDuty>Object to Update</param>
        /// <returns>Updated Object</returns>
        MilitaryPresentDutyViewModel Update(MilitaryPresentDutyViewModel MilitaryPresentDuty);

    }
}