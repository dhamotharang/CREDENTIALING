using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IRelationshipService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<RelationshipViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        RelationshipViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=Relationship>Object to Create</param>
        /// <returns>Updated Object</returns>
        RelationshipViewModel Create(RelationshipViewModel Relationship);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=Relationship>Object to Update</param>
        /// <returns>Updated Object</returns>
        RelationshipViewModel Update(RelationshipViewModel Relationship);

    }
}