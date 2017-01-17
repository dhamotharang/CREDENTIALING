using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services.CommonServices
{
    public class NotesServices //: INoteServices
    {
        NoteViewModel NoteModel = new NoteViewModel();
        List<NoteViewModel> NoteModels = new List<NoteViewModel>();
        public NoteViewModel AddNoteService(NoteViewModel model)
        {
            throw new NotImplementedException();
        }

        public NoteViewModel EditNoteService(NoteViewModel model)
        {
            throw new NotImplementedException();
        }

        public NoteViewModel ViewNoteService(NoteViewModel model)
        {
            throw new NotImplementedException();
        }

        public NoteViewModel DeleteNoteService(NoteViewModel model)
        {
            throw new NotImplementedException();
        }

        public List<NoteViewModel> GetAllNotesSubscriberService(string SubscriberID)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Authorization/Notes.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            NoteModels = serial.Deserialize<List<NoteViewModel>>(json);
            return NoteModels;
        }

        public List<NoteViewModel> GetAllNotesService(int AuthorizationID)
        {
            string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/JSONData/Authorization/Notes.txt");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            NoteModels = serial.Deserialize<List<NoteViewModel>>(json);
            return NoteModels;
        }
    }
}