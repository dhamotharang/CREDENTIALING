using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class QuestionTypeService : IQuestionTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// QuestionTypeService constructor For ServiceUtility
        /// </summary>
        public QuestionTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of QuestionType
        /// </summary>
        /// <returns>List of QuestionType</returns>
        public List<QuestionTypeViewModel> GetAll()
        {
            List<QuestionTypeViewModel> QuestionTypeList = new List<QuestionTypeViewModel>();
            Task<string> QuestionType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllQuestionTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (QuestionType.Result != null)
                {
                    QuestionTypeList = JsonConvert.DeserializeObject<List<QuestionTypeViewModel>>(QuestionType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return QuestionTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="QuestionTypeCode">QuestionType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public QuestionTypeViewModel GetByUniqueCode(string Code)
        {
            QuestionTypeViewModel _object = new QuestionTypeViewModel();
            Task<string> QuestionType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetQuestionType?QuestionTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (QuestionType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<QuestionTypeViewModel>(QuestionType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New QuestionType and Return Updated QuestionType
        /// </summary>
        /// <param name="QuestionType">QuestionType to Create</param>
        /// <returns>Updated QuestionType</returns>
        public QuestionTypeViewModel Create(QuestionTypeViewModel QuestionType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddQuestionType", QuestionType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QuestionTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update QuestionType and Return Updated QuestionType
        /// </summary>
        /// <param name="QuestionType">QuestionType to Update</param>
        /// <returns>Updated QuestionType</returns>
        public QuestionTypeViewModel Update(QuestionTypeViewModel QuestionType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateQuestionType", QuestionType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QuestionTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}