using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPatientRelationService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<PatientRelationViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        PatientRelationViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=PatientRelation>Object to Create</param>
        /// <returns>Updated Object</returns>
        PatientRelationViewModel Create(PatientRelationViewModel PatientRelation);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=PatientRelation>Object to Update</param>
        /// <returns>Updated Object</returns>
        PatientRelationViewModel Update(PatientRelationViewModel PatientRelation);

    }
}