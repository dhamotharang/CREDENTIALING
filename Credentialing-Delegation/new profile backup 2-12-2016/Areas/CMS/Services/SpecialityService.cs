using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class SpecialityService : ISpecialityService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// SpecialityService constructor For ServiceUtility
        /// </summary>
        public SpecialityService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Speciality
        /// </summary>
        /// <returns>List of Speciality</returns>
        public List<SpecialityViewModel> GetAll()
        {
            List<SpecialityViewModel> SpecialityList = new List<SpecialityViewModel>();
            Task<string> Speciality = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllSpecialitys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Speciality.Result != null)
                {
                    SpecialityList = JsonConvert.DeserializeObject<List<SpecialityViewModel>>(Speciality.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return SpecialityList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="SpecialityCode">Speciality's Code Parameter</param>
        /// <returns>Object Type</returns>
        public SpecialityViewModel GetByUniqueCode(string Code)
        {
            SpecialityViewModel _object = new SpecialityViewModel();
            Task<string> Speciality = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetSpeciality?SpecialityCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Speciality.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<SpecialityViewModel>(Speciality.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Speciality and Return Updated Speciality
        /// </summary>
        /// <param name="Speciality">Speciality to Create</param>
        /// <returns>Updated Speciality</returns>
        public SpecialityViewModel Create(SpecialityViewModel Speciality)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddSpeciality", Speciality);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SpecialityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Speciality and Return Updated Speciality
        /// </summary>
        /// <param name="Speciality">Speciality to Update</param>
        /// <returns>Updated Speciality</returns>
        public SpecialityViewModel Update(SpecialityViewModel Speciality)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateSpeciality", Speciality);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SpecialityViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}