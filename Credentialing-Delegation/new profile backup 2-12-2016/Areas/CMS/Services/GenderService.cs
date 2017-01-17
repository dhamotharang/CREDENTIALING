using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class GenderService : IGenderService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// GenderService constructor For ServiceUtility
        /// </summary>
        public GenderService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Gender
        /// </summary>
        /// <returns>List of Gender</returns>
        public List<GenderViewModel> GetAll()
        {
            List<GenderViewModel> GenderList = new List<GenderViewModel>();
            Task<string> Gender = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllGenders?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Gender.Result != null)
                {
                    GenderList = JsonConvert.DeserializeObject<List<GenderViewModel>>(Gender.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return GenderList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="GenderCode">Gender's Code Parameter</param>
        /// <returns>Object Type</returns>
        public GenderViewModel GetByUniqueCode(string Code)
        {
            GenderViewModel _object = new GenderViewModel();
            Task<string> Gender = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetGender?GenderCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Gender.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<GenderViewModel>(Gender.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Gender and Return Updated Gender
        /// </summary>
        /// <param name="Gender">Gender to Create</param>
        /// <returns>Updated Gender</returns>
        public GenderViewModel Create(GenderViewModel Gender)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddGender", Gender);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<GenderViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Gender and Return Updated Gender
        /// </summary>
        /// <param name="Gender">Gender to Update</param>
        /// <returns>Updated Gender</returns>
        public GenderViewModel Update(GenderViewModel Gender)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateGender", Gender);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<GenderViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}