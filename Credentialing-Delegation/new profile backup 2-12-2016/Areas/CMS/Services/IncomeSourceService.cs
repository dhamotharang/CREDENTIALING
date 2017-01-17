using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class IncomeSourceService : IIncomeSourceService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// IncomeSourceService constructor For ServiceUtility
        /// </summary>
        public IncomeSourceService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of IncomeSource
        /// </summary>
        /// <returns>List of IncomeSource</returns>
        public List<IncomeSourceViewModel> GetAll()
        {
            List<IncomeSourceViewModel> IncomeSourceList = new List<IncomeSourceViewModel>();
            Task<string> IncomeSource = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllIncomeSources?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (IncomeSource.Result != null)
                {
                    IncomeSourceList = JsonConvert.DeserializeObject<List<IncomeSourceViewModel>>(IncomeSource.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return IncomeSourceList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="IncomeSourceCode">IncomeSource's Code Parameter</param>
        /// <returns>Object Type</returns>
        public IncomeSourceViewModel GetByUniqueCode(string Code)
        {
            IncomeSourceViewModel _object = new IncomeSourceViewModel();
            Task<string> IncomeSource = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetIncomeSource?IncomeSourceCode=" + Code + "");
                return msg;
            });
            try
            {
                if (IncomeSource.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<IncomeSourceViewModel>(IncomeSource.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New IncomeSource and Return Updated IncomeSource
        /// </summary>
        /// <param name="IncomeSource">IncomeSource to Create</param>
        /// <returns>Updated IncomeSource</returns>
        public IncomeSourceViewModel Create(IncomeSourceViewModel IncomeSource)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddIncomeSource", IncomeSource);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<IncomeSourceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update IncomeSource and Return Updated IncomeSource
        /// </summary>
        /// <param name="IncomeSource">IncomeSource to Update</param>
        /// <returns>Updated IncomeSource</returns>
        public IncomeSourceViewModel Update(IncomeSourceViewModel IncomeSource)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateIncomeSource", IncomeSource);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<IncomeSourceViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}