using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ReviewTypeService : IReviewTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ReviewTypeService constructor For ServiceUtility
        /// </summary>
        public ReviewTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ReviewType
        /// </summary>
        /// <returns>List of ReviewType</returns>
        public List<ReviewTypeViewModel> GetAll()
        {
            List<ReviewTypeViewModel> ReviewTypeList = new List<ReviewTypeViewModel>();
            Task<string> ReviewType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllReviewTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ReviewType.Result != null)
                {
                    ReviewTypeList = JsonConvert.DeserializeObject<List<ReviewTypeViewModel>>(ReviewType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReviewTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ReviewTypeCode">ReviewType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ReviewTypeViewModel GetByUniqueCode(string Code)
        {
            ReviewTypeViewModel _object = new ReviewTypeViewModel();
            Task<string> ReviewType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetReviewType?ReviewTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ReviewType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ReviewTypeViewModel>(ReviewType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ReviewType and Return Updated ReviewType
        /// </summary>
        /// <param name="ReviewType">ReviewType to Create</param>
        /// <returns>Updated ReviewType</returns>
        public ReviewTypeViewModel Create(ReviewTypeViewModel ReviewType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddReviewType", ReviewType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReviewTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ReviewType and Return Updated ReviewType
        /// </summary>
        /// <param name="ReviewType">ReviewType to Update</param>
        /// <returns>Updated ReviewType</returns>
        public ReviewTypeViewModel Update(ReviewTypeViewModel ReviewType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateReviewType", ReviewType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReviewTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}