using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ISourcesService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<SourcesViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        SourcesViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Sources>Object to Create</param>
        /// <returns>Updated Object</returns>
        SourcesViewModel Create(SourcesViewModel Sources);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Sources>Object to Update</param>
        /// <returns>Updated Object</returns>
        SourcesViewModel Update(SourcesViewModel Sources);

    }
}