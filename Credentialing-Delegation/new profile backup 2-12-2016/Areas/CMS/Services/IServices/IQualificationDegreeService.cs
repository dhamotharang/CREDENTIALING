using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IQualificationDegreeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<QualificationDegreeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        QualificationDegreeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=QualificationDegree>Object to Create</param>
        /// <returns>Updated Object</returns>
        QualificationDegreeViewModel Create(QualificationDegreeViewModel QualificationDegree);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=QualificationDegree>Object to Update</param>
        /// <returns>Updated Object</returns>
        QualificationDegreeViewModel Update(QualificationDegreeViewModel QualificationDegree);

    }
}