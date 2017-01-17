using Newtonsoft.Json;
using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Models.Schedule;
using PortalTemplate.Areas.Encounters.Models.ScheduleCRUD;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Services
{
    public class Schedule : ISchedule
    {
        public EncounterViewModel CreateEncounterDetails()
        {

            PlanViewModel coventry = new PlanViewModel { PlanID = 1, PlanName = "Coventry" };
            PlanViewModel freedom = new PlanViewModel { PlanID = 2, PlanName = "Freedom Health Ins." };
            PlanViewModel ultimate = new PlanViewModel { PlanID = 2, PlanName = "Ultimate Health Plans" };
            PlanViewModel optimum = new PlanViewModel { PlanID = 2, PlanName = "Optimum(CAP)" };
            PlanViewModel wellcare = new PlanViewModel { PlanID = 2, PlanName = "Wellcare Health Plans" };

            EncounterViewModel encounterDetails = null;
            List<PortalTemplate.Areas.Encounters.Models.ProviderViewModel> ProviderList = new List<PortalTemplate.Areas.Encounters.Models.ProviderViewModel>{
            new PortalTemplate.Areas.Encounters.Models.ProviderViewModel{ ProviderFirstName="Parikshith", ProviderLastName="Singh", ProviderNPI="1417989625", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new PortalTemplate.Areas.Encounters.Models.ProviderViewModel{ ProviderFirstName="Nishat", ProviderLastName="Seema", ProviderNPI="1730249210", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new PortalTemplate.Areas.Encounters.Models.ProviderViewModel{ ProviderFirstName="Devabavus", ProviderLastName="Merceley", ProviderNPI="1164611349", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new PortalTemplate.Areas.Encounters.Models.ProviderViewModel{ ProviderFirstName="Deam", ProviderLastName="David", ProviderNPI="1518933456", Specialty="Internal Medicine", Taxonomy="	207R00000X"},
            new PortalTemplate.Areas.Encounters.Models.ProviderViewModel{ ProviderFirstName="Masood", ProviderLastName="Asif", ProviderNPI="1194715656", Specialty="Internal Medicine", Taxonomy="	207R00000X"}
        };
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

            encounterDetails = new EncounterViewModel();
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


        public ViewScheduleViewModel GetScheduleView()
        {
            ViewScheduleViewModel ScheduleDetails = new ViewScheduleViewModel();
            ScheduleDetails.SubscriberID = "K277057937";
            ScheduleDetails.MemberFirstName = "Janet";
            ScheduleDetails.MemberLastName = "Satterfield";
            ScheduleDetails.Gender = "Female";
            ScheduleDetails.DateOfBirth = new DateTime(1999, 11, 11);
            ScheduleDetails.Address = "10200, Yale Avenue";
            ScheduleDetails.PlanName = "Freedom Health Ins.";
            ScheduleDetails.RenderingProviderName = "Parikshith Singh";
            ScheduleDetails.ReferringProviderName = "Parikshith Singh";
            ScheduleDetails.BillingProviderName = "Parikshith Singh";
            ScheduleDetails.ServiceFacilityName = "1094 North Cliffe Dlve";
            ScheduleDetails.ScheduleDate = new DateTime(2016, 11, 19);
            ScheduleDetails.ScheduleTime = "10:00";
            ScheduleDetails.VisitCategory = "Injection";
            ScheduleDetails.VisitLength = "15";
            ScheduleDetails.VisitReason = "MVA";
            return ScheduleDetails;
        }


        public EditScheduleViewModel GetScheduleEdit()
        {
            EditScheduleViewModel ScheduleDetails = new EditScheduleViewModel();
            ScheduleDetails.SubscriberID = "K277057937";
            ScheduleDetails.MemberFirstName = "Janet";
            ScheduleDetails.MemberLastName = "Satterfield";
            ScheduleDetails.Gender = "Female";
            ScheduleDetails.DateOfBirth = new DateTime(1999, 11, 11);
            ScheduleDetails.Address = "10200, Yale Avenue";
            ScheduleDetails.PlanName = "Freedom Health Ins.";
            ScheduleDetails.RenderingProviderName = "Parikshith Singh";
            ScheduleDetails.ReferringProviderName = "Parikshith Singh";
            ScheduleDetails.BillingProviderName = "Parikshith Singh";
            ScheduleDetails.ServiceFacilityName = "1094 North Cliffe Dlve";
            ScheduleDetails.ScheduleDate = new DateTime(2016, 11, 19);
            ScheduleDetails.ScheduleTime = "10:00";
            ScheduleDetails.VisitCategory = "Injection";
            ScheduleDetails.VisitLength = "15";
            ScheduleDetails.VisitReason = "MVA";
            return ScheduleDetails;
        }

        public List<ProviderViewModel> GetProviderResultList(string ProviderSearchParameter)
        {
            return JsonConvert.DeserializeObject<List<ProviderViewModel>>(Task.Run(async () =>
            {
                return await ExternalService.GetDataFromServiceAsync("ProviderServiceWebAPIURL", "api/ProviderService/GetAllProviders");
            }).Result);
        }

        public List<MemberViewModel> GetMemberResultList()
        {
            List<MemberViewModel> MemberResultList = new List<MemberViewModel>();
            MemberResultList.Add(new MemberViewModel { MemberId = 1, ProviderId = 1, SubscriberID = "UL0000441", MemberLastName = "KJELL", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1935, 04, 26), Address = "6046 NEWMARK ST", PCP = "MERCELY DEVABAVUS", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 2, ProviderId = 2, SubscriberID = "UL0000442", MemberLastName = "MARY", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1933, 02, 18), Address = "6046 NEWMARK ST", PCP = "MERCELY DEVABAVUS", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 3, ProviderId = 3, SubscriberID = "UL0003909", MemberLastName = "ROY", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1942, 01, 23), Address = "3106 SAW MILL LN", PCP = "VENKATREDDY ALUGUBELLI", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 4, ProviderId = 4, SubscriberID = "UL0003910", MemberLastName = "BARBARA", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1941, 01, 21), Address = "3106 SAW MILL LN", PCP = "VENKATREDDY ALUGUBELLI", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 5, ProviderId = 5, SubscriberID = "P00090227", MemberLastName = "LOUISA", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1944, 12, 14), Address = "524 INDEPENDENCE HWY", PCP = "SYED ZAIDI", PayerName = "FREEDOM HEALTH INS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 6, ProviderId = 6, SubscriberID = "T00017794", MemberLastName = "ANNIE", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1943, 04, 12), Address = "3028 N AVENIDA REPUBLICA DE CUBA APT 310", PCP = "MIGUEL FANA", PayerName = "OPTIMUM CAP", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 7, ProviderId = 7, SubscriberID = "T00021788", MemberLastName = "KATHRYN", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1949, 05, 04), Address = "2227 EVANGELINA AVE", PCP = "MARIA SCUNZIANO SINGH", PayerName = "OPTIMUM CAP", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 8, ProviderId = 8, SubscriberID = "667374", MemberLastName = "SHIRLEY", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1955, 06, 13), Address = "4785 38TH CIR APT 108 ", PCP = "NICHOLAS  COPPOLA", PayerName = "WELLCARE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 9, ProviderId = 9, SubscriberID = "99407342800", MemberLastName = "MONROE", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1942, 11, 19), Address = "9838 LAKE DR", PCP = "POONAM MALHOTRA", PayerName = "HUMANA", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 10, ProviderId = 10, SubscriberID = "80331907201", MemberLastName = "MARIA", MemberMiddleName = "", MemberFirstName = "ABAD", DateOfBirth = new DateTime(1997, 04, 03), Address = "16500 COLLINS AVE APT 2452", PCP = "GARY MERLINO", PayerName = "COVENTRY", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });

            return MemberResultList;
        }


        public ProviderSelectMemberViewModel GerProviderSelectMembers(string ProviderId)
        {
            var ProviderResultList = GetProviderResultList("");


            var MemberResultList = GetMemberResultList();

            var Res = ProviderId.Split(',').ToList();
            ProviderSelectMemberViewModel ProviderMemberResult = new ProviderSelectMemberViewModel();

            List<ProviderViewModel> SelectedProvidersList = new List<ProviderViewModel>();

            foreach (var item in ProviderResultList)
            {
                for (int i = 0; i < Res.Count; i++)
                {
                    if (item.ProviderNPI.ToString() == Res[i])
                    {
                        SelectedProvidersList.Add(item);
                        break;
                    }

                }
            }
            ProviderMemberResult.Provider = SelectedProvidersList;
            ProviderMemberResult.Member = MemberResultList;
            return ProviderMemberResult;
        }
    }
}