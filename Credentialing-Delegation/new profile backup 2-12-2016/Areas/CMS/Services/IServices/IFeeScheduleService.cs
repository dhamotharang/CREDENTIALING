using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IFeeScheduleService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<FeeScheduleViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        FeeScheduleViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=FeeSchedule>Object to Create</param>
        /// <returns>Updated Object</returns>
        FeeScheduleViewModel Create(FeeScheduleViewModel FeeSchedule);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=FeeSchedule>Object to Update</param>
        /// <returns>Updated Object</returns>
        FeeScheduleViewModel Update(FeeScheduleViewModel FeeSchedule);

    }
}