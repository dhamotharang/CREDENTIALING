using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IMilitaryBranchService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<MilitaryBranchViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        MilitaryBranchViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryBranch>Object to Create</param>
        /// <returns>Updated Object</returns>
        MilitaryBranchViewModel Create(MilitaryBranchViewModel MilitaryBranch);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=MilitaryBranch>Object to Update</param>
        /// <returns>Updated Object</returns>
        MilitaryBranchViewModel Update(MilitaryBranchViewModel MilitaryBranch);

    }
}