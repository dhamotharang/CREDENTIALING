using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class RelationshipService : IRelationshipService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// RelationshipService constructor For ServiceUtility
        /// </summary>
        public RelationshipService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Relationship
        /// </summary>
        /// <returns>List of Relationship</returns>
        public List<RelationshipViewModel> GetAll()
        {
            List<RelationshipViewModel> RelationshipList = new List<RelationshipViewModel>();
            Task<string> Relationship = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllRelationships?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Relationship.Result != null)
                {
                    RelationshipList = JsonConvert.DeserializeObject<List<RelationshipViewModel>>(Relationship.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RelationshipList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="RelationshipCode">Relationship's Code Parameter</param>
        /// <returns>Object Type</returns>
        public RelationshipViewModel GetByUniqueCode(string Code)
        {
            RelationshipViewModel _object = new RelationshipViewModel();
            Task<string> Relationship = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetRelationship?RelationshipCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Relationship.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<RelationshipViewModel>(Relationship.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Relationship and Return Updated Relationship
        /// </summary>
        /// <param name="Relationship">Relationship to Create</param>
        /// <returns>Updated Relationship</returns>
        public RelationshipViewModel Create(RelationshipViewModel Relationship)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddRelationship", Relationship);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RelationshipViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Relationship and Return Updated Relationship
        /// </summary>
        /// <param name="Relationship">Relationship to Update</param>
        /// <returns>Updated Relationship</returns>
        public RelationshipViewModel Update(RelationshipViewModel Relationship)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateRelationship", Relationship);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<RelationshipViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}