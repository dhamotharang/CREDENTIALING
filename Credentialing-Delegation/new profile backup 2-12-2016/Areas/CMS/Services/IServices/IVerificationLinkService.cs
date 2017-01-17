using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IVerificationLinkService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<VerificationLinkViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        VerificationLinkViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=VerificationLink>Object to Create</param>
        /// <returns>Updated Object</returns>
        VerificationLinkViewModel Create(VerificationLinkViewModel VerificationLink);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=VerificationLink>Object to Update</param>
        /// <returns>Updated Object</returns>
        VerificationLinkViewModel Update(VerificationLinkViewModel VerificationLink);

    }
}