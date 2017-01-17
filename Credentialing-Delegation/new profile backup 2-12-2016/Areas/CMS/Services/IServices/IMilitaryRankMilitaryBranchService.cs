using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IMilitaryRankMilitaryBranchService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<MilitaryRankMilitaryBranchViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        MilitaryRankMilitaryBranchViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryRankMilitaryBranch>Object to Create</param>
        /// <returns>Updated Object</returns>
        MilitaryRankMilitaryBranchViewModel Create(MilitaryRankMilitaryBranchViewModel MilitaryRankMilitaryBranch);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryRankMilitaryBranch>Object to Update</param>
        /// <returns>Updated Object</returns>
        MilitaryRankMilitaryBranchViewModel Update(MilitaryRankMilitaryBranchViewModel MilitaryRankMilitaryBranch);

    }
}