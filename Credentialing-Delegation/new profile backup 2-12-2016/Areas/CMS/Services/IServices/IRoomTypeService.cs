using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IRoomTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<RoomTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        RoomTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=RoomType>Object to Create</param>
        /// <returns>Updated Object</returns>
        RoomTypeViewModel Create(RoomTypeViewModel RoomType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=RoomType>Object to Update</param>
        /// <returns>Updated Object</returns>
        RoomTypeViewModel Update(RoomTypeViewModel RoomType);

    }
}