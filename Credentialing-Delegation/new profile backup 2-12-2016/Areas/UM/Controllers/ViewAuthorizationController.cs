using PortalTemplate.Areas.UM.Models.ViewModels.Attachment;
using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using PortalTemplate.Areas.UM.Models.ViewModels.Letter;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using PortalTemplate.Areas.UM.Models.ViewModels.ViewAuthorization;
using PortalTemplate.Areas.UM.Services;
using PortalTemplate.Areas.UM.Services.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class ViewAuthorizationController : Controller
    {
        readonly ViewAuthService service = new ViewAuthService();
        public ActionResult GetViewAuth(int ID)
        {
            ViewAuthorizationViewModel ViewAuthModel = new ViewAuthorizationViewModel();
            ViewAuthModel = service.GetAuthByID(ID);
            return PartialView("~/Areas/UM/Views/ViewAuth/ViewAuthorization/_ViewAuth.cshtml", ViewAuthModel);
        }


        public ActionResult GetTabData(string TabId, int AuthID)
        {
            ViewAuthorizationViewModel ViewAuthModel = new ViewAuthorizationViewModel();
            
            try
            {
                switch (TabId)
                {
                    case "ReviewTab":
                        return PartialView("~/Areas/UM/Views/ViewAuth/Review/_Review.cshtml");
                    case "NotesTab":
                        List<NoteViewModel> NoteViewModel = new List<NoteViewModel>();
                        NotesServices NoteService = new NotesServices();
                        NoteViewModel = NoteService.GetAllNotesService(AuthID);
                        return PartialView("~/Areas/UM/Views/Authorization/Form/_NotesArea.cshtml", NoteViewModel);
                    case "ContactsTab":
                        List<AuthorizationContactViewModel> ContactViewModel = new List<AuthorizationContactViewModel>();
                        ContactServices service = new ContactServices();
                        ContactViewModel = service.GetAllContactsServices(AuthID);
                        return PartialView("~/Areas/UM/Views/Authorization/Form/_ContactsArea.cshtml", ContactViewModel);
                    case "AttachmentsTab":
                        List<AttachmentViewModel> AuthAttachmentsData = new List<AttachmentViewModel>();
                        AuthAttachmentsData.Add(new AttachmentViewModel { CreatedDate = new DateTime(2016, 09, 04, 14, 42, 6), Name = "SURGICAL RECORDS", AttachmentTypeName = "CLINICAL", CreatedBy = "HEMAVATHI", IncludeFax = "IncFax", DocumentFile = null });
                        AuthAttachmentsData.Add(new AttachmentViewModel { CreatedDate = new DateTime(2016, 08, 04, 14, 42, 6), Name = "SURGICAL RECORDS", AttachmentTypeName = "CLINICAL", CreatedBy = "HEMAVATHI", IncludeFax = "IncFax", DocumentFile = null });
                        AuthAttachmentsData.Add(new AttachmentViewModel { CreatedDate = new DateTime(2016, 08, 05, 14, 42, 6), Name = "SURGICAL RECORDS", AttachmentTypeName = "CLINICAL", CreatedBy = "HEMAVATHI", IncludeFax = "IncFax", DocumentFile = null });
                        return PartialView("~/Areas/UM/Views/ViewAuth/Attachments/_Attachments.cshtml", AuthAttachmentsData);
                    case "LettersTab":
                        List<LetterViewModel> LetterViewModel = new List<LetterViewModel>();
                        LetterServices LetterService = new LetterServices();
                        LetterViewModel = LetterService.GetAllLetters(AuthID);
                        return PartialView("~/Areas/UM/Views/ViewAuth/Letters/_Letters.cshtml", LetterViewModel);
                    case "ODAGTab":
                        return PartialView("~/Areas/UM/Views/ViewAuth/ODAG/_ODAG.cshtml");
                    case "StatusTab":
                        //AuthStatusandActivityViewModel both = new AuthStatusandActivityViewModel();
                        //both.Status.Add(new AuthStatusViewModel { AuthorizationCurrentStatusID = 1, Date = System.DateTime.Now, ExtensionNumber = 123456, UserName = "BARBARA JOY", Status = "NURSE REVIEW-2" });
                        //both.Status.Add(new AuthStatusViewModel { AuthorizationCurrentStatusID = 1, Date = System.DateTime.Now.AddDays(-1.2), ExtensionNumber = 123456, UserName = "BARBARA JOY", Status = "NURSE REVIEW-2" });
                        //both.Status.Add(new AuthStatusViewModel { AuthorizationCurrentStatusID = 1, Date = System.DateTime.Now.AddDays(-3.6), ExtensionNumber = 123456, UserName = "BARBARA JOY", Status = "NURSE REVIEW-2" });
                        //both.StatusActivity.Add(new AuthStatusActivityViewModel { Date = System.DateTime.Now, User = "MAHESH KUMAR", Module = "UM", Category = "AUTHORIZATION", Id = "1607250018", Screen = "INTAKE", Action = "PEND", Outcome = "DRAFT", ActivityLog = "07/09/2016 14:41:47" });
                        //both.StatusActivity.Add(new AuthStatusActivityViewModel { Date = System.DateTime.Now.AddDays(-1.2), User = "MAHESH KUMAR", Module = "UM", Category = "AUTHORIZATION", Id = "1607250018", Screen = "INTAKE", Action = "PEND", Outcome = "DRAFT", ActivityLog = "07/09/2016 14:41:47" });
                        //both.StatusActivity.Add(new AuthStatusActivityViewModel { Date = System.DateTime.Now.AddDays(-3.6), User = "MAHESH KUMAR", Module = "UM", Category = "AUTHORIZATION", Id = "1607250018", Screen = "INTAKE", Action = "PEND", Outcome = "DRAFT", ActivityLog = "07/09/2016 14:41:47" });

                        //return PartialView("~/Areas/UM/Views/ViewAuth/Status/_Status.cshtml", both);
                    default:
                        ViewAuthService services = new ViewAuthService();
                        ViewAuthModel = services.GetAuthByID(AuthID);
                        return PartialView("~/Areas/UM/Views/ViewAuth/Summary/_AuthSummary.cshtml", ViewAuthModel);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult GetAllContacts(int AuthID)
        {
            ContactServices contactservice = new ContactServices();
            List<AuthorizationContactViewModel> ContactViewModel = new List<AuthorizationContactViewModel>();
            ContactViewModel = contactservice.GetAllContactsServices(AuthID);
            return PartialView("~/Areas/UM/Views/Common/Contact/_ContactTable.cshtml", ContactViewModel);
        }
	}
}