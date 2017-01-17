using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IMilitaryRankService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<MilitaryRankViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        MilitaryRankViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryRank>Object to Create</param>
        /// <returns>Updated Object</returns>
        MilitaryRankViewModel Create(MilitaryRankViewModel MilitaryRank);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryRank>Object to Update</param>
        /// <returns>Updated Object</returns>
        MilitaryRankViewModel Update(MilitaryRankViewModel MilitaryRank);

    }
}