using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class PlanService : IPlanService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// PlanService constructor For ServiceUtility
        /// </summary>
        public PlanService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Plan
        /// </summary>
        /// <returns>List of Plan</returns>
        public List<PlanViewModel> GetAll()
        {
            List<PlanViewModel> PlanList = new List<PlanViewModel>();
            Task<string> Plan = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPlans?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Plan.Result != null)
                {
                    PlanList = JsonConvert.DeserializeObject<List<PlanViewModel>>(Plan.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PlanList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="PlanCode">Plan's Code Parameter</param>
        /// <returns>Object Type</returns>
        public PlanViewModel GetByUniqueCode(string Code)
        {
            PlanViewModel _object = new PlanViewModel();
            Task<string> Plan = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPlan?PlanCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Plan.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<PlanViewModel>(Plan.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Plan and Return Updated Plan
        /// </summary>
        /// <param name="Plan">Plan to Create</param>
        /// <returns>Updated Plan</returns>
        public PlanViewModel Create(PlanViewModel Plan)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPlan", Plan);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PlanViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Plan and Return Updated Plan
        /// </summary>
        /// <param name="Plan">Plan to Update</param>
        /// <returns>Updated Plan</returns>
        public PlanViewModel Update(PlanViewModel Plan)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePlan", Plan);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PlanViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}