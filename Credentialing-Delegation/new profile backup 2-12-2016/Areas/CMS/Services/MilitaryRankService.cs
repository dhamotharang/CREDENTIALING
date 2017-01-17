using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class MilitaryRankService : IMilitaryRankService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// MilitaryRankService constructor For ServiceUtility
        /// </summary>
        public MilitaryRankService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of MilitaryRank
        /// </summary>
        /// <returns>List of MilitaryRank</returns>
        public List<MilitaryRankViewModel> GetAll()
        {
            List<MilitaryRankViewModel> MilitaryRankList = new List<MilitaryRankViewModel>();
            Task<string> MilitaryRank = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllMilitaryRanks?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (MilitaryRank.Result != null)
                {
                    MilitaryRankList = JsonConvert.DeserializeObject<List<MilitaryRankViewModel>>(MilitaryRank.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return MilitaryRankList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="MilitaryRankCode">MilitaryRank's Code Parameter</param>
        /// <returns>Object Type</returns>
        public MilitaryRankViewModel GetByUniqueCode(string Code)
        {
            MilitaryRankViewModel _object = new MilitaryRankViewModel();
            Task<string> MilitaryRank = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetMilitaryRank?MilitaryRankCode=" + Code + "");
                return msg;
            });
            try
            {
                if (MilitaryRank.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<MilitaryRankViewModel>(MilitaryRank.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New MilitaryRank and Return Updated MilitaryRank
        /// </summary>
        /// <param name="MilitaryRank">MilitaryRank to Create</param>
        /// <returns>Updated MilitaryRank</returns>
        public MilitaryRankViewModel Create(MilitaryRankViewModel MilitaryRank)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddMilitaryRank", MilitaryRank);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryRankViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update MilitaryRank and Return Updated MilitaryRank
        /// </summary>
        /// <param name="MilitaryRank">MilitaryRank to Update</param>
        /// <returns>Updated MilitaryRank</returns>
        public MilitaryRankViewModel Update(MilitaryRankViewModel MilitaryRank)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateMilitaryRank", MilitaryRank);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<MilitaryRankViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}