using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ICOBPreferenceService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<COBPreferenceViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        COBPreferenceViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=COBPreference>Object to Create</param>
        /// <returns>Updated Object</returns>
        COBPreferenceViewModel Create(COBPreferenceViewModel COBPreference);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=COBPreference>Object to Update</param>
        /// <returns>Updated Object</returns>
        COBPreferenceViewModel Update(COBPreferenceViewModel COBPreference);

    }
}