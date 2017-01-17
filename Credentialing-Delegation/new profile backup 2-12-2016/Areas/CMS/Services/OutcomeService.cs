using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class OutcomeService : IOutcomeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// OutcomeService constructor For ServiceUtility
        /// </summary>
        public OutcomeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Outcome
        /// </summary>
        /// <returns>List of Outcome</returns>
        public List<OutcomeViewModel> GetAll()
        {
            List<OutcomeViewModel> OutcomeList = new List<OutcomeViewModel>();
            Task<string> Outcome = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllOutcomes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Outcome.Result != null)
                {
                    OutcomeList = JsonConvert.DeserializeObject<List<OutcomeViewModel>>(Outcome.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OutcomeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="OutcomeCode">Outcome's Code Parameter</param>
        /// <returns>Object Type</returns>
        public OutcomeViewModel GetByUniqueCode(string Code)
        {
            OutcomeViewModel _object = new OutcomeViewModel();
            Task<string> Outcome = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetOutcome?OutcomeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Outcome.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<OutcomeViewModel>(Outcome.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Outcome and Return Updated Outcome
        /// </summary>
        /// <param name="Outcome">Outcome to Create</param>
        /// <returns>Updated Outcome</returns>
        public OutcomeViewModel Create(OutcomeViewModel Outcome)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddOutcome", Outcome);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<OutcomeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Outcome and Return Updated Outcome
        /// </summary>
        /// <param name="Outcome">Outcome to Update</param>
        /// <returns>Updated Outcome</returns>
        public OutcomeViewModel Update(OutcomeViewModel Outcome)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateOutcome", Outcome);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<OutcomeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}