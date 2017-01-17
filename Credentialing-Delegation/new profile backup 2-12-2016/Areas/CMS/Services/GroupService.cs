using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class GroupService : IGroupService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// GroupService constructor For ServiceUtility
        /// </summary>
        public GroupService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Group
        /// </summary>
        /// <returns>List of Group</returns>
        public List<GroupViewModel> GetAll()
        {
            List<GroupViewModel> GroupList = new List<GroupViewModel>();
            Task<string> Group = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllGroups?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Group.Result != null)
                {
                    GroupList = JsonConvert.DeserializeObject<List<GroupViewModel>>(Group.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return GroupList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="GroupCode">Group's Code Parameter</param>
        /// <returns>Object Type</returns>
        public GroupViewModel GetByUniqueCode(string Code)
        {
            GroupViewModel _object = new GroupViewModel();
            Task<string> Group = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetGroup?GroupCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Group.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<GroupViewModel>(Group.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Group and Return Updated Group
        /// </summary>
        /// <param name="Group">Group to Create</param>
        /// <returns>Updated Group</returns>
        public GroupViewModel Create(GroupViewModel Group)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddGroup", Group);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<GroupViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Group and Return Updated Group
        /// </summary>
        /// <param name="Group">Group to Update</param>
        /// <returns>Updated Group</returns>
        public GroupViewModel Update(GroupViewModel Group)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateGroup", Group);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<GroupViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}