using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class MilitaryDischargeService : IMilitaryDischargeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// MilitaryDischargeService constructor For ServiceUtility
        /// </summary>
        public MilitaryDischargeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of MilitaryDischarge
        /// </summary>
        /// <returns>List of MilitaryDischarge</returns>
        public List<MilitaryDischargeViewModel> GetAll()
        {
            List<MilitaryDischargeViewModel> MilitaryDischargeList = new List<MilitaryDischargeViewModel>();
            Task<string> MilitaryDischarge = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllMilitaryDischarges?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (MilitaryDischarge.Result != null)
                {
                    MilitaryDischargeList = JsonConvert.DeserializeObject<List<MilitaryDischargeViewModel>>(MilitaryDischarge.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return MilitaryDischargeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="MilitaryDischargeCode">MilitaryDischarge's Code Parameter</param>
        /// <returns>Object Type</returns>
        public MilitaryDischargeViewModel GetByUniqueCode(string Code)
        {
            MilitaryDischargeViewModel _object = new MilitaryDischargeViewModel();
            Task<string> MilitaryDischarge = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetMilitaryDischarge?MilitaryDischargeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (MilitaryDischarge.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<MilitaryDischargeViewModel>(MilitaryDischarge.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New MilitaryDischarge and Return Updated MilitaryDischarge
        /// </summary>
        /// <param name="MilitaryDischarge">MilitaryDischarge to Create</param>
        /// <returns>Updated MilitaryDischarge</returns>
        public MilitaryDischargeViewModel Create(MilitaryDischargeViewModel MilitaryDischarge)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddMilitaryDischarge", MilitaryDischarge);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryDischargeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update MilitaryDischarge and Return Updated MilitaryDischarge
        /// </summary>
        /// <param name="MilitaryDischarge">MilitaryDischarge to Update</param>
        /// <returns>Updated MilitaryDischarge</returns>
        public MilitaryDischargeViewModel Update(MilitaryDischargeViewModel MilitaryDischarge)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateMilitaryDischarge", MilitaryDischarge);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryDischargeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}