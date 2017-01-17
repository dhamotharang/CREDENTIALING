using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IPOSRoomTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<POSRoomTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        POSRoomTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=POSRoomType>Object to Create</param>
        /// <returns>Updated Object</returns>
        POSRoomTypeViewModel Create(POSRoomTypeViewModel POSRoomType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=POSRoomType>Object to Update</param>
        /// <returns>Updated Object</returns>
        POSRoomTypeViewModel Update(POSRoomTypeViewModel POSRoomType);

    }
}