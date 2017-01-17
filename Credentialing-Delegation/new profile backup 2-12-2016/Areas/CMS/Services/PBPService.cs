using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class PBPService : IPBPService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// PBPService constructor For ServiceUtility
        /// </summary>
        public PBPService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of PBP
        /// </summary>
        /// <returns>List of PBP</returns>
        public List<PBPViewModel> GetAll()
        {
            List<PBPViewModel> PBPList = new List<PBPViewModel>();
            Task<string> PBP = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPBPs?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (PBP.Result != null)
                {
                    PBPList = JsonConvert.DeserializeObject<List<PBPViewModel>>(PBP.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PBPList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="PBPCode">PBP's Code Parameter</param>
        /// <returns>Object Type</returns>
        public PBPViewModel GetByUniqueCode(string Code)
        {
            PBPViewModel _object = new PBPViewModel();
            Task<string> PBP = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPBP?PBPCode=" + Code + "");
                return msg;
            });
            try
            {
                if (PBP.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<PBPViewModel>(PBP.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New PBP and Return Updated PBP
        /// </summary>
        /// <param name="PBP">PBP to Create</param>
        /// <returns>Updated PBP</returns>
        public PBPViewModel Create(PBPViewModel PBP)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPBP", PBP);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PBPViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update PBP and Return Updated PBP
        /// </summary>
        /// <param name="PBP">PBP to Update</param>
        /// <returns>Updated PBP</returns>
        public PBPViewModel Update(PBPViewModel PBP)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePBP", PBP);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PBPViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}