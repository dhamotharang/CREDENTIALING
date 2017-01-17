using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DisciplineService : IDisciplineService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DisciplineService constructor For ServiceUtility
        /// </summary>
        public DisciplineService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Discipline
        /// </summary>
        /// <returns>List of Discipline</returns>
        public List<DisciplineViewModel> GetAll()
        {
            List<DisciplineViewModel> DisciplineList = new List<DisciplineViewModel>();
            Task<string> Discipline = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDisciplines?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Discipline.Result != null)
                {
                    DisciplineList = JsonConvert.DeserializeObject<List<DisciplineViewModel>>(Discipline.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DisciplineList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DisciplineCode">Discipline's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DisciplineViewModel GetByUniqueCode(string Code)
        {
            DisciplineViewModel _object = new DisciplineViewModel();
            Task<string> Discipline = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDiscipline?DisciplineCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Discipline.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DisciplineViewModel>(Discipline.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Discipline and Return Updated Discipline
        /// </summary>
        /// <param name="Discipline">Discipline to Create</param>
        /// <returns>Updated Discipline</returns>
        public DisciplineViewModel Create(DisciplineViewModel Discipline)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDiscipline", Discipline);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DisciplineViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Discipline and Return Updated Discipline
        /// </summary>
        /// <param name="Discipline">Discipline to Update</param>
        /// <returns>Updated Discipline</returns>
        public DisciplineViewModel Update(DisciplineViewModel Discipline)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDiscipline", Discipline);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DisciplineViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}