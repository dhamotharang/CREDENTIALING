using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class PatientRelationService : IPatientRelationService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// PatientRelationService constructor For ServiceUtility
        /// </summary>
        public PatientRelationService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of PatientRelation
        /// </summary>
        /// <returns>List of PatientRelation</returns>
        public List<PatientRelationViewModel> GetAll()
        {
            List<PatientRelationViewModel> PatientRelationList = new List<PatientRelationViewModel>();
            Task<string> PatientRelation = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPatientRelations?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (PatientRelation.Result != null)
                {
                    PatientRelationList = JsonConvert.DeserializeObject<List<PatientRelationViewModel>>(PatientRelation.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PatientRelationList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="PatientRelationCode">PatientRelation's Code Parameter</param>
        /// <returns>Object Type</returns>
        public PatientRelationViewModel GetByUniqueCode(string Code)
        {
            PatientRelationViewModel _object = new PatientRelationViewModel();
            Task<string> PatientRelation = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPatientRelation?PatientRelationCode=" + Code + "");
                return msg;
            });
            try
            {
                if (PatientRelation.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<PatientRelationViewModel>(PatientRelation.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New PatientRelation and Return Updated PatientRelation
        /// </summary>
        /// <param name="PatientRelation">PatientRelation to Create</param>
        /// <returns>Updated PatientRelation</returns>
        public PatientRelationViewModel Create(PatientRelationViewModel PatientRelation)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPatientRelation", PatientRelation);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PatientRelationViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update PatientRelation and Return Updated PatientRelation
        /// </summary>
        /// <param name="PatientRelation">PatientRelation to Update</param>
        /// <returns>Updated PatientRelation</returns>
        public PatientRelationViewModel Update(PatientRelationViewModel PatientRelation)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePatientRelation", PatientRelation);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PatientRelationViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}