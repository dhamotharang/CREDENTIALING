using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IServiceRequestService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ServiceRequestViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ServiceRequestViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ServiceRequest>Object to Create</param>
        /// <returns>Updated Object</returns>
        ServiceRequestViewModel Create(ServiceRequestViewModel ServiceRequest);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ServiceRequest>Object to Update</param>
        /// <returns>Updated Object</returns>
        ServiceRequestViewModel Update(ServiceRequestViewModel ServiceRequest);

    }
}