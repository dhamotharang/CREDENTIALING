using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IEthnicityService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<EthnicityViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        EthnicityViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Ethnicity>Object to Create</param>
        /// <returns>Updated Object</returns>
        EthnicityViewModel Create(EthnicityViewModel Ethnicity);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Ethnicity>Object to Update</param>
        /// <returns>Updated Object</returns>
        EthnicityViewModel Update(EthnicityViewModel Ethnicity);

    }
}