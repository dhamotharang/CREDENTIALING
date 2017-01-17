using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPremiumTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<PremiumTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        PremiumTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=PremiumType>Object to Create</param>
        /// <returns>Updated Object</returns>
        PremiumTypeViewModel Create(PremiumTypeViewModel PremiumType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=PremiumType>Object to Update</param>
        /// <returns>Updated Object</returns>
        PremiumTypeViewModel Update(PremiumTypeViewModel PremiumType);

    }
}