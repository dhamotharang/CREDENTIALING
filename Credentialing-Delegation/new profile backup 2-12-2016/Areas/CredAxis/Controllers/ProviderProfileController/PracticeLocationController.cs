using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.ProviderProfileController
{
    public class PracticeLocationController : Controller
    {
        //
        // GET: /CredAxis/PracticeLocation/
        public ActionResult Index()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/_PracticeLocationIndex.cshtml");
        }

        #region Billing Contact 

        public ActionResult GetBillingContactView()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/BillingContact/_ViewBillingContact.cshtml");
        }
      
        public ActionResult EditOfficeStaff()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OfficeStaffContact/_AddEditOfficeStaffContact.cshtml");
        }
        public ActionResult ViewOfficeStaff()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OfficeStaffContact/_ViewOfficeStaffContact.cshtml");
        }
        public ActionResult AddOfficeStaff()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OfficeStaffContact/_AddOfficeContact.cshtml");
        }
        public ActionResult GetBillingContactAddView()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/BillingContact/_BillingContactAddView.cshtml");
        }
        public ActionResult GetBillingContactEditView()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/BillingContact/_BillingContactEditView.cshtml");
        }

        #endregion Billing Contact

        #region Other Information
        public ActionResult EditOtherInformation()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OtherInformation/_AddEditOtherInformation.cshtml");
        }
        public ActionResult ViewOtherInformation()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OtherInformation/_ViewOtherInformation.cshtml");
        }
        #endregion


        #region Payment and Remittance
        public ActionResult GetPaymentRemittanceAddEditView(string type)
        {
            if(type == "Add")
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/PaymentRemittance/_AddPaymentRemittance.cshtml");
            }
            else
            {
                return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/PaymentRemittance/_EditPaymentRemittance.cshtml");
            }
        }

        public ActionResult GetPaymentRemittanceView()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/PaymentRemittance/_ViewPaymentRemittance.cshtml");
        }
        #endregion

        #region worker Information

        public  ActionResult EditworkerInformation()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/WorkersCompensation/_AddEditWorkerCompensation.cshtml");

        }
        public ActionResult ViewworkerInformation()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/WorkersCompensation/_ViewWorkersCompensation.cshtml");

        }

        #endregion


        public ActionResult getAddEditOpenPracticeStatus()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OpenPracticeStatus/_AddEditOpenPracticStatus.cshtml");
        }
       
        public ActionResult getViewOpenPracticeStatus()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OpenPracticeStatus/_ViewOpenPracticeStatus.cshtml");
        }

        #region Office Hours
        public ActionResult ViewOfficeHoursPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OfficeHours/_ViewOfficeHours.cshtml");

        }
        public ActionResult AddEditOfficeHoursPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/OfficeHours/_AddEditOfficeHours.cshtml");

        }
        #endregion


        public ActionResult GetViewCredentialingContact()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/CredentialingContact/_ViewCredentialingContact.cshtml");
        }
        public ActionResult GetAddEditCredentialingContact()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/CredentialingContact/_AddEditCredentialingContact.cshtml");
        }


        #region Partner Information

        public ActionResult ViewPartnerInformation()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/PartnersColleagues/_ViewPartnersColleagues.cshtml");

        }
        public ActionResult EditPartnerInformation()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/PartnersColleagues/_EditPartnerColleague.cshtml");

        }

        #endregion


        public ActionResult GetViewSupervisingPractitioner()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/SupervisingPractitioner/_ViewSupervisingPractitioner.cshtml");
        }
        public ActionResult GetAddEditSupervisingPractitioner()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/PracticeLocation/SupervisingPractitioner/_AddEditSupervisingPratitioner.cshtml");
        }

	}
}