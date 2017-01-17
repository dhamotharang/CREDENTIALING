using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class FeeScheduleService : IFeeScheduleService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// FeeScheduleService constructor For ServiceUtility
        /// </summary>
        public FeeScheduleService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of FeeSchedule
        /// </summary>
        /// <returns>List of FeeSchedule</returns>
        public List<FeeScheduleViewModel> GetAll()
        {
            List<FeeScheduleViewModel> FeeScheduleList = new List<FeeScheduleViewModel>();
            Task<string> FeeSchedule = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllFeeSchedules?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (FeeSchedule.Result != null)
                {
                    FeeScheduleList = JsonConvert.DeserializeObject<List<FeeScheduleViewModel>>(FeeSchedule.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return FeeScheduleList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="FeeScheduleCode">FeeSchedule's Code Parameter</param>
        /// <returns>Object Type</returns>
        public FeeScheduleViewModel GetByUniqueCode(string Code)
        {
            FeeScheduleViewModel _object = new FeeScheduleViewModel();
            Task<string> FeeSchedule = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetFeeSchedule?FeeScheduleCode=" + Code + "");
                return msg;
            });
            try
            {
                if (FeeSchedule.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<FeeScheduleViewModel>(FeeSchedule.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New FeeSchedule and Return Updated FeeSchedule
        /// </summary>
        /// <param name="FeeSchedule">FeeSchedule to Create</param>
        /// <returns>Updated FeeSchedule</returns>
        public FeeScheduleViewModel Create(FeeScheduleViewModel FeeSchedule)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddFeeSchedule", FeeSchedule);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<FeeScheduleViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update FeeSchedule and Return Updated FeeSchedule
        /// </summary>
        /// <param name="FeeSchedule">FeeSchedule to Update</param>
        /// <returns>Updated FeeSchedule</returns>
        public FeeScheduleViewModel Update(FeeScheduleViewModel FeeSchedule)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateFeeSchedule", FeeSchedule);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<FeeScheduleViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}