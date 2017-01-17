using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ISpecialityService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<SpecialityViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        SpecialityViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Speciality>Object to Create</param>
        /// <returns>Updated Object</returns>
        SpecialityViewModel Create(SpecialityViewModel Speciality);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Speciality>Object to Update</param>
        /// <returns>Updated Object</returns>
        SpecialityViewModel Update(SpecialityViewModel Speciality);

    }
}