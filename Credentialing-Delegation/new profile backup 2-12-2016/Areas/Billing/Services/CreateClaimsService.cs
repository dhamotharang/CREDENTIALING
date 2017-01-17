using AutoMapper;
using PortalTemplate.Areas.Billing.Models.CMS1500.New;
using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Models.CreateClaim.Claim_Info;
using PortalTemplate.Areas.Billing.Models.CreateClaim.CreateClaimTemplate;
using PortalTemplate.Areas.Billing.Services.IServices;
using PortalTemplate.Areas.Billing.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Billing.Services
{
    public class CreateClaimsService : ICreateClaimsService
    {
        readonly List<MemberResultViewModel> MemberResultList;
        readonly List<ProviderResultViewModel> ProviderResultList;
        readonly List<ProviderResult> Providers;
        readonly MemberInfoViewModels MemberInformation;
        readonly HttpContext context;
        public CreateClaimsService()
        {
            context = HttpContext.Current;
            MemberResultList = new List<MemberResultViewModel>();
            ProviderResultList = new List<ProviderResultViewModel>();
            Providers = new List<ProviderResult>();
            MemberInformation = new MemberInfoViewModels();



        }
        public ClaimInfo GetClaimInfo(MemberRelatedId ids)
        {
            MemberInfoViewModels MemberInfo = new MemberInfoViewModels();

            //--------------temp code----------------------------
            foreach (var item in MemberResultList)
            {
                if (item.MemberUniqueId == ids.MemberId)
                {
                    MemberInfo.PatientFirstName = item.FirstName;
                    //Mapper.CreateMap<MemberResultViewModel, MemberInfoViewModels>();
                    MemberInfo = Mapper.Map<MemberResultViewModel, MemberInfoViewModels>(item);
                }
            }
            MemberInfo.PayerName = "FREEDOM HEALTH INS";
            MemberInfo.PayerFirstAddress = "P.O. BOX 151348";
            MemberInfo.PayerSecondAddress = "SPRING HILL";
            MemberInfo.PayerState = "AA";
            MemberInfo.PayerCity = "TEMPA";
            MemberInfo.PayerID = "41212";
            MemberInfo.PayerZip = "336840401";

            MemberInfo.BillingGroupNumber = "1851689947";
            MemberInfo.BillingProviderLastOrOrganizationName = "ACCESS 2 HEALTHCARE";
            MemberInfo.BillingProviderPhoneNo = "587977444";
            MemberInfo.BillingProviderZip = "346098102";
            MemberInfo.BillingProviderTaxonomy = "207RG0300X";
            MemberInfo.BillingProviderCity = "SPRING HILL";
            MemberInfo.BillingProviderState = "FL";
            MemberInfo.BillingProviderFirstAddress = "14690 SPRING HILL DR";
            MemberInfo.ReferringProviderLastName = "BENSON";
            MemberInfo.RenderingProviderFirstName = "DALTON";
            MemberInfo.RenderingProviderTaxonomy = "207RG0300X";
            MemberInfo.ReferringProviderIdentifier = "1164477618";

            MemberInfo.FacilityName = "ACCESS 2 HEALTHCARE";
            MemberInfo.FacilityAddress1 = "1903 W HIGHWAY 44";
            MemberInfo.FacilityCity = "INVERNESS";
            MemberInfo.FacilityState = "FL";
            MemberInfo.FacilityZip = "344533801";
            MemberInfo.FacilityIdentifier1 = "1851689947";
            MemberInfo.FacilityIdentifier2 = "454545544";

            MemberInfo.PatientAccountNumber = "1851689947";
            MemberInfo.SubscriberFirstAddress = "1230 SILVERTHORN LOOP";
            MemberInfo.SubscriberCity = "HERNANDO";
            MemberInfo.SubscriberState = "FL";
            MemberInfo.SubscriberZip = "34442";
            MemberInfo.SubscriberPhoneNo = "3524650375";

            ClaimInfo claimInfo = new ClaimInfo();
            MemberInfo memberInfo = new MemberInfo();
            ProviderInfo providerInfo = new ProviderInfo();
            PayerInfo payerInfo = new PayerInfo();
            BillingProviderInfo billingProviderInfo = new BillingProviderInfo();
            FacilityInfo facilityInfo = new FacilityInfo();

            Mapper.Initialize(config => { config.CreateMap<MemberInfoViewModels, MemberInfo>(); });

            //Mapper.CreateMap<MemberInfoViewModels, MemberInfo>();
            //Mapper.CreateMap<MemberInfoViewModels, ProviderInfo>();
            //Mapper.CreateMap<MemberInfoViewModels, PayerInfo>();
            //Mapper.CreateMap<MemberInfoViewModels, BillingProviderInfo>();
            //Mapper.CreateMap<MemberInfoViewModels, FacilityInfo>();

            memberInfo = Mapper.Map<MemberInfoViewModels, MemberInfo>(MemberInfo);
            Mapper.Initialize(config => { config.CreateMap<MemberInfoViewModels, ProviderInfo>(); });
            providerInfo = Mapper.Map<MemberInfoViewModels, ProviderInfo>(MemberInfo);
            providerInfo.RenderingProviderFirstAddress = "P.O. BOX 151348";
            providerInfo.RenderingProviderSecondAddress = "SPRING HILL";
            providerInfo.RenderingProviderCity = "TAMPA";
            providerInfo.RenderingProviderState = "FL";
            providerInfo.RenderingProviderZip = "336840401";


            Mapper.Initialize(config => { config.CreateMap<MemberInfoViewModels, PayerInfo>(); });
            payerInfo = Mapper.Map<MemberInfoViewModels, PayerInfo>(MemberInfo);

            Mapper.Initialize(config => { config.CreateMap<MemberInfoViewModels, BillingProviderInfo>(); });
            billingProviderInfo = Mapper.Map<MemberInfoViewModels, BillingProviderInfo>(MemberInfo);

            Mapper.Initialize(config => { config.CreateMap<MemberInfoViewModels, FacilityInfo>(); });
            facilityInfo = Mapper.Map<MemberInfoViewModels, FacilityInfo>(MemberInfo);

            claimInfo.MemberInfo = memberInfo;
            claimInfo.ProviderInfo = providerInfo;
            claimInfo.PayerInfo = payerInfo;
            claimInfo.BillingProviderInfo = billingProviderInfo;
            claimInfo.FacilityInfo = facilityInfo;
            context.Session["MemberInfo"] = MemberInfo;
            List<ServiceLineViewModels> serviceLines = new List<ServiceLineViewModels>();
            serviceLines.Add(new ServiceLineViewModels { Charges = 324 });
            claimInfo.ClaimsInfo = new ClaimsInfoViewModels();
            claimInfo.ClaimsInfo.ServiceLines = serviceLines;
            return claimInfo;
        }

        public List<MemberResultViewModel> GetMemberResult(string SubscriberID, string MemberName)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<MemberResultViewModel>>("api/MemberService/SearchMemberByFactors", null, "MemberServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Models.CreateClaim.MemberResultViewModel GetSelectedMemberResult(string MemberId)
        {
            MemberResultViewModel MemberResult = new MemberResultViewModel();
            foreach (var item in MemberResultList)
            {
                if (item.MemberUniqueId == MemberId)
                {
                    MemberResult = item;
                }
            }
            return MemberResult;
        }

        public List<Models.CreateClaim.ProviderResultViewModel> GetProviderResult(string ProviderName, string ProviderNPI)
        {

            try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<List<Models.CreateClaim.ProviderResultViewModel>>("api/ProviderService/SearchProvider?npiNumber=" + ProviderNPI + "&name=" + ProviderName + "&limit=50", "ProviderServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<MemberResultViewModel> GetSelectedProviderResult(List<string> ProviderId)
        {
            //var Res = ProviderId.Split(',').ToList();
            //List<ProviderResultViewModel> ProviderResult = new List<ProviderResultViewModel>();
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<MemberResultViewModel>>("api/MemberService/SearchMemberListByProviderIDs?ProvUniqueIDs=" + ProviderId + "", null, "MemberServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Models.CreateClaim.MemberResultViewModel> GetMemberListForProviderResult()
        {
            return MemberResultList;
        }

        public Models.CreateClaim.MemberResultViewModel GetSelectedMemberForProviderResult(string MemberId)
        {
            MemberResultViewModel MemberResult = new MemberResultViewModel();
            foreach (var item in MemberResultList)
            {
                if (item.MemberUniqueId == MemberId)
                {
                    MemberResult = item;
                }
            }
            return MemberResult;
        }

        public BillingInfo GetCms1500Form(Models.CreateClaim.ClaimsInfoViewModels ClaimsInfo)
        {
            BillingInfo billingInfo = new BillingInfo();

            CodedEncounter codedEncounter = new CodedEncounter();

            List<int> diagnosisPointers = new List<int>();
            diagnosisPointers.Add(0);
            diagnosisPointers.Add(1);
            diagnosisPointers.Add(2);
            diagnosisPointers.Add(3);

            List<Procedure> Procedures = new List<Procedure>();
            Procedures.Add(new Procedure { DiagnosisPointers = diagnosisPointers });



            List<Diagnosis> diagnosis = new List<Diagnosis>();
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });

            codedEncounter.Procedures = Procedures;
            codedEncounter.Diagnosis = diagnosis;
            billingInfo.CodedEncounter = codedEncounter;

            return billingInfo;

        }

        public Models.CreateClaim.CreateClaimTemplate.CreateClaimsTemplate GetCreateClaimTemplate(string MemberId)
        {
            CreateClaimsTemplate createClaimsTemplate = new CreateClaimsTemplate();
            ProviderResultViewModel ProviderResult = new ProviderResultViewModel();
            MemberResultViewModel MemberResult = new MemberResultViewModel();
            foreach (var item in MemberResultList)
            {
                if (item.MemberUniqueId == MemberId)
                {
                    MemberResult = item;
                    break;
                }
            }

            //foreach (var item in ProviderResultList)
            //{
            //    if (item.ProviderUniqueId == MemberResult.MemberUniqueId)
            //    {
            //        ProviderResult = item;
            //        break;
            //    }
            //}


            BillingProviderInfo BillingProvider = new BillingProviderInfo();

            BillingProvider.BillingProviderLastOrOrganizationName = "ACCESS 2 HEALTHCARE";
            BillingProvider.BillingProviderPhoneNo = "587977444";
            BillingProvider.BillingProviderZip = "346098102";
            BillingProvider.BillingProviderTaxonomy = "207RG0300X";
            BillingProvider.BillingProviderCity = "SPRING HILL";
            BillingProvider.BillingProviderState = "FL";
            BillingProvider.BillingProviderFirstAddress = "14690 SPRING HILL DR";

            ReferingProviderInfo ReferingProvider = new ReferingProviderInfo();


            ReferingProvider.ReferringProviderLastName = "BENSON";
            ReferingProvider.ReferringProviderIdentifier = "1164477618";
            ReferingProvider.ReferringProviderFirstAddress = "1903 W HIGHWAY 44";
            ReferingProvider.ReferringProviderSecondAddress = "";

            SupervisingProviderInfo SupervisingProvider = new SupervisingProviderInfo();
            SupervisingProvider.SupervisingProviderFirstAddress = "LORENE";
            SupervisingProvider.SupervisingProviderMiddleName = "";
            SupervisingProvider.SupervisingProviderLastName = "JHONY";
            SupervisingProvider.SupervisingProviderFirstAddress = "1903 W HIGHWAY 44";
            SupervisingProvider.SupervisingProviderSecondAddress = "";

            FacilityInfo Facility = new FacilityInfo();

            Facility.FacilityName = "ACCESS 2 HEALTHCARE";
            Facility.FacilityAddress1 = "1903 W HIGHWAY 44";
            Facility.FacilityCity = "INVERNESS";
            Facility.FacilityState = "FL";
            Facility.FacilityZip = "344533801";
            Facility.FacilityIdentifier1 = "1851689947";
            Facility.FacilityIdentifier2 = "454545544";

            createClaimsTemplate.RenderingProvider = ProviderResult;
            createClaimsTemplate.Member = MemberResult;
            createClaimsTemplate.BillingProvider = BillingProvider;
            createClaimsTemplate.ReferringProvider = ReferingProvider;
            createClaimsTemplate.Facility = Facility;
            createClaimsTemplate.SupervisingProvider = SupervisingProvider;

            return createClaimsTemplate;
        }

        public Models.CreateClaim.CreateClaimTemplate.CreateClaimsTemplate GetCreateClaimTemplateForMember(string MemberId)
        {
            CreateClaimsTemplate createClaimsTemplate = new CreateClaimsTemplate();
            try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<CreateClaimsTemplate>("api/MemberService/GetMemberSerachResultBySubscriberID?SubScriberID=" + MemberId + "", "MemberServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }

     


        }

        public List<ProviderResult> GetBillingProviderResult(ProviderResult SearchResult)
        {

            return Task.Run(() => ServiceRepository.PostDataToService<List<ProviderResult>>("api/ProviderService/GetBillingProviders", SearchResult, "ProviderServiceWebAPIURL")).Result;

        }


        public List<ProviderResult> GetRenderingProviderResult(ProviderResult SearchResult)
        {
            return Task.Run(() => ServiceRepository.PostDataToService<List<ProviderResult>>("api/ProviderService/GetRenderingProviders", SearchResult, "ProviderServiceWebAPIURL")).Result;
        }

        public List<ProviderResult> GetSupervisingProviderResult(ProviderResult SearchResult)
        {
            return Task.Run(() => ServiceRepository.PostDataToService<List<ProviderResult>>("api/ProviderService/GetSupervisingProviders", SearchResult, "ProviderServiceWebAPIURL")).Result;
        }

        public List<ProviderResult> GetReferringProviderResult(ProviderResult SearchResult)
        {
            return Task.Run(() => ServiceRepository.PostDataToService<List<ProviderResult>>("api/ProviderService/GetReferringProviders", SearchResult, "ProviderServiceWebAPIURL")).Result;
        }

        public List<Models.CreateClaim.CreateClaimTemplate.FacilityResult> GetFacilityResult()
        {
            List<FacilityResult> facilities = new List<FacilityResult>();
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "10200", FacilityName = "10200 Yale Avenue", FacilityAddress1 = " YALE AVENUE ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "1100", FacilityName = "1100 SW Saint Lucie West Blvd", FacilityAddress1 = " SW SAINT LUCIE WEST BLVD STE 209 ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "11151", FacilityName = "11151 Spring Hill Drive", FacilityAddress1 = " SPRING HILL DRIVE  ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "11373", FacilityName = "11373 Cortez Blvd Ste 300", FacilityAddress1 = " CORTEZ BLVD SUITE 201 ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "120", FacilityName = "120 MEDICAL BLVD STE 107", FacilityAddress1 = " MEDICAL BLVD STE 107 ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "14540", FacilityName = "14540 CORTEZ BLVD STE 113", FacilityAddress1 = " CORTEZ BLVD STE 113 ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "5344", FacilityName = "5344 Spring Hill Drive", FacilityAddress1 = " SPRING HILL DRIVE ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "5362", FacilityName = "5362 Spring Hill Drive", FacilityAddress1 = " SPRING HILL DRIVE  ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "1111", FacilityName = "7th Avenue North", FacilityAddress1 = " 7TH AVENUE NORTH STE 107 ", FacilityAddress2 = "" });
            facilities.Add(new FacilityResult { Id = 1, FacilityId = "19409", FacilityName = "Access 2 Health Care Physicians", FacilityAddress1 = " SHUMARD OAK DRIVE SUITE 101 ", FacilityAddress2 = "" });
            return facilities;
        }

        public List<Models.CreateClaim.Claim_Info.CodeHistory> GetIcdHistory()
        {
            List<CodeHistory> CodeHistoryList = new List<CodeHistory>();
            CodeHistoryList.Add(new CodeHistory { DiagnosisCode = "A207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23", "2" }, HCCType = new string[] { "Medical", "Rx" }, HCCVersion = new string[] { "v22", "v05" }, HCCDescription = new string[] { "Septicemia/Shock", "Muliple Sclerosis" }, HCCWeight = new string[] { "0.2", "0.3" } });
            CodeHistoryList.Add(new CodeHistory { DiagnosisCode = "B207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" } });
            CodeHistoryList.Add(new CodeHistory { DiagnosisCode = "C207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" } });
            CodeHistoryList.Add(new CodeHistory { DiagnosisCode = "D207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" } });
            CodeHistoryList.Add(new CodeHistory { DiagnosisCode = "E207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" } });
            return CodeHistoryList;
        }

        public List<Models.CreateClaim.Claim_Info.MedicalReview> GetMedicalReview()
        {
            List<MedicalReview> MedicalReviewList = new List<MedicalReview>();
            MedicalReviewList.Add(new MedicalReview { DiagnosisCode = "A207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23", "2" }, HCCType = new string[] { "Medical", "Rx" }, HCCVersion = new string[] { "v22", "v05" }, HCCDescription = new string[] { "Septicemia/Shock", "Muliple Sclerosis" }, HCCWeight = new string[] { "0.2", "0.3" } });
            MedicalReviewList.Add(new MedicalReview { DiagnosisCode = "B207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" }, BillingStatus = "success" });
            MedicalReviewList.Add(new MedicalReview { DiagnosisCode = "C207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" } });
            MedicalReviewList.Add(new MedicalReview { DiagnosisCode = "D207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" } });
            MedicalReviewList.Add(new MedicalReview { DiagnosisCode = "E207", DiagnosisDescription = "Septicemic Plague", HCCCode = new string[] { "23" }, HCCType = new string[] { "Medical" }, HCCVersion = new string[] { "v22" }, HCCDescription = new string[] { "Septicemia/Shock" }, HCCWeight = new string[] { "0.2" } });
            return MedicalReviewList;
        }


        public bool SaveCMS1500Form(BillingInfo BillingInfo)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<bool>("api/ClaimCreation/CreateClaim", BillingInfo)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<State> GetAllStates(bool IncludedInactive)
        {
            try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<List<State>>("/api/Common/GetAllStates?IncludedInactive=" + IncludedInactive, "CMSServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public BillingInfo MapMemberProviderToBillingInfo(CreateClaimsTemplate createClaimsTemplate)
        {
            BillingInfo BillingInfo = new BillingInfo();

            List<int> diagnosisPointers = new List<int>();
            diagnosisPointers.Add(0);
            diagnosisPointers.Add(1);
            diagnosisPointers.Add(2);
            diagnosisPointers.Add(3);

            List<Procedure> Procedures = new List<Procedure>();
            Procedures.Add(new Procedure { DiagnosisPointers = diagnosisPointers });

            List<Diagnosis> diagnosis = new List<Diagnosis>();
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });
            diagnosis.Add(new Diagnosis { });


            CodedEncounter CodedEncounter = new CodedEncounter();
            Encounter Encounter = new Encounter();
            EncounteredSchedule EncounteredSchedule = new EncounteredSchedule();
            Schedule Schedule = new Schedule();
            Member Member = new Member();
            ContactInfo ContactInfo = new ContactInfo();
            Address Address = new Address();
            List<Subscriber> Subscribers = new List<Subscriber>();
            Subscriber Subscriber = new Subscriber();
            Plan Plan = new Plan();
            Schedule.Facility = new Models.CMS1500.New.Facility { Address = new Address() };
            Schedule.RefferingProvider = new Provider { ContactInfo = new ContactInfo { Address = new Address() } };
            Schedule.BillingProvider = new Provider { ContactInfo = new ContactInfo { Address = new Address() } };
            Schedule.RenderingProvider = new Provider { ContactInfo = new ContactInfo { Address = new Address() } };
            ContactInfo.Address = Address;
            Subscriber.ContactInfo = ContactInfo;
            Subscriber.Plan = Plan;
            Subscribers.Add(Subscriber);
            Member.ContactInfo = ContactInfo;
            Member.Subscriber = Subscribers;
            Schedule.Member = Member;
            EncounteredSchedule.Schedule = Schedule;
            Encounter.EncounteredSchedule.Add(EncounteredSchedule);
            CodedEncounter.Encounter = Encounter;
            CodedEncounter.Procedures = Procedures;
            CodedEncounter.Diagnosis = diagnosis;
            BillingInfo.CodedEncounter = CodedEncounter;
            //--------------------------------------member data mapping-------------------------
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.FirstName = createClaimsTemplate.Member.FirstName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.MiddleName = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.LastName = createClaimsTemplate.Member.LastName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.DateOfBirth = createClaimsTemplate.Member.DateOfBirth;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.Gender = createClaimsTemplate.Member.Gender;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.PatientRelationShipCode = createClaimsTemplate.Member.PatientRelationToInsured;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.AddressLine1 = createClaimsTemplate.Member.AddressLine1;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.AddressLine2 = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.City = createClaimsTemplate.Member.City;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.State = createClaimsTemplate.Member.State;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Address.ZipCode = createClaimsTemplate.Member.ZipCode;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.ContactInfo.Phone = createClaimsTemplate.Member.ContactNumber;

            // insurance details mapping
            Subscriber.Plan.PlanName = createClaimsTemplate.PayerInfo.PayerId;
            Subscriber.Plan.PlanName = createClaimsTemplate.PayerInfo.PayerName;

            Address PlanAddress = new Address();
            PlanAddress.AddressLine1 = createClaimsTemplate.PayerInfo.PayerFirstAddress;
            PlanAddress.AddressLine2 = createClaimsTemplate.PayerInfo.PayerSecondAddress;
            PlanAddress.City = createClaimsTemplate.PayerInfo.PayerCity;
            PlanAddress.State = createClaimsTemplate.PayerInfo.PayerState;
            PlanAddress.ZipCode = createClaimsTemplate.PayerInfo.PayerZip;

            Subscriber.Plan.Address = PlanAddress;

            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Member.Subscriber.Add(Subscriber);
            //------------------------------------Facility Fields Mapping-------------------------------------------------
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.FacilityName = createClaimsTemplate.Facility.FacilityName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.FacilityName = createClaimsTemplate.Facility.FacilityName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.AddressLine1 = createClaimsTemplate.Facility.FacilityAddress1;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.AddressLine2 = createClaimsTemplate.Facility.FacilityAddress2;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.City = createClaimsTemplate.Facility.FacilityCity;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.Country = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.County = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.State = createClaimsTemplate.Facility.FacilityState;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.Street = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.Facility.Address.ZipCode = createClaimsTemplate.Facility.FacilityZip;
            //-------------------------rendering provider mapping---------------------------
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.LastName = createClaimsTemplate.RenderingProvider.ProviderLastName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.MiddleName = createClaimsTemplate.RenderingProvider.ProviderMiddleName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.FirstName = createClaimsTemplate.RenderingProvider.ProviderFirstName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.ContactInfo.Address.AddressLine1 = createClaimsTemplate.RenderingProvider.ProviderFirstAddress;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.ContactInfo.Address.AddressLine2 = createClaimsTemplate.RenderingProvider.ProviderSecondAddress;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.ContactInfo.Address.City = createClaimsTemplate.RenderingProvider.ProviderCity;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.ContactInfo.Address.State = createClaimsTemplate.RenderingProvider.ProviderState;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.ContactInfo.Address.ZipCode = createClaimsTemplate.RenderingProvider.ProviderZip;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.ContactInfo.Phone = createClaimsTemplate.RenderingProvider.ProviderPhoneNo;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.TaxonomyCode = createClaimsTemplate.RenderingProvider.Taxonomy;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RenderingProvider.FederalTaxNumber = createClaimsTemplate.RenderingProvider.TaxId;


            // Provider Fields Mapping
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.LastName = createClaimsTemplate.ReferringProvider.ReferringProviderLastName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.MiddleName = createClaimsTemplate.ReferringProvider.ReferringProviderMiddleName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.FirstName = createClaimsTemplate.ReferringProvider.ReferringProviderFirstName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.ContactInfo.Address.AddressLine1 = createClaimsTemplate.ReferringProvider.ReferringProviderFirstAddress;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.ContactInfo.Address.AddressLine2 = createClaimsTemplate.ReferringProvider.ReferringProviderSecondAddress;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.ContactInfo.Address.City = createClaimsTemplate.ReferringProvider.ReferringProviderCity;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.ContactInfo.Address.State = createClaimsTemplate.ReferringProvider.ReferringProviderState;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.ContactInfo.Phone = createClaimsTemplate.ReferringProvider.ReferringProviderPhoneNo;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.RefferingProvider.TaxonomyCode = createClaimsTemplate.ReferringProvider.ReferringProviderTaxonomy;

            // Payer Fields Mapping
            //BillingInfo.CodedEncounter.Encounter.EncounteredSchedule.Schedule.


            //------------------------------------Billing Provider Information Mapping---------------------------------------------------------
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Phone = createClaimsTemplate.BillingProvider.BillingProviderPhoneNo;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Address.AddressLine1 = createClaimsTemplate.BillingProvider.BillingProviderFirstAddress;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Address.AddressLine2 = createClaimsTemplate.BillingProvider.BillingProviderSecondAddress;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Address.City = createClaimsTemplate.BillingProvider.BillingProviderCity;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Address.State = createClaimsTemplate.BillingProvider.BillingProviderState;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Address.ZipCode = createClaimsTemplate.BillingProvider.BillingProviderZip;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Address.Country = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Address.County = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ContactInfo.Email = "";

            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.FirstName = createClaimsTemplate.BillingProvider.BillingProviderFirstName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.LastName = createClaimsTemplate.BillingProvider.BillingProviderLastOrOrganizationName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.MiddleName = createClaimsTemplate.BillingProvider.BillingProviderMiddleName;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.NPI = createClaimsTemplate.BillingProvider.BillingGroupNPI;
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.ProviderCode = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.SSN = "";
            BillingInfo.CodedEncounter.Encounter.EncounteredSchedule[0].Schedule.BillingProvider.TaxonomyCode = createClaimsTemplate.BillingProvider.BillingProviderTaxonomy;

            return BillingInfo;
        }


        public List<CPTCodeViewModel> GetCPTCodeHistory()
        {
            List<CPTCodeViewModel> CPTCodeLists = new List<CPTCodeViewModel>();
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00218", CPTDescription = "ANESTH SPECIAL HEAD SURGERY", Fee = 35.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00216", CPTDescription = "ANESTH HEAD VESSEL SURGERY", Fee = 52.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00215", CPTDescription = "ANESTH SKULL REPAIR/FRACT", Fee = 85.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00214", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 45.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00212", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 28.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00218", CPTDescription = "ANESTH SPECIAL HEAD SURGERY", Fee = 35.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00216", CPTDescription = "ANESTH HEAD VESSEL SURGERY", Fee = 52.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00215", CPTDescription = "ANESTH SKULL REPAIR/FRACT", Fee = 85.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00214", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 45.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00212", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 28.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00218", CPTDescription = "ANESTH SPECIAL HEAD SURGERY", Fee = 35.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00216", CPTDescription = "ANESTH HEAD VESSEL SURGERY", Fee = 52.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00215", CPTDescription = "ANESTH SKULL REPAIR/FRACT", Fee = 85.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00214", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 45.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00212", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 28.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00218", CPTDescription = "ANESTH SPECIAL HEAD SURGERY", Fee = 35.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00216", CPTDescription = "ANESTH HEAD VESSEL SURGERY", Fee = 52.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00215", CPTDescription = "ANESTH SKULL REPAIR/FRACT", Fee = 85.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00214", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 45.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00212", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 28.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00218", CPTDescription = "ANESTH SPECIAL HEAD SURGERY", Fee = 35.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00216", CPTDescription = "ANESTH HEAD VESSEL SURGERY", Fee = 52.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00215", CPTDescription = "ANESTH SKULL REPAIR/FRACT", Fee = 85.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00214", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 45.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { CPTCode = "00212", CPTDescription = "ANESTH SKULL DRAINAGE", Fee = 28.66 });          
            return CPTCodeLists;
        }

        public BillingProviderInfo GetSelectedBillingProvider(string ProviderId)
        {
            try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<BillingProviderInfo>("api/ProviderService/GetBillingProviderByProviderID?ProviderId=" + ProviderId + "", "ProviderServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ProviderResultViewModel GetSelectedRenderingProvider(string ProviderId){
        
             try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<ProviderResultViewModel>("api/ProviderService/GetRenderingProviderByProviderID?ProviderId=" + ProviderId + "", "ProviderServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ReferingProviderInfo GetSelectedReferringProvider(string ProviderId){
        
             try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<ReferingProviderInfo>("api/ProviderService/GetReferringProviderByProviderID?ProviderId=" + ProviderId + "", "ProviderServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SupervisingProviderInfo GetSelectedSupervisingProvider(string ProviderId){
        
             try
            {
                return Task.Run(() => ServiceRepository.GetDataFromService<SupervisingProviderInfo>("api/ProviderService/GetSupervisingProviderByProviderID?ProviderId=" + ProviderId + "", "ProviderServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}