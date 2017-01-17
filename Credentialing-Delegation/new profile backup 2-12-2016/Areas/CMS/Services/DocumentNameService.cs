using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DocumentNameService : IDocumentNameService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DocumentNameService constructor For ServiceUtility
        /// </summary>
        public DocumentNameService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DocumentName
        /// </summary>
        /// <returns>List of DocumentName</returns>
        public List<DocumentNameViewModel> GetAll()
        {
            List<DocumentNameViewModel> DocumentNameList = new List<DocumentNameViewModel>();
            Task<string> DocumentName = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDocumentNames?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DocumentName.Result != null)
                {
                    DocumentNameList = JsonConvert.DeserializeObject<List<DocumentNameViewModel>>(DocumentName.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DocumentNameList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DocumentNameCode">DocumentName's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DocumentNameViewModel GetByUniqueCode(string Code)
        {
            DocumentNameViewModel _object = new DocumentNameViewModel();
            Task<string> DocumentName = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDocumentName?DocumentNameCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DocumentName.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DocumentNameViewModel>(DocumentName.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DocumentName and Return Updated DocumentName
        /// </summary>
        /// <param name="DocumentName">DocumentName to Create</param>
        /// <returns>Updated DocumentName</returns>
        public DocumentNameViewModel Create(DocumentNameViewModel DocumentName)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDocumentName", DocumentName);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DocumentNameViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DocumentName and Return Updated DocumentName
        /// </summary>
        /// <param name="DocumentName">DocumentName to Update</param>
        /// <returns>Updated DocumentName</returns>
        public DocumentNameViewModel Update(DocumentNameViewModel DocumentName)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDocumentName", DocumentName);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DocumentNameViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}