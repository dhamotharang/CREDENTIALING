using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IVisaStatusService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<VisaStatusViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        VisaStatusViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=VisaStatus>Object to Create</param>
        /// <returns>Updated Object</returns>
        VisaStatusViewModel Create(VisaStatusViewModel VisaStatus);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=VisaStatus>Object to Update</param>
        /// <returns>Updated Object</returns>
        VisaStatusViewModel Update(VisaStatusViewModel VisaStatus);

    }
}