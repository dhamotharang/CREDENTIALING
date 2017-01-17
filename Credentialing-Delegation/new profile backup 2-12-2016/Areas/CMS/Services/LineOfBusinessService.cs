using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class LineOfBusinessService : ILineOfBusinessService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// LineOfBusinessService constructor For ServiceUtility
        /// </summary>
        public LineOfBusinessService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of LineOfBusiness
        /// </summary>
        /// <returns>List of LineOfBusiness</returns>
        public List<LineOfBusinessViewModel> GetAll()
        {
            List<LineOfBusinessViewModel> LineOfBusinessList = new List<LineOfBusinessViewModel>();
            Task<string> LineOfBusiness = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllLineOfBusinesss?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (LineOfBusiness.Result != null)
                {
                    LineOfBusinessList = JsonConvert.DeserializeObject<List<LineOfBusinessViewModel>>(LineOfBusiness.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return LineOfBusinessList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="LineOfBusinessCode">LineOfBusiness's Code Parameter</param>
        /// <returns>Object Type</returns>
        public LineOfBusinessViewModel GetByUniqueCode(string Code)
        {
            LineOfBusinessViewModel _object = new LineOfBusinessViewModel();
            Task<string> LineOfBusiness = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetLineOfBusiness?LineOfBusinessCode=" + Code + "");
                return msg;
            });
            try
            {
                if (LineOfBusiness.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<LineOfBusinessViewModel>(LineOfBusiness.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New LineOfBusiness and Return Updated LineOfBusiness
        /// </summary>
        /// <param name="LineOfBusiness">LineOfBusiness to Create</param>
        /// <returns>Updated LineOfBusiness</returns>
        public LineOfBusinessViewModel Create(LineOfBusinessViewModel LineOfBusiness)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddLineOfBusiness", LineOfBusiness);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LineOfBusinessViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update LineOfBusiness and Return Updated LineOfBusiness
        /// </summary>
        /// <param name="LineOfBusiness">LineOfBusiness to Update</param>
        /// <returns>Updated LineOfBusiness</returns>
        public LineOfBusinessViewModel Update(LineOfBusinessViewModel LineOfBusiness)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateLineOfBusiness", LineOfBusiness);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<LineOfBusinessViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}