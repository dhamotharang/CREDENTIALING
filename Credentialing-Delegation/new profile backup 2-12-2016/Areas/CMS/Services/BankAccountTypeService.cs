using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class BankAccountTypeService : IBankAccountTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// BankAccountTypeService constructor For ServiceUtility
        /// </summary>
        public BankAccountTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of BankAccountType
        /// </summary>
        /// <returns>List of BankAccountType</returns>
        public List<BankAccountTypeViewModel> GetAll()
        {
            List<BankAccountTypeViewModel> BankAccountTypeList = new List<BankAccountTypeViewModel>();
            Task<string> BankAccountType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllBankAccountTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (BankAccountType.Result != null)
                {
                    BankAccountTypeList = JsonConvert.DeserializeObject<List<BankAccountTypeViewModel>>(BankAccountType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return BankAccountTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="BankAccountTypeCode">BankAccountType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public BankAccountTypeViewModel GetByUniqueCode(string Code)
        {
            BankAccountTypeViewModel _object = new BankAccountTypeViewModel();
            Task<string> BankAccountType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetBankAccountType?BankAccountTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (BankAccountType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<BankAccountTypeViewModel>(BankAccountType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New BankAccountType and Return Updated BankAccountType
        /// </summary>
        /// <param name="BankAccountType">BankAccountType to Create</param>
        /// <returns>Updated BankAccountType</returns>
        public BankAccountTypeViewModel Create(BankAccountTypeViewModel BankAccountType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddBankAccountType", BankAccountType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<BankAccountTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update BankAccountType and Return Updated BankAccountType
        /// </summary>
        /// <param name="BankAccountType">BankAccountType to Update</param>
        /// <returns>Updated BankAccountType</returns>
        public BankAccountTypeViewModel Update(BankAccountTypeViewModel BankAccountType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateBankAccountType", BankAccountType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<BankAccountTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}