using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class MilitaryPresentDutyService : IMilitaryPresentDutyService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// MilitaryPresentDutyService constructor For ServiceUtility
        /// </summary>
        public MilitaryPresentDutyService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of MilitaryPresentDuty
        /// </summary>
        /// <returns>List of MilitaryPresentDuty</returns>
        public List<MilitaryPresentDutyViewModel> GetAll()
        {
            List<MilitaryPresentDutyViewModel> MilitaryPresentDutyList = new List<MilitaryPresentDutyViewModel>();
            Task<string> MilitaryPresentDuty = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllMilitaryPresentDutys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (MilitaryPresentDuty.Result != null)
                {
                    MilitaryPresentDutyList = JsonConvert.DeserializeObject<List<MilitaryPresentDutyViewModel>>(MilitaryPresentDuty.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return MilitaryPresentDutyList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="MilitaryPresentDutyCode">MilitaryPresentDuty's Code Parameter</param>
        /// <returns>Object Type</returns>
        public MilitaryPresentDutyViewModel GetByUniqueCode(string Code)
        {
            MilitaryPresentDutyViewModel _object = new MilitaryPresentDutyViewModel();
            Task<string> MilitaryPresentDuty = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetMilitaryPresentDuty?MilitaryPresentDutyCode=" + Code + "");
                return msg;
            });
            try
            {
                if (MilitaryPresentDuty.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<MilitaryPresentDutyViewModel>(MilitaryPresentDuty.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New MilitaryPresentDuty and Return Updated MilitaryPresentDuty
        /// </summary>
        /// <param name="MilitaryPresentDuty">MilitaryPresentDuty to Create</param>
        /// <returns>Updated MilitaryPresentDuty</returns>
        public MilitaryPresentDutyViewModel Create(MilitaryPresentDutyViewModel MilitaryPresentDuty)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddMilitaryPresentDuty", MilitaryPresentDuty);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryPresentDutyViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update MilitaryPresentDuty and Return Updated MilitaryPresentDuty
        /// </summary>
        /// <param name="MilitaryPresentDuty">MilitaryPresentDuty to Update</param>
        /// <returns>Updated MilitaryPresentDuty</returns>
        public MilitaryPresentDutyViewModel Update(MilitaryPresentDutyViewModel MilitaryPresentDuty)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateMilitaryPresentDuty", MilitaryPresentDuty);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryPresentDutyViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}