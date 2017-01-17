using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface ISpecialityBoardService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<SpecialityBoardViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        SpecialityBoardViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=SpecialityBoard>Object to Create</param>
        /// <returns>Updated Object</returns>
        SpecialityBoardViewModel Create(SpecialityBoardViewModel SpecialityBoard);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=SpecialityBoard>Object to Update</param>
        /// <returns>Updated Object</returns>
        SpecialityBoardViewModel Update(SpecialityBoardViewModel SpecialityBoard);

    }
}