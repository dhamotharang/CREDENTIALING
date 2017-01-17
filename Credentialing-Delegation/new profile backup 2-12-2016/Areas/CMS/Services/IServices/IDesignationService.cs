using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDesignationService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DesignationViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DesignationViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Designation>Object to Create</param>
        /// <returns>Updated Object</returns>
        DesignationViewModel Create(DesignationViewModel Designation);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Designation>Object to Update</param>
        /// <returns>Updated Object</returns>
        DesignationViewModel Update(DesignationViewModel Designation);

    }
}