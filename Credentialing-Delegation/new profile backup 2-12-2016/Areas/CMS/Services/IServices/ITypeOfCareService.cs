using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ITypeOfCareService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<TypeOfCareViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        TypeOfCareViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=TypeOfCare>Object to Create</param>
        /// <returns>Updated Object</returns>
        TypeOfCareViewModel Create(TypeOfCareViewModel TypeOfCare);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=TypeOfCare>Object to Update</param>
        /// <returns>Updated Object</returns>
        TypeOfCareViewModel Update(TypeOfCareViewModel TypeOfCare);

    }
}