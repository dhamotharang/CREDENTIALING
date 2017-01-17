using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ISuffixService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<SuffixViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        SuffixViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Suffix>Object to Create</param>
        /// <returns>Updated Object</returns>
        SuffixViewModel Create(SuffixViewModel Suffix);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Suffix>Object to Update</param>
        /// <returns>Updated Object</returns>
        SuffixViewModel Update(SuffixViewModel Suffix);

    }
}