using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class MilitaryRankMilitaryBranchService : IMilitaryRankMilitaryBranchService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// MilitaryRankMilitaryBranchService constructor For ServiceUtility
        /// </summary>
        public MilitaryRankMilitaryBranchService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of MilitaryRankMilitaryBranch
        /// </summary>
        /// <returns>List of MilitaryRankMilitaryBranch</returns>
        public List<MilitaryRankMilitaryBranchViewModel> GetAll()
        {
            List<MilitaryRankMilitaryBranchViewModel> MilitaryRankMilitaryBranchList = new List<MilitaryRankMilitaryBranchViewModel>();
            Task<string> MilitaryRankMilitaryBranch = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllMilitaryRankMilitaryBranchs?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (MilitaryRankMilitaryBranch.Result != null)
                {
                    MilitaryRankMilitaryBranchList = JsonConvert.DeserializeObject<List<MilitaryRankMilitaryBranchViewModel>>(MilitaryRankMilitaryBranch.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return MilitaryRankMilitaryBranchList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="MilitaryRankMilitaryBranchCode">MilitaryRankMilitaryBranch's Code Parameter</param>
        /// <returns>Object Type</returns>
        public MilitaryRankMilitaryBranchViewModel GetByUniqueCode(string Code)
        {
            MilitaryRankMilitaryBranchViewModel _object = new MilitaryRankMilitaryBranchViewModel();
            Task<string> MilitaryRankMilitaryBranch = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetMilitaryRankMilitaryBranch?MilitaryRankMilitaryBranchCode=" + Code + "");
                return msg;
            });
            try
            {
                if (MilitaryRankMilitaryBranch.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<MilitaryRankMilitaryBranchViewModel>(MilitaryRankMilitaryBranch.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New MilitaryRankMilitaryBranch and Return Updated MilitaryRankMilitaryBranch
        /// </summary>
        /// <param name="MilitaryRankMilitaryBranch">MilitaryRankMilitaryBranch to Create</param>
        /// <returns>Updated MilitaryRankMilitaryBranch</returns>
        public MilitaryRankMilitaryBranchViewModel Create(MilitaryRankMilitaryBranchViewModel MilitaryRankMilitaryBranch)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddMilitaryRankMilitaryBranch", MilitaryRankMilitaryBranch);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryRankMilitaryBranchViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update MilitaryRankMilitaryBranch and Return Updated MilitaryRankMilitaryBranch
        /// </summary>
        /// <param name="MilitaryRankMilitaryBranch">MilitaryRankMilitaryBranch to Update</param>
        /// <returns>Updated MilitaryRankMilitaryBranch</returns>
        public MilitaryRankMilitaryBranchViewModel Update(MilitaryRankMilitaryBranchViewModel MilitaryRankMilitaryBranch)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateMilitaryRankMilitaryBranch", MilitaryRankMilitaryBranch);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryRankMilitaryBranchViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}