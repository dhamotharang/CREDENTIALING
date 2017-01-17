using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class ReviewLinkService : IReviewLinkService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// ReviewLinkService constructor For ServiceUtility
        /// </summary>
        public ReviewLinkService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of ReviewLink
        /// </summary>
        /// <returns>List of ReviewLink</returns>
        public List<ReviewLinkViewModel> GetAll()
        {
            List<ReviewLinkViewModel> ReviewLinkList = new List<ReviewLinkViewModel>();
            Task<string> ReviewLink = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllReviewLinks?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (ReviewLink.Result != null)
                {
                    ReviewLinkList = JsonConvert.DeserializeObject<List<ReviewLinkViewModel>>(ReviewLink.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ReviewLinkList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="ReviewLinkCode">ReviewLink's Code Parameter</param>
        /// <returns>Object Type</returns>
        public ReviewLinkViewModel GetByUniqueCode(string Code)
        {
            ReviewLinkViewModel _object = new ReviewLinkViewModel();
            Task<string> ReviewLink = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetReviewLink?ReviewLinkCode=" + Code + "");
                return msg;
            });
            try
            {
                if (ReviewLink.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<ReviewLinkViewModel>(ReviewLink.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New ReviewLink and Return Updated ReviewLink
        /// </summary>
        /// <param name="ReviewLink">ReviewLink to Create</param>
        /// <returns>Updated ReviewLink</returns>
        public ReviewLinkViewModel Create(ReviewLinkViewModel ReviewLink)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddReviewLink", ReviewLink);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReviewLinkViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update ReviewLink and Return Updated ReviewLink
        /// </summary>
        /// <param name="ReviewLink">ReviewLink to Update</param>
        /// <returns>Updated ReviewLink</returns>
        public ReviewLinkViewModel Update(ReviewLinkViewModel ReviewLink)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateReviewLink", ReviewLink);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<ReviewLinkViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}