using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class StateService : IStateService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// StateService constructor For ServiceUtility
        /// </summary>
        public StateService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of State
        /// </summary>
        /// <returns>List of State</returns>
        public List<StateViewModel> GetAll()
        {
            List<StateViewModel> StateList = new List<StateViewModel>();
            Task<string> State = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllStates?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (State.Result != null)
                {
                    StateList = JsonConvert.DeserializeObject<List<StateViewModel>>(State.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return StateList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="StateCode">State's Code Parameter</param>
        /// <returns>Object Type</returns>
        public StateViewModel GetByUniqueCode(string Code)
        {
            StateViewModel _object = new StateViewModel();
            Task<string> State = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetState?StateCode=" + Code + "");
                return msg;
            });
            try
            {
                if (State.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<StateViewModel>(State.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New State and Return Updated State
        /// </summary>
        /// <param name="State">State to Create</param>
        /// <returns>Updated State</returns>
        public StateViewModel Create(StateViewModel State)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddState", State);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<StateViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update State and Return Updated State
        /// </summary>
        /// <param name="State">State to Update</param>
        /// <returns>Updated State</returns>
        public StateViewModel Update(StateViewModel State)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateState", State);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<StateViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}