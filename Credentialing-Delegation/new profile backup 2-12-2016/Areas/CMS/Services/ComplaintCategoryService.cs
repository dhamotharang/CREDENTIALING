using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ComplaintCategoryService : IComplaintCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ComplaintCategoryService constructor For ServiceUtility
        /// </summary>
        public ComplaintCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ComplaintCategory
        /// </summary>
        /// <returns>List of ComplaintCategory</returns>
        public List<ComplaintCategoryViewModel> GetAll()
        {
            List<ComplaintCategoryViewModel> ComplaintCategoryList = new List<ComplaintCategoryViewModel>();
            Task<string> ComplaintCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllComplaintCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ComplaintCategory.Result != null)
                {
                    ComplaintCategoryList = JsonConvert.DeserializeObject<List<ComplaintCategoryViewModel>>(ComplaintCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ComplaintCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ComplaintCategoryCode">ComplaintCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ComplaintCategoryViewModel GetByUniqueCode(string Code)
        {
            ComplaintCategoryViewModel _object = new ComplaintCategoryViewModel();
            Task<string> ComplaintCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetComplaintCategory?ComplaintCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ComplaintCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ComplaintCategoryViewModel>(ComplaintCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ComplaintCategory and Return Updated ComplaintCategory
        /// </summary>
        /// <param name="ComplaintCategory">ComplaintCategory to Create</param>
        /// <returns>Updated ComplaintCategory</returns>
        public ComplaintCategoryViewModel Create(ComplaintCategoryViewModel ComplaintCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddComplaintCategory", ComplaintCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ComplaintCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ComplaintCategory and Return Updated ComplaintCategory
        /// </summary>
        /// <param name="ComplaintCategory">ComplaintCategory to Update</param>
        /// <returns>Updated ComplaintCategory</returns>
        public ComplaintCategoryViewModel Update(ComplaintCategoryViewModel ComplaintCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateComplaintCategory", ComplaintCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ComplaintCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}