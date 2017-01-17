using PortalTemplate.Areas.CMS.Models;
using System.Collections.Generic;

namespace PortalTemplate.Areas.CMS.Services.IServices
{
    public interface IBankAccountTypeService
    {
        /// <summary>
        /// Return List Of Object
        /// </summary>
        /// <returns>List Type</returns>
        List<BankAccountTypeViewModel> GetAll();

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="Code">Object's Code Parameter</param>
        /// <returns>Object Type</returns>
        BankAccountTypeViewModel GetByUniqueCode(string Code);

        /// <summary>
        /// Create New Object and Return Updated Object
        /// </summary>
        /// <param name=BankAccountType>Object to Create</param>
        /// <returns>Updated Object</returns>
        BankAccountTypeViewModel Create(BankAccountTypeViewModel BankAccountType);

        /// <summary>
        /// Update Object and Return Updated Object
        /// </summary>
        /// <param name=BankAccountType>Object to Update</param>
        /// <returns>Updated Object</returns>
        BankAccountTypeViewModel Update(BankAccountTypeViewModel BankAccountType);

    }
}