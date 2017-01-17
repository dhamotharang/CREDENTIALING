using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class PracticeOpenStatusQuestionService : IPracticeOpenStatusQuestionService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// PracticeOpenStatusQuestionService constructor For ServiceUtility
        /// </summary>
        public PracticeOpenStatusQuestionService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of PracticeOpenStatusQuestion
        /// </summary>
        /// <returns>List of PracticeOpenStatusQuestion</returns>
        public List<PracticeOpenStatusQuestionViewModel> GetAll()
        {
            List<PracticeOpenStatusQuestionViewModel> PracticeOpenStatusQuestionList = new List<PracticeOpenStatusQuestionViewModel>();
            Task<string> PracticeOpenStatusQuestion = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllPracticeOpenStatusQuestions?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (PracticeOpenStatusQuestion.Result != null)
                {
                    PracticeOpenStatusQuestionList = JsonConvert.DeserializeObject<List<PracticeOpenStatusQuestionViewModel>>(PracticeOpenStatusQuestion.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return PracticeOpenStatusQuestionList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="PracticeOpenStatusQuestionCode">PracticeOpenStatusQuestion's Code Parameter</param>
        /// <returns>Object Type</returns>
        public PracticeOpenStatusQuestionViewModel GetByUniqueCode(string Code)
        {
            PracticeOpenStatusQuestionViewModel _object = new PracticeOpenStatusQuestionViewModel();
            Task<string> PracticeOpenStatusQuestion = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetPracticeOpenStatusQuestion?PracticeOpenStatusQuestionCode=" + Code + "");
                return msg;
            });
            try
            {
                if (PracticeOpenStatusQuestion.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<PracticeOpenStatusQuestionViewModel>(PracticeOpenStatusQuestion.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New PracticeOpenStatusQuestion and Return Updated PracticeOpenStatusQuestion
        /// </summary>
        /// <param name="PracticeOpenStatusQuestion">PracticeOpenStatusQuestion to Create</param>
        /// <returns>Updated PracticeOpenStatusQuestion</returns>
        public PracticeOpenStatusQuestionViewModel Create(PracticeOpenStatusQuestionViewModel PracticeOpenStatusQuestion)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddPracticeOpenStatusQuestion", PracticeOpenStatusQuestion);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PracticeOpenStatusQuestionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update PracticeOpenStatusQuestion and Return Updated PracticeOpenStatusQuestion
        /// </summary>
        /// <param name="PracticeOpenStatusQuestion">PracticeOpenStatusQuestion to Update</param>
        /// <returns>Updated PracticeOpenStatusQuestion</returns>
        public PracticeOpenStatusQuestionViewModel Update(PracticeOpenStatusQuestionViewModel PracticeOpenStatusQuestion)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdatePracticeOpenStatusQuestion", PracticeOpenStatusQuestion);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<PracticeOpenStatusQuestionViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}