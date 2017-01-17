using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDisciplineService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DisciplineViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DisciplineViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Discipline>Object to Create</param>
        /// <returns>Updated Object</returns>
        DisciplineViewModel Create(DisciplineViewModel Discipline);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Discipline>Object to Update</param>
        /// <returns>Updated Object</returns>
        DisciplineViewModel Update(DisciplineViewModel Discipline);

    }
}