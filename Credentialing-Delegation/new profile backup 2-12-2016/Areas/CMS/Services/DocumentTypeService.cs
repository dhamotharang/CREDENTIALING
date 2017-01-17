using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DocumentTypeService constructor For ServiceUtility
        /// </summary>
        public DocumentTypeService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DocumentType
        /// </summary>
        /// <returns>List of DocumentType</returns>
        public List<DocumentTypeViewModel> GetAll()
        {
            List<DocumentTypeViewModel> DocumentTypeList = new List<DocumentTypeViewModel>();
            Task<string> DocumentType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDocumentTypes?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DocumentType.Result != null)
                {
                    DocumentTypeList = JsonConvert.DeserializeObject<List<DocumentTypeViewModel>>(DocumentType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DocumentTypeList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DocumentTypeCode">DocumentType's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DocumentTypeViewModel GetByUniqueCode(string Code)
        {
            DocumentTypeViewModel _object = new DocumentTypeViewModel();
            Task<string> DocumentType = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDocumentType?DocumentTypeCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DocumentType.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DocumentTypeViewModel>(DocumentType.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DocumentType and Return Updated DocumentType
        /// </summary>
        /// <param name="DocumentType">DocumentType to Create</param>
        /// <returns>Updated DocumentType</returns>
        public DocumentTypeViewModel Create(DocumentTypeViewModel DocumentType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDocumentType", DocumentType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DocumentTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DocumentType and Return Updated DocumentType
        /// </summary>
        /// <param name="DocumentType">DocumentType to Update</param>
        /// <returns>Updated DocumentType</returns>
        public DocumentTypeViewModel Update(DocumentTypeViewModel DocumentType)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDocumentType", DocumentType);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DocumentTypeViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}