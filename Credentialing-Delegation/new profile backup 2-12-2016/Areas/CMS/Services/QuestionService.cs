using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class QuestionService : IQuestionService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// QuestionService constructor For ServiceUtility
        /// </summary>
        public QuestionService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Question
        /// </summary>
        /// <returns>List of Question</returns>
        public List<QuestionViewModel> GetAll()
        {
            List<QuestionViewModel> QuestionList = new List<QuestionViewModel>();
            Task<string> Question = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllQuestions?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Question.Result != null)
                {
                    QuestionList = JsonConvert.DeserializeObject<List<QuestionViewModel>>(Question.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return QuestionList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="QuestionCode">Question's Code Parameter</param>
        /// <returns>Object Type</returns>
        public QuestionViewModel GetByUniqueCode(string Code)
        {
            QuestionViewModel _object = new QuestionViewModel();
            Task<string> Question = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetQuestion?QuestionCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Question.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<QuestionViewModel>(Question.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Question and Return Updated Question
        /// </summary>
        /// <param name="Question">Question to Create</param>
        /// <returns>Updated Question</returns>
        public QuestionViewModel Create(QuestionViewModel Question)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddQuestion", Question);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QuestionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Question and Return Updated Question
        /// </summary>
        /// <param name="Question">Question to Update</param>
        /// <returns>Updated Question</returns>
        public QuestionViewModel Update(QuestionViewModel Question)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateQuestion", Question);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QuestionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}