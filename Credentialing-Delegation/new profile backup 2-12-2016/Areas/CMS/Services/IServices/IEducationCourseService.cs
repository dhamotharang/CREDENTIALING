using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IEducationCourseService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<EducationCourseViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        EducationCourseViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=EducationCourse>Object to Create</param>
        /// <returns>Updated Object</returns>
        EducationCourseViewModel Create(EducationCourseViewModel EducationCourse);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=EducationCourse>Object to Update</param>
        /// <returns>Updated Object</returns>
        EducationCourseViewModel Update(EducationCourseViewModel EducationCourse);

    }
}