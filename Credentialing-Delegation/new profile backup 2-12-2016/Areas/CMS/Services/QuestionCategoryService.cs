using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class QuestionCategoryService : IQuestionCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// QuestionCategoryService constructor For ServiceUtility
        /// </summary>
        public QuestionCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of QuestionCategory
        /// </summary>
        /// <returns>List of QuestionCategory</returns>
        public List<QuestionCategoryViewModel> GetAll()
        {
            List<QuestionCategoryViewModel> QuestionCategoryList = new List<QuestionCategoryViewModel>();
            Task<string> QuestionCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllQuestionCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (QuestionCategory.Result != null)
                {
                    QuestionCategoryList = JsonConvert.DeserializeObject<List<QuestionCategoryViewModel>>(QuestionCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return QuestionCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="QuestionCategoryCode">QuestionCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public QuestionCategoryViewModel GetByUniqueCode(string Code)
        {
            QuestionCategoryViewModel _object = new QuestionCategoryViewModel();
            Task<string> QuestionCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetQuestionCategory?QuestionCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (QuestionCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<QuestionCategoryViewModel>(QuestionCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New QuestionCategory and Return Updated QuestionCategory
        /// </summary>
        /// <param name="QuestionCategory">QuestionCategory to Create</param>
        /// <returns>Updated QuestionCategory</returns>
        public QuestionCategoryViewModel Create(QuestionCategoryViewModel QuestionCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddQuestionCategory", QuestionCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QuestionCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update QuestionCategory and Return Updated QuestionCategory
        /// </summary>
        /// <param name="QuestionCategory">QuestionCategory to Update</param>
        /// <returns>Updated QuestionCategory</returns>
        public QuestionCategoryViewModel Update(QuestionCategoryViewModel QuestionCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateQuestionCategory", QuestionCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QuestionCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}