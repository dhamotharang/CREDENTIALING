using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using PortalTemplate.Areas.UM.Models.ViewModels.Member;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using PortalTemplate.Areas.UM.Services;
using PortalTemplate.Areas.UM.Services.Contact;
using PortalTemplate.Areas.UM.Services.Note;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class MemberController : Controller
    {
        IContactServices ContactsService = new ContactService();
        INoteServices NotesService = new NoteService();
        //IAttachmentServices AttachmentsService = new AttachmentServices()


        [HttpPost]
        public PartialViewResult SearchMembers(SearchMember searchParams)
        {
            IMemberService memberService=new MemberService();
            ViewBag.SearchResultData = memberService.GetAllMembers(searchParams);
            

            return PartialView("~/Areas/UM/Views/SearchMember/Common/_MemberSearchResult.cshtml", ViewBag);
        }

        public ActionResult GetMemberContacts(string SubscriberID)
        {

            List<AuthorizationContactViewModel> contacts = ContactsService.GetAllContactSubscriberIDServices(SubscriberID);

            return PartialView("~/Areas/Portal/Views/Member/Tabs/_Contacts.cshtml", contacts);
        }
        public ActionResult GetMemberNotes(string SubscriberID)
        {

            List<NoteViewModel> notes = NotesService.GetAllNotesSubscriberService(SubscriberID);

            return PartialView("~/Areas/Portal/Views/Member/Tabs/_Notes.cshtml", notes);
        }

        //public ActionResult GetMemberAttachments(string SubscriberID)
        //{

        //    List<AuthorizationContactViewModel> contacts = service.GetAllContactSubscriberIDServices(SubscriberID);

        //    return PartialView("~/Areas/Portal/Views/Member/Tabs/_Contacts.cshtml", contacts);
        //}


    }
}