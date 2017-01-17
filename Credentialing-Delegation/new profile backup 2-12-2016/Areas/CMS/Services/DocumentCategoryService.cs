using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class DocumentCategoryService : IDocumentCategoryService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// DocumentCategoryService constructor For ServiceUtility
        /// </summary>
        public DocumentCategoryService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of DocumentCategory
        /// </summary>
        /// <returns>List of DocumentCategory</returns>
        public List<DocumentCategoryViewModel> GetAll()
        {
            List<DocumentCategoryViewModel> DocumentCategoryList = new List<DocumentCategoryViewModel>();
            Task<string> DocumentCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllDocumentCategorys?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (DocumentCategory.Result != null)
                {
                    DocumentCategoryList = JsonConvert.DeserializeObject<List<DocumentCategoryViewModel>>(DocumentCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DocumentCategoryList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="DocumentCategoryCode">DocumentCategory's Code Parameter</param>
        /// <returns>Object Type</returns>
        public DocumentCategoryViewModel GetByUniqueCode(string Code)
        {
            DocumentCategoryViewModel _object = new DocumentCategoryViewModel();
            Task<string> DocumentCategory = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetDocumentCategory?DocumentCategoryCode=" + Code + "");
                return msg;
            });
            try
            {
                if (DocumentCategory.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<DocumentCategoryViewModel>(DocumentCategory.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New DocumentCategory and Return Updated DocumentCategory
        /// </summary>
        /// <param name="DocumentCategory">DocumentCategory to Create</param>
        /// <returns>Updated DocumentCategory</returns>
        public DocumentCategoryViewModel Create(DocumentCategoryViewModel DocumentCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddDocumentCategory", DocumentCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DocumentCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update DocumentCategory and Return Updated DocumentCategory
        /// </summary>
        /// <param name="DocumentCategory">DocumentCategory to Update</param>
        /// <returns>Updated DocumentCategory</returns>
        public DocumentCategoryViewModel Update(DocumentCategoryViewModel DocumentCategory)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateDocumentCategory", DocumentCategory);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<DocumentCategoryViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}