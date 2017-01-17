using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DateQualifierService : IDateQualifierService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DateQualifierService constructor For ServiceUtility
        /// </summary>
        public DateQualifierService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DateQualifier
        /// </summary>
        /// <returns>List of DateQualifier</returns>
        public List<DateQualifierViewModel> GetAll()
        {
            List<DateQualifierViewModel> DateQualifierList = new List<DateQualifierViewModel>();
            Task<string> DateQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDateQualifiers?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DateQualifier.Result != null)
                {
                    DateQualifierList = JsonConvert.DeserializeObject<List<DateQualifierViewModel>>(DateQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DateQualifierList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DateQualifierCode">DateQualifier's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DateQualifierViewModel GetByUniqueCode(string Code)
        {
            DateQualifierViewModel _object = new DateQualifierViewModel();
            Task<string> DateQualifier = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDateQualifier?DateQualifierCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DateQualifier.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DateQualifierViewModel>(DateQualifier.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DateQualifier and Return Updated DateQualifier
        /// </summary>
        /// <param name="DateQualifier">DateQualifier to Create</param>
        /// <returns>Updated DateQualifier</returns>
        public DateQualifierViewModel Create(DateQualifierViewModel DateQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDateQualifier", DateQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DateQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DateQualifier and Return Updated DateQualifier
        /// </summary>
        /// <param name="DateQualifier">DateQualifier to Update</param>
        /// <returns>Updated DateQualifier</returns>
        public DateQualifierViewModel Update(DateQualifierViewModel DateQualifier)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDateQualifier", DateQualifier);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DateQualifierViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}