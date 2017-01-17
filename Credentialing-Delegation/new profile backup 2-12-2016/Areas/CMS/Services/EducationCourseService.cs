using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class EducationCourseService : IEducationCourseService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// EducationCourseService constructor For ServiceUtility
        /// </summary>
        public EducationCourseService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of EducationCourse
        /// </summary>
        /// <returns>List of EducationCourse</returns>
        public List<EducationCourseViewModel> GetAll()
        {
            List<EducationCourseViewModel> EducationCourseList = new List<EducationCourseViewModel>();
            Task<string> EducationCourse = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllEducationCourses?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (EducationCourse.Result != null)
                {
                    EducationCourseList = JsonConvert.DeserializeObject<List<EducationCourseViewModel>>(EducationCourse.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return EducationCourseList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="EducationCourseCode">EducationCourse's Code Parameter</param>
        /// <returns>Object Type</returns>
        public EducationCourseViewModel GetByUniqueCode(string Code)
        {
            EducationCourseViewModel _object = new EducationCourseViewModel();
            Task<string> EducationCourse = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetEducationCourse?EducationCourseCode=" + Code + "");
                return msg;
            });
            try
            {
                if (EducationCourse.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<EducationCourseViewModel>(EducationCourse.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New EducationCourse and Return Updated EducationCourse
        /// </summary>
        /// <param name="EducationCourse">EducationCourse to Create</param>
        /// <returns>Updated EducationCourse</returns>
        public EducationCourseViewModel Create(EducationCourseViewModel EducationCourse)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddEducationCourse", EducationCourse);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EducationCourseViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update EducationCourse and Return Updated EducationCourse
        /// </summary>
        /// <param name="EducationCourse">EducationCourse to Update</param>
        /// <returns>Updated EducationCourse</returns>
        public EducationCourseViewModel Update(EducationCourseViewModel EducationCourse)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateEducationCourse", EducationCourse);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<EducationCourseViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}