using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Services
{
    public class EncounterCRUD : IEncounterCRUD
    {
        public EncounterViewModel ViewEncounter()
        {
            List<ProviderViewModel> ProviderList = new List<ProviderViewModel>{
            new ProviderViewModel{ ProviderFirstName="Parikshith", ProviderLastName="Singh", ProviderNPI="1417989625", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Nishat", ProviderLastName="Seema", ProviderNPI="1730249210", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Devabavus", ProviderLastName="Merceley", ProviderNPI="1164611349", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Deam", ProviderLastName="David", ProviderNPI="1518933456", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Masood", ProviderLastName="Asif", ProviderNPI="1194715656", Specialty="Internal Medicine", Taxonomy="	207R00000X"}
        };


            PlanViewModel coventry = new PlanViewModel { PlanID = 1, PlanName = "Coventry" };
            PlanViewModel freedom = new PlanViewModel { PlanID = 2, PlanName = "Freedom Health Ins." };
            PlanViewModel ultimate = new PlanViewModel { PlanID = 2, PlanName = "Ultimate Health Plans" };
            PlanViewModel optimum = new PlanViewModel { PlanID = 2, PlanName = "Optimum(CAP)" };
            PlanViewModel wellcare = new PlanViewModel { PlanID = 2, PlanName = "Wellcare Health Plans" };



            List<MemberViewModel> MemberList = new List<MemberViewModel>{
            new MemberViewModel{ SubscriberID="K277057937", MemberFirstName="Janet", MemberLastName="Satterfield", Gender="Female", DateOfBirth=new DateTime(1947,07,10), Plan=freedom, Address="10200, Yale Avenue"},
            new MemberViewModel{ SubscriberID="K277057938", MemberFirstName="LELAND", MemberLastName="Hansen Jr.", Gender="Male", DateOfBirth=new DateTime(1947,07,10), Plan=ultimate, Address="10494 Northcliffe Blvd"},
            new MemberViewModel{ SubscriberID="K277057939", MemberFirstName="John", MemberLastName="Satterfield Jr.", Gender="Male", DateOfBirth=new DateTime(1947,07,10), Plan=optimum, Address="1100 SW Saint Lucie West Blvd"},
            new MemberViewModel{ SubscriberID="K277057940", MemberFirstName="Eveline", MemberLastName="Hewett", Gender="Female", DateOfBirth=new DateTime(1947,07,10), Plan=wellcare, Address="11151 Spring Hill Drive"},
            new MemberViewModel{ SubscriberID="K277057941", MemberFirstName="Jill", MemberLastName="Crawley", Gender="Female", DateOfBirth=new DateTime(1947,07,10), Plan=coventry, Address="11323 CORTEZ BLVD"}
        };

            ServiceInfoViewModel serviceInfo = new ServiceInfoViewModel();
            serviceInfo.BillingProvider = ProviderList[1];
            serviceInfo.RenderingProvider = ProviderList[2];
            serviceInfo.ReferringProvider = ProviderList[0];
            serviceInfo.CheckInTime = "9:00 AM";
            serviceInfo.CheckOutTime = "10:00 AM";
            serviceInfo.DOSFrom = "12/12/2016";
            serviceInfo.DOSTo = "12/12/2016";
            serviceInfo.PlaceOfService = new PlaceOfServiceViewModel { PlaceOfServiceID = 21, Name = "Inpatient Hospital" };
            serviceInfo.ServiceFacility = new FacilityViewModel { FacilityID = "1", FacilityName = "1094 North Cliffe Dlve" };
            serviceInfo.ReasonForVisit = "MVA";
            serviceInfo.NextAppointmentDate = "12/20/2016";

            EncounterViewModel encounterDetails = new EncounterViewModel();
            encounterDetails.EncounterID = "99374823";
            encounterDetails.ProviderInfo = ProviderList[0];
            encounterDetails.MemberInfo = MemberList[0];
            encounterDetails.ServiceInfo = serviceInfo;
            encounterDetails.DateOfServiceFrom = new DateTime(2016, 10, 26, 9, 0, 0);
            encounterDetails.DateOfServiceTo = new DateTime(2016, 10, 27, 12, 0, 0);
            encounterDetails.DateOfCreation = "12/21/2016";
            encounterDetails.EncounterType = EncounterType.HMO;
            encounterDetails.EncounterStatus = EncounterStatus.ACTIVE;
            encounterDetails.EncounterNotes = "Progress Notes Missing";

            List<DocumentViewModel> Documents = new List<DocumentViewModel>
            {
                new DocumentViewModel{ DocumentID=1, DocumentName="Notes", Category="Progress Note", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=2, DocumentName="X-Ray", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=3, DocumentName="Sonogram", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"}
            };
            encounterDetails.Documents = Documents;
            return encounterDetails;
        }


        public EncounterViewModel EditEncounter()
        {
            List<ProviderViewModel> ProviderList = new List<ProviderViewModel>{
            new ProviderViewModel{ ProviderFirstName="Parikshith", ProviderLastName="Singh", ProviderNPI="1417989625", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Nishat", ProviderLastName="Seema", ProviderNPI="1730249210", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Devabavus", ProviderLastName="Merceley", ProviderNPI="1164611349", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Deam", ProviderLastName="David", ProviderNPI="1518933456", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new ProviderViewModel{ ProviderFirstName="Masood", ProviderLastName="Asif", ProviderNPI="1194715656", Specialty="Internal Medicine", Taxonomy="	207R00000X"}
        };


            PlanViewModel coventry = new PlanViewModel { PlanID = 1, PlanName = "Coventry" };
            PlanViewModel freedom = new PlanViewModel { PlanID = 2, PlanName = "Freedom Health Ins." };
            PlanViewModel ultimate = new PlanViewModel { PlanID = 2, PlanName = "Ultimate Health Plans" };
            PlanViewModel optimum = new PlanViewModel { PlanID = 2, PlanName = "Optimum(CAP)" };
            PlanViewModel wellcare = new PlanViewModel { PlanID = 2, PlanName = "Wellcare Health Plans" };



            List<MemberViewModel> MemberList = new List<MemberViewModel>{
            new MemberViewModel{ SubscriberID="K277057937", MemberFirstName="Janet", MemberLastName="Satterfield", Gender="Female", DateOfBirth=new DateTime(1947,07,10), Plan=freedom, Address="10200, Yale Avenue"},
            new MemberViewModel{ SubscriberID="K277057938", MemberFirstName="LELAND", MemberLastName="Hansen Jr.", Gender="Male", DateOfBirth=new DateTime(1947,07,10), Plan=ultimate, Address="10494 Northcliffe Blvd"},
            new MemberViewModel{ SubscriberID="K277057939", MemberFirstName="John", MemberLastName="Satterfield Jr.", Gender="Male", DateOfBirth=new DateTime(1947,07,10), Plan=optimum, Address="1100 SW Saint Lucie West Blvd"},
            new MemberViewModel{ SubscriberID="K277057940", MemberFirstName="Eveline", MemberLastName="Hewett", Gender="Female", DateOfBirth=new DateTime(1947,07,10), Plan=wellcare, Address="11151 Spring Hill Drive"},
            new MemberViewModel{ SubscriberID="K277057941", MemberFirstName="Jill", MemberLastName="Crawley", Gender="Female", DateOfBirth=new DateTime(1947,07,10), Plan=coventry, Address="11323 CORTEZ BLVD"}
        };

            ServiceInfoViewModel serviceInfo = new ServiceInfoViewModel();
            serviceInfo.BillingProvider = ProviderList[1];
            serviceInfo.RenderingProvider = ProviderList[2];
            serviceInfo.ReferringProvider = ProviderList[0];
            serviceInfo.CheckInTime = "9:00 AM";
            serviceInfo.CheckOutTime = "10:00 AM";
            serviceInfo.DOSFrom = "12/12/2016";
            serviceInfo.DOSTo = "12/12/2016";
            serviceInfo.PlaceOfService = new PlaceOfServiceViewModel { PlaceOfServiceID = 21, Name = "Inpatient Hospital" };
            serviceInfo.ServiceFacility = new FacilityViewModel { FacilityID = "1", FacilityName = "1094 North Cliffe Dlve" };
            serviceInfo.ReasonForVisit = "MVA";
            serviceInfo.NextAppointmentDate = "12/20/2016";

            EncounterViewModel encounterDetails = new EncounterViewModel();
            encounterDetails.EncounterID = "99374823";
            encounterDetails.ProviderInfo = ProviderList[0];
            encounterDetails.MemberInfo = MemberList[0];
            encounterDetails.ServiceInfo = serviceInfo;
            encounterDetails.DateOfServiceFrom = new DateTime(2016, 10, 26, 9, 0, 0);
            encounterDetails.DateOfServiceTo = new DateTime(2016, 10, 27, 12, 0, 0);
            encounterDetails.DateOfCreation = "12/21/2016";
            encounterDetails.EncounterType = EncounterType.HMO;
            encounterDetails.EncounterStatus = EncounterStatus.ACTIVE;
            encounterDetails.EncounterNotes = "Progress Notes Missing";
            encounterDetails.ServiceInfo.ProviderList = ProviderList;
            encounterDetails.ServiceInfo.FacilityList = new List<FacilityViewModel>
            {
                new FacilityViewModel{ FacilityID = "1", FacilityName="1094, North Cliffe Dlve", FacilityAddress="Yale Avenue"},
                new FacilityViewModel{ FacilityID = "2", FacilityName="10441, QUALITY DR", FacilityAddress="SW SAINT LUCIE WEST BLVD STE 209"},
                new FacilityViewModel{ FacilityID = "3", FacilityName="10494, INTERNAL MED", FacilityAddress="SHUMARD OAK DRIVE SUITE 101"}
            };

            List<DocumentViewModel> Documents = new List<DocumentViewModel>
            {
                new DocumentViewModel{ DocumentID=1, DocumentName="Notes", Category="Progress Note", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=2, DocumentName="X-Ray", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"},
                new DocumentViewModel{ DocumentID=3, DocumentName="Sonogram", Category="Lab Report", UploadedBy="Moore", UploadedOn="12/12/2016"}
            };
            encounterDetails.Documents = Documents;
            return encounterDetails;
        }
    }
}