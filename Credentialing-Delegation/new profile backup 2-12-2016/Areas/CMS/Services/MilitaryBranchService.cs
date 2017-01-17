using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class MilitaryBranchService : IMilitaryBranchService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// MilitaryBranchService constructor For ServiceUtility
        /// </summary>
        public MilitaryBranchService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of MilitaryBranch
        /// </summary>
        /// <returns>List of MilitaryBranch</returns>
        public List<MilitaryBranchViewModel> GetAll()
        {
            List<MilitaryBranchViewModel> MilitaryBranchList = new List<MilitaryBranchViewModel>();
            Task<string> MilitaryBranch = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllMilitaryBranchs?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (MilitaryBranch.Result != null)
                {
                    MilitaryBranchList = JsonConvert.DeserializeObject<List<MilitaryBranchViewModel>>(MilitaryBranch.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return MilitaryBranchList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="MilitaryBranchCode">MilitaryBranch's Code Parameter</param>
        /// <returns>Object Type</returns>
        public MilitaryBranchViewModel GetByUniqueCode(string Code)
        {
            MilitaryBranchViewModel _object = new MilitaryBranchViewModel();
            Task<string> MilitaryBranch = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetMilitaryBranch?MilitaryBranchCode=" + Code + "");
                return msg;
            });
            try
            {
                if (MilitaryBranch.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<MilitaryBranchViewModel>(MilitaryBranch.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New MilitaryBranch and Return Updated MilitaryBranch
        /// </summary>
        /// <param name="MilitaryBranch">MilitaryBranch to Create</param>
        /// <returns>Updated MilitaryBranch</returns>
        public MilitaryBranchViewModel Create(MilitaryBranchViewModel MilitaryBranch)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddMilitaryBranch", MilitaryBranch);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryBranchViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update MilitaryBranch and Return Updated MilitaryBranch
        /// </summary>
        /// <param name="MilitaryBranch">MilitaryBranch to Update</param>
        /// <returns>Updated MilitaryBranch</returns>
        public MilitaryBranchViewModel Update(MilitaryBranchViewModel MilitaryBranch)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateMilitaryBranch", MilitaryBranch);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryBranchViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}