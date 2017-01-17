using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface INoteServices
    {
        NoteViewModel AddNoteService(NoteViewModel model);
        NoteViewModel EditNoteService(NoteViewModel model);
        PortalTemplate.Areas.Portal.Models.Note.NoteViewModel ViewNoteService(int NoteID);
        NoteViewModel DeleteNoteService(NoteViewModel model);
        List<NoteViewModel> GetAllNotesSubscriberService(string SubscriberID);
        List<NoteViewModel> GetAllNotesService(int AuthorizationID);
    }
}