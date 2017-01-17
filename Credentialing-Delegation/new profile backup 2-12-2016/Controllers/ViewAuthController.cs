using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Models.ViewAuth;
using PortalTemplate.Areas.UM.Models.ViewModels.ViewAuthorization;
using PortalTemplate.Areas.UM.Services;

namespace PortalTemplate.Controllers
{
    public class ViewAuthController : Controller
    {
        
       
        //
        // GET: /ViewAuth/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTabData(string TabId, int AuthID)
        {
            try
            {
                switch (TabId)
                {
                    case "ReviewTab":
                        return PartialView("~/Areas/UM/Views/ViewAuth/Review/_Review.cshtml");
                    case "NotesTab":
                        List<AuthNotesViewModel> AuthNotesData = new List<AuthNotesViewModel>();
                        AuthNotesData.Add(new AuthNotesViewModel { NoteID = 1, Date = new DateTime(2016, 09, 04, 14, 42, 6), NoteType = "Auth Note", UserName = "Lavanya Jami", Subject = "UM Note", Description = "There is no Explanation about Discussion There is no Explanation about Discussion", IncludeFax = true });
                        AuthNotesData.Add(new AuthNotesViewModel { NoteID = 2, Date = new DateTime(2016, 08, 30, 14, 31, 6), NoteType = "Auth Note", UserName = "Lavanya Jami", Subject = "UM Note", Description = "There is no Explanation about Discussion There is no Explanation about Discussion", IncludeFax = true });
                        AuthNotesData.Add(new AuthNotesViewModel { NoteID = 3, Date = new DateTime(2016, 08, 06, 16, 15, 51), NoteType = "Auth Note", UserName = "Lavanya Jami", Subject = "UM Note", Description = "There is no Explanation about Discussion There is no Explanation about Discussion", IncludeFax = true });
                        return PartialView("~/Areas/UM/Views/ViewAuth/Notes/_Notes.cshtml");
                    case "ContactsTab":
                        List<AuthContactsViewModel> AuthContactsData = new List<AuthContactsViewModel>();
                        AuthContactsData.Add(new AuthContactsViewModel { CreatedDate = new DateTime(2016, 09, 04, 14, 42, 6), ContactEntity = "Hospital", ContactName = "JACQUELINE", ContactType = "Portal", EMailFaxOther = "JACQUELINE@DOMAIN.COM", Direction = "OUTBOUND", OutcomeType = "Successful", Description = "There is no Explanation about Discussion There is no Explanation about Discussion", CreatedBy = "DEV2", IncludeFax = true });
                        AuthContactsData.Add(new AuthContactsViewModel { CreatedDate = new DateTime(2016, 08, 04, 14, 42, 6), ContactEntity = "Hospital", ContactName = "JACQUELINE", ContactType = "Portal", EMailFaxOther = "JACQUELINE@DOMAIN.COM", Direction = "OUTBOUND", OutcomeType = "Successful", Description = "There is no Explanation about Discussion There is no Explanation about Discussion", CreatedBy = "DEV2", IncludeFax = true });
                        AuthContactsData.Add(new AuthContactsViewModel { CreatedDate = new DateTime(2016, 07, 04, 14, 42, 6), ContactEntity = "Hospital", ContactName = "JACQUELINE", ContactType = "Portal", EMailFaxOther = "JACQUELINE@DOMAIN.COM", Direction = "OUTBOUND", OutcomeType = "Successful", Description = "There is no Explanation about Discussion There is no Explanation about Discussion", CreatedBy = "DEV2", IncludeFax = true });
                        return PartialView("~/Areas/UM/Views/ViewAuth/Contacts/_Contacts.cshtml");
                    case "AttachmentsTab":
                        List<AuthAttachmentsViewModel> AuthAttachmentsData = new List<AuthAttachmentsViewModel>();
                        AuthAttachmentsData.Add(new AuthAttachmentsViewModel { CreatedDate = new DateTime(2016, 09, 04, 14, 42, 6), Name = "SURGICAL RECORDS", DocumentType = "CLINICAL", CreatedBy = "HEMAVATHI", IncludeFax = true, DocumentFile = null });
                        AuthAttachmentsData.Add(new AuthAttachmentsViewModel { CreatedDate = new DateTime(2016, 08, 04, 14, 42, 6), Name = "SURGICAL RECORDS", DocumentType = "CLINICAL", CreatedBy = "HEMAVATHI", IncludeFax = true, DocumentFile = null });
                        AuthAttachmentsData.Add(new AuthAttachmentsViewModel { CreatedDate = new DateTime(2016, 08, 05, 14, 42, 6), Name = "SURGICAL RECORDS", DocumentType = "CLINICAL", CreatedBy = "HEMAVATHI", IncludeFax = true, DocumentFile = null });
                        return PartialView("~/Views/ViewAuth/_AuthAttachments.cshtml", AuthAttachmentsData);
                    case "LettersTab":
                        List<AuthLettersViewModel> AuthLettersData = new List<AuthLettersViewModel>();
                        AuthLettersData.Add(new AuthLettersViewModel { LetterID = 1, MailDate = new DateTime(2016, 09, 04, 14, 42, 6), LetterEntity = "MEMBER", Reason = "Approval Letter", SentBy = "BARBARA JOY", BatchNumber = 123456, LetterFile = null });
                        AuthLettersData.Add(new AuthLettersViewModel { LetterID = 2, MailDate = new DateTime(2016, 08, 04, 14, 42, 6), LetterEntity = "MEMBER", Reason = "Denial Letter", SentBy = "BARBARA JOY", BatchNumber = 123456, LetterFile = null });
                        AuthLettersData.Add(new AuthLettersViewModel { LetterID = 3, MailDate = new DateTime(2016, 07, 04, 14, 42, 6), LetterEntity = "MEMBER", Reason = "Nomnc Letter", SentBy = "BARBARA JOY", BatchNumber = 123456, LetterFile = null });
                        AuthLettersData.Add(new AuthLettersViewModel { LetterID = 4, MailDate = new DateTime(2016, 07, 04, 14, 42, 6), LetterEntity = "MEMBER", Reason = "Denc Letter", SentBy = "BARBARA JOY", BatchNumber = 123456, LetterFile = null });
                        return PartialView("~/Views/ViewAuth/_AuthLetters.cshtml", AuthLettersData);
                    case "ODAGTab":
                        return PartialView("~/Areas/UM/Views/ViewAuth/ODAG/_ODAG.cshtml");
                    case "StatusTab":

                        AuthStatusandActivityViewModel both = new AuthStatusandActivityViewModel();

                        both.Status.Add(new AuthStatusViewModel { AuthorizationCurrentStatusID = 1, Date = System.DateTime.Now, ExtensionNumber = 123456, UserName = "BARBARA JOY", Status = "NURSE REVIEW-2" });
                        both.Status.Add(new AuthStatusViewModel { AuthorizationCurrentStatusID = 1, Date = System.DateTime.Now.AddDays(-1.2), ExtensionNumber = 123456, UserName = "BARBARA JOY", Status = "NURSE REVIEW-2" });
                        both.Status.Add(new AuthStatusViewModel { AuthorizationCurrentStatusID = 1, Date = System.DateTime.Now.AddDays(-3.6), ExtensionNumber = 123456, UserName = "BARBARA JOY", Status = "NURSE REVIEW-2" });

                        both.StatusActivity.Add(new AuthStatusActivityViewModel { Date = System.DateTime.Now, User = "MAHESH KUMAR", Module = "UM", Category = "AUTHORIZATION", Id = "1607250018", Screen = "INTAKE", Action = "PEND", Outcome = "DRAFT", ActivityLog = "07/09/2016 14:41:47" });
                        both.StatusActivity.Add(new AuthStatusActivityViewModel { Date = System.DateTime.Now.AddDays(-1.2), User = "MAHESH KUMAR", Module = "UM", Category = "AUTHORIZATION", Id = "1607250018", Screen = "INTAKE", Action = "PEND", Outcome = "DRAFT", ActivityLog = "07/09/2016 14:41:47" });
                        both.StatusActivity.Add(new AuthStatusActivityViewModel { Date = System.DateTime.Now.AddDays(-3.6), User = "MAHESH KUMAR", Module = "UM", Category = "AUTHORIZATION", Id = "1607250018", Screen = "INTAKE", Action = "PEND", Outcome = "DRAFT", ActivityLog = "07/09/2016 14:41:47" });

                        return PartialView("~/Areas/UM/Views/ViewAuth/Status/_Status.cshtml", both);
                    default:
                         ViewAuthorizationViewModel ViewAuthModel = new ViewAuthorizationViewModel();
                         ViewAuthService service = new ViewAuthService();
                         ViewAuthModel = service.GetAuthByID(AuthID);
                         return PartialView("~/Areas/UM/Views/ViewAuth/Summary/_AuthSummary.cshtml", ViewAuthModel);
                }
            }
            catch(Exception ex) {

                throw ex; 
            }
        }
	}
}