using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class SourcesService : ISourcesService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// SourcesService constructor For ServiceUtility
        /// </summary>
        public SourcesService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of Sources
        /// </summary>
        /// <returns>List of Sources</returns>
        public List<SourcesViewModel> GetAll()
        {
            List<SourcesViewModel> SourcesList = new List<SourcesViewModel>();
            Task<string> Sources = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllSourcess?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (Sources.Result != null)
                {
                    SourcesList = JsonConvert.DeserializeObject<List<SourcesViewModel>>(Sources.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return SourcesList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="SourcesCode">Sources's Code Parameter</param>
        /// <returns>Object Type</returns>
        public SourcesViewModel GetByUniqueCode(string Code)
        {
            SourcesViewModel _object = new SourcesViewModel();
            Task<string> Sources = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetSources?SourcesCode=" + Code + "");
                return msg;
            });
            try
            {
                if (Sources.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<SourcesViewModel>(Sources.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New Sources and Return Updated Sources
        /// </summary>
        /// <param name="Sources">Sources to Create</param>
        /// <returns>Updated Sources</returns>
        public SourcesViewModel Create(SourcesViewModel Sources)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddSources", Sources);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SourcesViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update Sources and Return Updated Sources
        /// </summary>
        /// <param name="Sources">Sources to Update</param>
        /// <returns>Updated Sources</returns>
        public SourcesViewModel Update(SourcesViewModel Sources)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateSources", Sources);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SourcesViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}