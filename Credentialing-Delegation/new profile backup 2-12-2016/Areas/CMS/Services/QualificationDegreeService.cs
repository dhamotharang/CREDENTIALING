using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class QualificationDegreeService : IQualificationDegreeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// QualificationDegreeService constructor For ServiceUtility
        /// </summary>
        public QualificationDegreeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of QualificationDegree
        /// </summary>
        /// <returns>List of QualificationDegree</returns>
        public List<QualificationDegreeViewModel> GetAll()
        {
            List<QualificationDegreeViewModel> QualificationDegreeList = new List<QualificationDegreeViewModel>();
            Task<string> QualificationDegree = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllQualificationDegrees?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (QualificationDegree.Result != null)
                {
                    QualificationDegreeList = JsonConvert.DeserializeObject<List<QualificationDegreeViewModel>>(QualificationDegree.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return QualificationDegreeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="QualificationDegreeCode">QualificationDegree's Code Parameter</param>
        /// <returns>Object Type</returns>
        public QualificationDegreeViewModel GetByUniqueCode(string Code)
        {
            QualificationDegreeViewModel _object = new QualificationDegreeViewModel();
            Task<string> QualificationDegree = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetQualificationDegree?QualificationDegreeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (QualificationDegree.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<QualificationDegreeViewModel>(QualificationDegree.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New QualificationDegree and Return Updated QualificationDegree
        /// </summary>
        /// <param name="QualificationDegree">QualificationDegree to Create</param>
        /// <returns>Updated QualificationDegree</returns>
        public QualificationDegreeViewModel Create(QualificationDegreeViewModel QualificationDegree)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddQualificationDegree", QualificationDegree);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QualificationDegreeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update QualificationDegree and Return Updated QualificationDegree
        /// </summary>
        /// <param name="QualificationDegree">QualificationDegree to Update</param>
        /// <returns>Updated QualificationDegree</returns>
        public QualificationDegreeViewModel Update(QualificationDegreeViewModel QualificationDegree)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateQualificationDegree", QualificationDegree);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<QualificationDegreeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}