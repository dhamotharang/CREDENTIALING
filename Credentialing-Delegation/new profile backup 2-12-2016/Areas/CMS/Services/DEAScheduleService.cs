using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DEAScheduleService : IDEAScheduleService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DEAScheduleService constructor For ServiceUtility
        /// </summary>
        public DEAScheduleService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DEASchedule
        /// </summary>
        /// <returns>List of DEASchedule</returns>
        public List<DEAScheduleViewModel> GetAll()
        {
            List<DEAScheduleViewModel> DEAScheduleList = new List<DEAScheduleViewModel>();
            Task<string> DEASchedule = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDEASchedules?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DEASchedule.Result != null)
                {
                    DEAScheduleList = JsonConvert.DeserializeObject<List<DEAScheduleViewModel>>(DEASchedule.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DEAScheduleList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DEAScheduleCode">DEASchedule's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DEAScheduleViewModel GetByUniqueCode(string Code)
        {
            DEAScheduleViewModel _object = new DEAScheduleViewModel();
            Task<string> DEASchedule = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDEASchedule?DEAScheduleCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DEASchedule.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DEAScheduleViewModel>(DEASchedule.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DEASchedule and Return Updated DEASchedule
        /// </summary>
        /// <param name="DEASchedule">DEASchedule to Create</param>
        /// <returns>Updated DEASchedule</returns>
        public DEAScheduleViewModel Create(DEAScheduleViewModel DEASchedule)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDEASchedule", DEASchedule);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DEAScheduleViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DEASchedule and Return Updated DEASchedule
        /// </summary>
        /// <param name="DEASchedule">DEASchedule to Update</param>
        /// <returns>Updated DEASchedule</returns>
        public DEAScheduleViewModel Update(DEAScheduleViewModel DEASchedule)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDEASchedule", DEASchedule);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DEAScheduleViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}