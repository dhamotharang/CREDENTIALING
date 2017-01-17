using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IProcedureCodeTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<ProcedureCodeTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        ProcedureCodeTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=ProcedureCodeType>Object to Create</param>
        /// <returns>Updated Object</returns>
        ProcedureCodeTypeViewModel Create(ProcedureCodeTypeViewModel ProcedureCodeType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=ProcedureCodeType>Object to Update</param>
        /// <returns>Updated Object</returns>
        ProcedureCodeTypeViewModel Update(ProcedureCodeTypeViewModel ProcedureCodeType);

    }
}