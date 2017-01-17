using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class TypeOfCareService : ITypeOfCareService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// TypeOfCareService constructor For ServiceUtility
        /// </summary>
        public TypeOfCareService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of TypeOfCare
        /// </summary>
        /// <returns>List of TypeOfCare</returns>
        public List<TypeOfCareViewModel> GetAll()
        {
            List<TypeOfCareViewModel> TypeOfCareList = new List<TypeOfCareViewModel>();
            Task<string> TypeOfCare = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllTypeOfCares?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (TypeOfCare.Result != null)
                {
                    TypeOfCareList = JsonConvert.DeserializeObject<List<TypeOfCareViewModel>>(TypeOfCare.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return TypeOfCareList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="TypeOfCareCode">TypeOfCare's Code Parameter</param>
        /// <returns>Object Type</returns>
        public TypeOfCareViewModel GetByUniqueCode(string Code)
        {
            TypeOfCareViewModel _object = new TypeOfCareViewModel();
            Task<string> TypeOfCare = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetTypeOfCare?TypeOfCareCode=" + Code + "");
                return msg;
            });
            try
            {
                if (TypeOfCare.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<TypeOfCareViewModel>(TypeOfCare.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New TypeOfCare and Return Updated TypeOfCare
        /// </summary>
        /// <param name="TypeOfCare">TypeOfCare to Create</param>
        /// <returns>Updated TypeOfCare</returns>
        public TypeOfCareViewModel Create(TypeOfCareViewModel TypeOfCare)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddTypeOfCare", TypeOfCare);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<TypeOfCareViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update TypeOfCare and Return Updated TypeOfCare
        /// </summary>
        /// <param name="TypeOfCare">TypeOfCare to Update</param>
        /// <returns>Updated TypeOfCare</returns>
        public TypeOfCareViewModel Update(TypeOfCareViewModel TypeOfCare)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateTypeOfCare", TypeOfCare);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<TypeOfCareViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}