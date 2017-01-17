using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDEAScheduleService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DEAScheduleViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DEAScheduleViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DEASchedule>Object to Create</param>
        /// <returns>Updated Object</returns>
        DEAScheduleViewModel Create(DEAScheduleViewModel DEASchedule);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DEASchedule>Object to Update</param>
        /// <returns>Updated Object</returns>
        DEAScheduleViewModel Update(DEAScheduleViewModel DEASchedule);

    }
}