using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class TextSnippetService : ITextSnippetService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// TextSnippetService constructor For ServiceUtility
        /// </summary>
        public TextSnippetService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of TextSnippet
        /// </summary>
        /// <returns>List of TextSnippet</returns>
        public List<TextSnippetViewModel> GetAll()
        {
            List<TextSnippetViewModel> TextSnippetList = new List<TextSnippetViewModel>();
            Task<string> TextSnippet = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllTextSnippets?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (TextSnippet.Result != null)
                {
                    TextSnippetList = JsonConvert.DeserializeObject<List<TextSnippetViewModel>>(TextSnippet.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return TextSnippetList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="TextSnippetCode">TextSnippet's Code Parameter</param>
        /// <returns>Object Type</returns>
        public TextSnippetViewModel GetByUniqueCode(string Code)
        {
            TextSnippetViewModel _object = new TextSnippetViewModel();
            Task<string> TextSnippet = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetTextSnippet?TextSnippetCode=" + Code + "");
                return msg;
            });
            try
            {
                if (TextSnippet.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<TextSnippetViewModel>(TextSnippet.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New TextSnippet and Return Updated TextSnippet
        /// </summary>
        /// <param name="TextSnippet">TextSnippet to Create</param>
        /// <returns>Updated TextSnippet</returns>
        public TextSnippetViewModel Create(TextSnippetViewModel TextSnippet)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddTextSnippet", TextSnippet);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<TextSnippetViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update TextSnippet and Return Updated TextSnippet
        /// </summary>
        /// <param name="TextSnippet">TextSnippet to Update</param>
        /// <returns>Updated TextSnippet</returns>
        public TextSnippetViewModel Update(TextSnippetViewModel TextSnippet)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateTextSnippet", TextSnippet);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<TextSnippetViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}