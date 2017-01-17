using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.SharedView.Models.Encounter;
namespace PortalTemplate.Areas.SharedView.Controllers
{
    public class EncounterController : Controller
    {
        public ActionResult GetEncounterTemplate()
        {
            EncounterDetailsViewModel EncounterDetails = new EncounterDetailsViewModel();
            EncounterDetails.CheckInTime = "10:30 am";
            EncounterDetails.CheckOutTime = "11:00 am";
            EncounterDetails.DOSFrom = new DateTime(2016, 10, 09);
            EncounterDetails.DOSTo = new DateTime(2016, 10, 10);
            EncounterDetails.EncounterId = "116244951";
            //EncounterDetails.EncounterNotes = "PROGRESS NOTES NOT ATTACHED";
            EncounterDetails.EncounterStatus = "OPEN";
            EncounterDetails.EncounterType = "HMO";
            EncounterDetails.NextAppointmentDate = new DateTime(2016, 12, 12);
            EncounterDetails.PatientBirthDate = new DateTime(1985, 10, 10);
            EncounterDetails.PatientCity = "AMIDON";
            EncounterDetails.PatientFirstAddress = "";
            EncounterDetails.PatientFirstName = "SMITH";
            EncounterDetails.PatientGender = "MALE";
            EncounterDetails.PatientLastOrOrganizationName = "LUCAS";
            EncounterDetails.PatientMiddleName = "";
            EncounterDetails.PatientSecondAddress = "";
            EncounterDetails.PatientState = "FLORIDA";
            EncounterDetails.PatientZip = "100482";
            EncounterDetails.PlanName = "";
            EncounterDetails.PlaceOfService = "21-INPATIENT HOSPITAL";
            EncounterDetails.RenderingProviderFirstName = "PARIKSITH";
            EncounterDetails.RenderingProviderLastOrOrganizationName = "SINGH";
            EncounterDetails.RenderingProviderMiddleName = "";
            EncounterDetails.RenderingProviderNPI = "1417989625";
            EncounterDetails.RenderingProviderSpeciality = "INTERNAL MEDICINE";
            EncounterDetails.RenderingProviderTaxonomy = "207R00000X";
            EncounterDetails.SubscriberID = "K277057937";
            EncounterDetails.VisitLength = "30 MINUTES";
            EncounterDetails.VisitReason = "MVA";

            return PartialView("~/Areas/SharedView/Views/Encounter/_EncounterTemplate.cshtml", EncounterDetails);
        }

        public ActionResult GetDocumentsView()
        {
            DocumentListViewModel documentList = new DocumentListViewModel();
            documentList.DocumentHistory = new List<DocumentViewModel>
            {
                new DocumentViewModel{ DocumentID=1, DocumentName="Notes", Category="Progress Note", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=2, DocumentName="X-Ray", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=3, DocumentName="Sonogram", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"}
            };

            documentList.UploadedDocuments = new List<DocumentViewModel>
            {
                new DocumentViewModel{ DocumentID=4, DocumentName="ECG Report", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"}
            };
            return PartialView("~/Areas/SharedView/Views/Documents/_DocumentsView.cshtml", documentList);
        }
	}
}