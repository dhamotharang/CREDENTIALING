using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IVisitTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<VisitTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        VisitTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=VisitType>Object to Create</param>
        /// <returns>Updated Object</returns>
        VisitTypeViewModel Create(VisitTypeViewModel VisitType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=VisitType>Object to Update</param>
        /// <returns>Updated Object</returns>
        VisitTypeViewModel Update(VisitTypeViewModel VisitType);

    }
}