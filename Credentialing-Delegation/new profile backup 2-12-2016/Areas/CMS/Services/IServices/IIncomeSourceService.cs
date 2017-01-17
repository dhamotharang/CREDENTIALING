using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IIncomeSourceService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<IncomeSourceViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        IncomeSourceViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=IncomeSource>Object to Create</param>
        /// <returns>Updated Object</returns>
        IncomeSourceViewModel Create(IncomeSourceViewModel IncomeSource);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=IncomeSource>Object to Update</param>
        /// <returns>Updated Object</returns>
        IncomeSourceViewModel Update(IncomeSourceViewModel IncomeSource);

    }
}