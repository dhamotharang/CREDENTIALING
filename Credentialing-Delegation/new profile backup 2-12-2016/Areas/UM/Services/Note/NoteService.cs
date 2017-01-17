using Newtonsoft.Json;
using PortalTemplate.Areas.UM.CustomHelpers;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services.Note
{
    public class NoteService : INoteServices
    {
        private readonly string baseURL;
        private readonly ServiceUtility serviceUtility;

        public NoteService()
        {
            this.baseURL = ConfigurationManager.AppSettings["UMService"].ToString();
            this.serviceUtility = new ServiceUtility();
        }
        public Models.ViewModels.Note.NoteViewModel AddNoteService(Models.ViewModels.Note.NoteViewModel model)
        {
            Models.ViewModels.Note.NoteViewModel noteViewModel = new Models.ViewModels.Note.NoteViewModel();
            Task<string> noteViewModeldata = Task.Run(async () =>
            {
                string msg = await serviceUtility.PostDataToService(baseURL, "api/Note/SaveAuthorizationNote", model);
                return msg;
            });
            if (noteViewModeldata.Result != null)
            {
                noteViewModel = JsonConvert.DeserializeObject<Models.ViewModels.Note.NoteViewModel>(noteViewModeldata.Result);
            }
            return noteViewModel;
        }

        public Models.ViewModels.Note.NoteViewModel EditNoteService(Models.ViewModels.Note.NoteViewModel model)
        {
            Models.ViewModels.Note.NoteViewModel noteViewModel = new Models.ViewModels.Note.NoteViewModel();
            Task<string> noteViewModeldata = Task.Run(async () =>
            {
                string msg = await serviceUtility.PostDataToService(baseURL, "api/Note/Update AuthorizationNote", model);
                return msg;
            });
            if (noteViewModeldata.Result != null)
            {
                noteViewModel = JsonConvert.DeserializeObject<Models.ViewModels.Note.NoteViewModel>(noteViewModeldata.Result);
            }
            return noteViewModel;
        }

        public PortalTemplate.Areas.Portal.Models.Note.NoteViewModel ViewNoteService(int NoteID)
        {
            PortalTemplate.Areas.Portal.Models.Note.NoteViewModel noteViewModel = new PortalTemplate.Areas.Portal.Models.Note.NoteViewModel();
            Task<string> noteViewModeldata = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/Notes/GetAuthorizationNoteByAuthorizationNoteID?NoteID=" + NoteID);
                return msg;
            });
            if (noteViewModeldata.Result != null)
            {
                noteViewModel = JsonConvert.DeserializeObject<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>(noteViewModeldata.Result);
            }
            return noteViewModel;
        }

        public Models.ViewModels.Note.NoteViewModel DeleteNoteService(Models.ViewModels.Note.NoteViewModel model)
        {
            throw new NotImplementedException();
        }

        public List<Models.ViewModels.Note.NoteViewModel> GetAllNotesSubscriberService(string SubscriberID)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Authorization/Notes.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<NoteViewModel> NoteModels = new List<NoteViewModel>(); 
            NoteModels = serial.Deserialize<List<NoteViewModel>>(json);
            return NoteModels;
        }

        public List<Models.ViewModels.Note.NoteViewModel> GetAllNotesService(int AuthorizationID)
        {
            throw new NotImplementedException();
        }
    }
}