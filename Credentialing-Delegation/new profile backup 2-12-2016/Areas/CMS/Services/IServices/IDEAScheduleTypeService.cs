using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDEAScheduleTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DEAScheduleTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DEAScheduleTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DEAScheduleType>Object to Create</param>
        /// <returns>Updated Object</returns>
        DEAScheduleTypeViewModel Create(DEAScheduleTypeViewModel DEAScheduleType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DEAScheduleType>Object to Update</param>
        /// <returns>Updated Object</returns>
        DEAScheduleTypeViewModel Update(DEAScheduleTypeViewModel DEAScheduleType);

    }
}