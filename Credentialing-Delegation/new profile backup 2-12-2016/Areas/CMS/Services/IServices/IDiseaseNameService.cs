using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IDiseaseNameService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<DiseaseNameViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        DiseaseNameViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=DiseaseName>Object to Create</param>
        /// <returns>Updated Object</returns>
        DiseaseNameViewModel Create(DiseaseNameViewModel DiseaseName);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=DiseaseName>Object to Update</param>
        /// <returns>Updated Object</returns>
        DiseaseNameViewModel Update(DiseaseNameViewModel DiseaseName);

    }
}