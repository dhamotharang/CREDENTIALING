using AutoMapper;
using Newtonsoft.Json;
using PortalTemplate.Areas.Encounters.Models;
using PortalTemplate.Areas.Encounters.Models.CreateEncounter;
using PortalTemplate.Areas.Encounters.Models.Schedule;
using PortalTemplate.Areas.Encounters.Services.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Services
{
    public class CreateEncounter : ICreateEncounter
    {
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
            MemberResultList.Add(new MemberViewModel { MemberId = 1, ProviderId = 1, SubscriberID = "UL0000441", MemberLastName = "KJELL", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1935, 04, 26), Address = "6046NEWMARK ST", PCP = "MERCELY DEVABAVUS", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 2, ProviderId = 2, SubscriberID = "UL0000442", MemberLastName = "MARY", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1933, 02, 18), Address = "6046NEWMARK ST", PCP = "MERCELY DEVABAVUS", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 3, ProviderId = 3, SubscriberID = "UL0003909", MemberLastName = "ROY", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1942, 01, 23), Address = "3106SAW MILL LN", PCP = "VENKATREDDY ALUGUBELLI", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 4, ProviderId = 4, SubscriberID = "UL0003910", MemberLastName = "BARBARA", MemberMiddleName = "", MemberFirstName = "AANONSEN", DateOfBirth = new DateTime(1941, 01, 21), Address = "3106SAW MILL LN", PCP = "VENKATREDDY ALUGUBELLI", PayerName = "ULTIMATE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 5, ProviderId = 5, SubscriberID = "P00090227", MemberLastName = "LOUISA", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1944, 12, 14), Address = "524INDEPENDENCE HWY", PCP = "SYED ZAIDI", PayerName = "FREEDOM HEALTH INS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 6, ProviderId = 6, SubscriberID = "T00017794", MemberLastName = "ANNIE", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1943, 04, 12), Address = "3028N AVENIDA REPUBLICA DE CUBA APT 310", PCP = "MIGUEL FANA", PayerName = "OPTIMUM CAP", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 7, ProviderId = 7, SubscriberID = "T00021788", MemberLastName = "KATHRYN", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1949, 05, 04), Address = "2227EVANGELINA AVE", PCP = "MARIA SCUNZIANO SINGH", PayerName = "OPTIMUM CAP", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 8, ProviderId = 8, SubscriberID = "667374", MemberLastName = "SHIRLEY", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1955, 06, 13), Address = "478538TH CIR APT 108 ", PCP = "NICHOLAS  COPPOLA", PayerName = "WELLCARE HEALTH PLANS", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 9, ProviderId = 9, SubscriberID = "99407342800", MemberLastName = "MONROE", MemberMiddleName = "", MemberFirstName = "AARON", DateOfBirth = new DateTime(1942, 11, 19), Address = "9838LAKE DR", PCP = "POONAM MALHOTRA", PayerName = "HUMANA", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });
            MemberResultList.Add(new MemberViewModel { MemberId = 10, ProviderId = 10, SubscriberID = "80331907201", MemberLastName = "MARIA", MemberMiddleName = "", MemberFirstName = "ABAD", DateOfBirth = new DateTime(1997, 04, 03), Address = "16500COLLINS AVE APT 2452", PCP = "GARY MERLINO", PayerName = "COVENTRY", StartDate = new DateTime(2002, 10, 02), EndDate = new DateTime(2018, 10, 02) });

            return MemberResultList;
        }


        public PrimaryEncounterViewModel GetEncounterDetails()
        {
            PrimaryEncounterViewModel PrimaryEncounter = new PrimaryEncounterViewModel();
            PrimaryEncounter.RenderingProviderNPI = "1417989625";
            PrimaryEncounter.RenderingProviderFirstName = "SANDY";
            PrimaryEncounter.RenderingProviderMiddleName = "";
            PrimaryEncounter.RenderingProviderLastOrOrganizationName = "AANONSEN";
            PrimaryEncounter.RenderingProviderTaxonomy = "A54545555";

            PrimaryEncounter.SubscriberID = "K277057937";
            PrimaryEncounter.PatientFirstName = "Smith";
            PrimaryEncounter.PatientMiddleName = "";
            PrimaryEncounter.PatientLastOrOrganizationName = "Lucas";
            PrimaryEncounter.PatientGender = "Male";
            PrimaryEncounter.PatientBirthDate = new DateTime(1956, 07, 02);
            PrimaryEncounter.PatientFirstAddress = "10048";
            PrimaryEncounter.PatientSecondAddress = "Amidon";
            PrimaryEncounter.PayerName = "Humana";

            PrimaryEncounter.EncounterId = "116244951";
            PrimaryEncounter.EncounterStatus = "Open";

            return PrimaryEncounter;
        }


        public PrimaryEncounterViewModel GetEncounterDetailsForMember()
        {
            PrimaryEncounterViewModel PrimaryEncounter = new PrimaryEncounterViewModel();
            PrimaryEncounter.RenderingProviderNPI = "1417989625";
            PrimaryEncounter.RenderingProviderFirstName = "SANDY";
            PrimaryEncounter.RenderingProviderMiddleName = "";
            PrimaryEncounter.RenderingProviderLastOrOrganizationName = "AANONSEN";
            PrimaryEncounter.RenderingProviderTaxonomy = "A54545555";

            PrimaryEncounter.SubscriberID = "K277057937";
            PrimaryEncounter.PatientFirstName = "Smith";
            PrimaryEncounter.PatientMiddleName = "";
            PrimaryEncounter.PatientLastOrOrganizationName = "Lucas";
            PrimaryEncounter.PatientGender = "Male";
            PrimaryEncounter.PatientBirthDate = new DateTime(1956, 07, 02);
            PrimaryEncounter.PatientFirstAddress = "10048";
            PrimaryEncounter.PatientSecondAddress = "Amidon";
            PrimaryEncounter.PayerName = "Humana";

            PrimaryEncounter.EncounterId = "116244951";
            PrimaryEncounter.EncounterStatus = "Open";
            return PrimaryEncounter;
        }


        public CodeDetailsViewModel GetCodingDetails(PrimaryEncounterViewModel EncounterDetails)
        {
            CodeDetailsViewModel CodeDetails = new CodeDetailsViewModel();
            CodeDetails.EncounterDetails = EncounterDetails;

            PortalTemplate.Areas.Coding.Models.CreateCoding.CodingDetailsViewModel CodingInfo = new Coding.Models.CreateCoding.CodingDetailsViewModel();
            //------------------ICD codes------------------------

            PortalTemplate.Areas.Coding.Models.CreateCoding.ICDCodeDetailsViewModel ICDCodeDetails = new Coding.Models.CreateCoding.ICDCodeDetailsViewModel();
            List<PortalTemplate.Areas.Coding.Models.ICDCodes.ICDCodeViewModel> IcdCodes = new List<Coding.Models.ICDCodes.ICDCodeViewModel>();
            PortalTemplate.Areas.Coding.Models.ICDCodes.ICDCodeViewModel ICDCode = new Coding.Models.ICDCodes.ICDCodeViewModel();
            List<PortalTemplate.Areas.Coding.Models.ICDCodes.HCCCodeViewModel> HCCCodes = new List<Coding.Models.ICDCodes.HCCCodeViewModel>();
            HCCCodes.Add(new PortalTemplate.Areas.Coding.Models.ICDCodes.HCCCodeViewModel { });
            ICDCode.HCCCodes = HCCCodes;
            IcdCodes.Add(ICDCode);
            ICDCodeDetails.ICDCodes = IcdCodes;
            CodingInfo.ICDCodeDetails = ICDCodeDetails;

            //-----------------CPT Codes------------------------------
            PortalTemplate.Areas.Coding.Models.CreateCoding.CPTCodeDetailsViewModel CPTCodeDetails = new Coding.Models.CreateCoding.CPTCodeDetailsViewModel();
            List<PortalTemplate.Areas.Coding.Models.ICDCPTMapping.ICDCPTCodemappingViewModel> CPTCodes = new List<PortalTemplate.Areas.Coding.Models.ICDCPTMapping.ICDCPTCodemappingViewModel>();
            CPTCodes.Add(new PortalTemplate.Areas.Coding.Models.ICDCPTMapping.ICDCPTCodemappingViewModel { });
            CPTCodeDetails.CPTCodes = CPTCodes;
            CodingInfo.CPTCodeDetails = CPTCodeDetails;

            CodeDetails.CodingDetails = CodingInfo;
            return CodeDetails;
        }


        public AuditingViewModel GetAuditingDetails(EncounterCodingDetailsViewModel CodeDetails)
        {
            AuditingViewModel AuditingDetails = new PortalTemplate.Areas.Encounters.Models.CreateEncounter.AuditingViewModel();
            PrimaryEncounterViewModel EncounterDetails = new PrimaryEncounterViewModel();
            EncounterDetails = Mapper.Map<EncounterCodingDetailsViewModel, PrimaryEncounterViewModel>(CodeDetails);
            PortalTemplate.Areas.Auditing.Models.CreateAuditing.ICDCodesInfoViewModel IcdCodingDetails = new Auditing.Models.CreateAuditing.ICDCodesInfoViewModel();
            IcdCodingDetails = Mapper.Map<EncounterCodingDetailsViewModel, PortalTemplate.Areas.Auditing.Models.CreateAuditing.ICDCodesInfoViewModel>(CodeDetails);
            PortalTemplate.Areas.Auditing.Models.CreateAuditing.CPTCodesInfoViewModel CptCodingDetails = new Auditing.Models.CreateAuditing.CPTCodesInfoViewModel();
            CptCodingDetails = Mapper.Map<EncounterCodingDetailsViewModel, PortalTemplate.Areas.Auditing.Models.CreateAuditing.CPTCodesInfoViewModel>(CodeDetails);
            PortalTemplate.Areas.Auditing.Models.CreateAuditing.CodingDetailsViewModel CodingDetails = new Auditing.Models.CreateAuditing.CodingDetailsViewModel();
            CodingDetails.CPTCodeDetails = CptCodingDetails;
            CodingDetails.ICDCodeDetails = IcdCodingDetails;
            AuditingDetails.CodingDetails = CodingDetails;
            AuditingDetails.EncounterDetails = EncounterDetails;
            return AuditingDetails;
        }


        public List<ICDCodeViewModel> GetActiveDiagnosisCode()
        {
            List<ICDCodeViewModel> ICDCodes = new List<ICDCodeViewModel>();
            ICDCodes.Add(new ICDCodeViewModel { ICDCode = "A207", CodeDescription = "SEPTICEMIC PLAGUE", HCCCode = "2", HCCVersion = "MEDICAL", HCCDescription = "SEPTICEMIA/SHOCK", HCCWeight = "0.6" });
            ICDCodes.Add(new ICDCodeViewModel { ICDCode = "B207", CodeDescription = "SEPTICEMIC PLAGUE", HCCCode = "2", HCCVersion = "RX", HCCDescription = "SEPTICEMIA/SHOCK", HCCWeight = "0.6,0.3" });
            return ICDCodes;
        }


        public List<ActiveProcedureCodeViewModel> GetActiveProcedureCode()
        {
            List<ActiveProcedureCodeViewModel> ActiveProcedureCodes = new List<ActiveProcedureCodeViewModel>();
            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86152", CPTDescription = "CELL ENUMERATION &ID" });
            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86153", CPTDescription = "CELL ENUMERATION PHYS INTERP" });
            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86890", CPTDescription = "AUTOLOGOUS BLOOD PROCESS" });

            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86891", CPTDescription = "AUTOLOGOUS BLOOD OP SALVAGE" });
            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86927", CPTDescription = "PLASMA FRESH FROZEN" });
            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86930", CPTDescription = "FROZEN BLOOD PREP" });

            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86931", CPTDescription = "FROZEN BLOOD THAW" });
            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86932", CPTDescription = "FROZEN BLOOD FREEZE/THAW" });
            ActiveProcedureCodes.Add(new ActiveProcedureCodeViewModel { CPTCode = "86945", CPTDescription = "BLOOD PRODUCT/IRRADIATION" });

            return ActiveProcedureCodes;
        }


        public List<DocumentHistoryViewModel> GetDocumentHistory()
        {
            List<DocumentHistoryViewModel> DocumentHistoryList = new List<DocumentHistoryViewModel>();
            DocumentHistoryList.Add(new DocumentHistoryViewModel { DocumentName = "NOTES", DocumentCategory = "PROGRESS NOTE", UploadedBy = "MOORE", UploadedOn = new DateTime(2016, 07, 05), DocumentPath = "/Content/CustomTheme/PBAS%20Documents/SampleProgressNote.pdf" });
            DocumentHistoryList.Add(new DocumentHistoryViewModel { DocumentName = "X-RAY", DocumentCategory = "LAB REPORT", UploadedBy = "MOORE", UploadedOn = new DateTime(2016, 07, 05), DocumentPath = "/Content/CustomTheme/PBAS%20Documents/SampleProgressNote.pdf" });
            DocumentHistoryList.Add(new DocumentHistoryViewModel { DocumentName = "SONOGRAM", DocumentCategory = "LAB REPORT", UploadedBy = "MOORE", UploadedOn = new DateTime(2016, 07, 05), DocumentPath = "/Content/CustomTheme/PBAS%20Documents/SampleProgressNote.pdf" });

            return DocumentHistoryList;
        }


        public List<ReferingProviderListViewModel> GetReferingProviderList()
        {
            List<ReferingProviderListViewModel> ReferingProviderList = new List<ReferingProviderListViewModel>();

            ReferingProviderList.Add(new ReferingProviderListViewModel { ProviderNPI = "1417989625", ProviderFirstName = "PARIKSITH", ProviderLastName = "SINGH", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            ReferingProviderList.Add(new ReferingProviderListViewModel { ProviderNPI = "1730249210", ProviderFirstName = "NISHAT", ProviderLastName = "SEEMA", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            ReferingProviderList.Add(new ReferingProviderListViewModel { ProviderNPI = "1164611349", ProviderFirstName = "DEVABAVUS", ProviderLastName = "MERCELY", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            ReferingProviderList.Add(new ReferingProviderListViewModel { ProviderNPI = "1518933456", ProviderFirstName = "DEAM", ProviderLastName = "DAVID", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });

            return ReferingProviderList;
        }


        public List<BillingProviderListViewModel> GetBillingProviderList()
        {
            List<BillingProviderListViewModel> BillingProviderList = new List<BillingProviderListViewModel>();

            BillingProviderList.Add(new BillingProviderListViewModel { ProviderNPI = "1417989625", ProviderFirstName = "PARIKSITH", ProviderLastName = "SINGH", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            BillingProviderList.Add(new BillingProviderListViewModel { ProviderNPI = "1730249210", ProviderFirstName = "NISHAT", ProviderLastName = "SEEMA", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            BillingProviderList.Add(new BillingProviderListViewModel { ProviderNPI = "1164611349", ProviderFirstName = "DEVABAVUS", ProviderLastName = "MERCELY", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            BillingProviderList.Add(new BillingProviderListViewModel { ProviderNPI = "1518933456", ProviderFirstName = "DEAM", ProviderLastName = "DAVID", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });

            return BillingProviderList;
        }


        public List<RenderingProviderListViewModel> GetRenderingProviderList()
        {
            List<RenderingProviderListViewModel> RenderingProviderList = new List<RenderingProviderListViewModel>();

            RenderingProviderList.Add(new RenderingProviderListViewModel { ProviderNPI = "1417989625", ProviderFirstName = "PARIKSITH", ProviderLastName = "SINGH", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            RenderingProviderList.Add(new RenderingProviderListViewModel { ProviderNPI = "1730249210", ProviderFirstName = "NISHAT", ProviderLastName = "SEEMA", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            RenderingProviderList.Add(new RenderingProviderListViewModel { ProviderNPI = "1164611349", ProviderFirstName = "DEVABAVUS", ProviderLastName = "MERCELY", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });
            RenderingProviderList.Add(new RenderingProviderListViewModel { ProviderNPI = "1518933456", ProviderFirstName = "DEAM", ProviderLastName = "DAVID", Speciality = "INTERNAL MEDICINE", TaxanomyCode = "207R00000X" });

            return RenderingProviderList;
        }


        public List<FacilityListViewModel> GetFacilityList()
        {
            List<FacilityListViewModel> FacilityList = new List<FacilityListViewModel>();

            FacilityList.Add(new FacilityListViewModel { FacilityID = "1094", FacilityName = "YALE AVENUE", FacilityAddress = "1094 NORTH CLIFFE DLVE" });
            FacilityList.Add(new FacilityListViewModel { FacilityID = "10441", FacilityName = "SW SAINT LUCIE WEST BLVD STE 209", FacilityAddress = "10441 QUALITY DR" });
            FacilityList.Add(new FacilityListViewModel { FacilityID = "10494", FacilityName = "SHUMARD OAK DRIVE SUITE 101", FacilityAddress = "10494 INTERNAL MED" });
            FacilityList.Add(new FacilityListViewModel { FacilityID = "10045", FacilityName = "CORTEZ BLVD", FacilityAddress = "1094 NORTH CLIFFE DLVE" });

            return FacilityList;
        }


        public List<ClaimHistoryViewModel> GetClaimsHistory()
        {
            List<ClaimHistoryViewModel> ClaimHistoryList = new List<ClaimHistoryViewModel>();
            ClaimHistoryList.Add(new ClaimHistoryViewModel { EncounterID = "126985", MemberFirstName = "JOHN", MemberLastName = "KATHALEEN", ProviderFirstName = "ANDRE", ProviderLastName = "MOORE", BillingProvider = "ACCESS HEALTH CARE", DateOfCreation = new DateTime(2016, 01, 01), DateOfService = new DateTime(2016, 01, 01), Age = "55", CreatedBy = "CAPRI RITCH", EncounterType = "ENCOUNTER(CAP)", Status = "ON HOLD" });

            return ClaimHistoryList;
        }


        public Models.Schedule.ProviderSelectMemberViewModel GerProviderSelectMembers(string ProviderId)
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