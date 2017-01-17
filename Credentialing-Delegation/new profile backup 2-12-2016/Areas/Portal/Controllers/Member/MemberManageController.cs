using PortalTemplate.Areas.Portal.IManager.Member;
using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.Portal.Manager.Manager;
using PortalTemplate.Areas.Portal.Models.Member;
using PortalTemplate.Areas.Portal.Models.MemberManager;
using PortalTemplate.Areas.Portal.Models.Note;
using PortalTemplate.Areas.Portal.Services.Member;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Services.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.UM.Services.Contact;
using PortalTemplate.Areas.Portal.Models.Contact;
using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Services;
using PortalTemplate.Areas.UM.Models.PowerDriveService;
using System.Threading.Tasks;
using System.IO;
namespace PortalTemplate.Areas.Portal.Controllers.Member
{
    public class MemberManageController : Controller
    {
        //
        // GET: /Portal/MemberManage/
        IMemberServiceManager profileManager = new MemberServiceManager();
        INoteServices noteservices = new NoteService();
        IContactServices contactservices = new ContactService();
        IPowerDriveService _powerdriveService = new PowerDriveService();
        PortalTemplate.Areas.Portal.IServices.IMemberViewService services = new PortalTemplate.Areas.Portal.Services.Member.MemberService();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMemberDetails(string SubscriberID)
        {
            MemberManagerModel memberManager = profileManager.GetMemberDetailsBySubsriberID(SubscriberID);
            MemberViewModel member = (MemberViewModel)memberManager.ResultObject;
            return View(memberManager.URL, member);
        }
        public ActionResult GetMemberHeaderById(string SubscriberID, string RefID)
        {
            MemberManagerModel memberManager = profileManager.GetMemberHeaderDetailsBySubscriberID(SubscriberID, RefID);

            MemberHeaderViewModel Header = (MemberHeaderViewModel)memberManager.ResultObject;


            return View(memberManager.URL, Header);

        }
        public ActionResult GetPartialViewByTabId(string ID, string SubscriberId)
        {
            MemberManagerModel memberManager = profileManager.GetMemberPartialTab(ID, SubscriberId);
            ViewBag.SubscriberID = SubscriberId;
            return View(memberManager.URL, memberManager.ResultObject);
        }
        public ActionResult GetFilteredDataViewBySubsriberID(string ID, string SubscriberId, List<string> ModuleArray)
        {
            MemberManagerModel memberManager = profileManager.GetFilteredDataBySubsriberID(ID, ModuleArray, SubscriberId);
            ViewBag.SubscriberID = SubscriberId;
            return View(memberManager.URL, memberManager.ResultObject);
        }
        [HttpGet]
        public JsonResult GetNoteDetailsByID(int ID, string ModuleName)
        {
            NoteViewModel note = new NoteViewModel();
            if (ModuleName == "UM")
            {
                note = noteservices.ViewNoteService(ID);
            }
            else
            {
                note = (NoteViewModel)services.GetFilteredServiceDataBySubsriberID("NOTE", ModuleName, ID);
            }

            return Json(note, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetContactDetailsByID(int ID, string ModuleName)
        {
            AuthorizationContactViewModel contact = new AuthorizationContactViewModel();
            if (ModuleName == "UM")
            {
                contact = contactservices.ViewContactServices(ID);
            }
            else
            {
                object Servicecontact = services.GetFilteredServiceDataBySubsriberID("CONTACT", ModuleName, ID);
                contact = (AuthorizationContactViewModel)Servicecontact;
            }

            return Json(DateTimeserializer(contact), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewNote()
        {

            return PartialView("~/Areas/UM/Views/Common/Note/_ViewNote.cshtml");
        }
        private string DateTimeserializer<T>(T value)
        {
            string data = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });
            return data;
        }

        [HttpGet]
        public FileResult PreviewDocument(string DocKey, string FileName)
        {
            UserInfo user = new UserInfo();
            user.UserName = "testprev@gmail.com";
            user.ApplicaionOrGroupName = "TestPrev";
            var stream=Task.Run(async () =>
            {
                return await (_powerdriveService.PreviewFile(DocKey, user));              
            });                 
            var data = MimeMapping.GetMimeMapping(FileName);
            return File(stream.Result, data);
        }



        public void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }


    }
}