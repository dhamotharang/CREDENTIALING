using PortalTemplate.Areas.Coding.Models.CreateCoding;
using PortalTemplate.Areas.Coding.Models.CodingList;
using PortalTemplate.Areas.Coding.Models.ICDCodes;
using PortalTemplate.Areas.Coding.Models.CPTCodes;
using Newtonsoft.Json;
using PortalTemplate.Areas.Coding.Models.ICDCPTMapping;
using PortalTemplate.Areas.SharedView.Models.Encounter;
using PortalTemplate.Areas.Coding.Services.IServices;
using PortalTemplate.Areas.Coding.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Coding.Models.Notes;
using PortalTemplate.Areas.SharedView.Models.Notes;
using PortalTemplate.Areas.Coding.Models.DashBoard;
using PortalTemplate.Areas.Coding.DTO;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Coding.Services
{
    public class CodingService : ICodingService
    {
        HttpClient client = null;


        #region DashBoard
        public CodingDashBoardViewModel GetCodingDashBoardDetails()
        {
            CodingDashBoardViewModel DashBoardDetails = new CodingDashBoardViewModel();
            CodedEncounterCountsViewModel StatusCounts = new CodedEncounterCountsViewModel();
            StatusCounts.DraftCount = 23;
            StatusCounts.OnHoldCount = 54;
            StatusCounts.OpenCount = 34;
            StatusCounts.ReadytoAuditCount = 56;
            DashBoardDetails.BiscuitCounts = StatusCounts;
            return DashBoardDetails;
        }
        #endregion

        #region Coding List
        List<CodingListViewModel> ReadytoauditLists = new List<CodingListViewModel>();
        List<CodingListViewModel> DraftLists = new List<CodingListViewModel>();
        List<CodingListViewModel> OpenLists = new List<CodingListViewModel>();
        List<CodingListViewModel> OnHoldLists = new List<CodingListViewModel>();
        List<CodingListViewModel> InactiveLists = new List<CodingListViewModel>();
        List<CodingListViewModel> RejectedLists = new List<CodingListViewModel>();

        public CodingService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CodingServiceWebAPIURL"]);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });
            OpenLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Open" });

            DraftLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Draft" });
            DraftLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Draft" });
            DraftLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Draft" });
            DraftLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Draft" });
            DraftLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Draft" });
            DraftLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Draft" });
            DraftLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Draft" });

            ReadytoauditLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Audit" });
            ReadytoauditLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Audit" });
            ReadytoauditLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Audit" });
            ReadytoauditLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Audit" });
            ReadytoauditLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "Ready to Audit" });


            OnHoldLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On-Hold" });
            OnHoldLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On-Hold" });
            OnHoldLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "On-Hold" });

            InactiveLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "InActive" });
            InactiveLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "InActive" });
            InactiveLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "InActive" });
            InactiveLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "InActive" });
            InactiveLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "InActive" });
            InactiveLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "InActive" });
            InactiveLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "InActive" });

            RejectedLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "REJECTED" });
            RejectedLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "REJECTED" });
            RejectedLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "REJECTED" });
            RejectedLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "REJECTED" });
            RejectedLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "REJECTED" });
            RejectedLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "REJECTED" });
            RejectedLists.Add(new CodingListViewModel { EncounterID = "116244953", MemberID = "1607132022", MemberFirstName = "Kameisha", MemberLastName = "Oliver", ProviderNPI = "3282556434", ProviderFirstName = "Ignazio", ProviderLastName = "Rubino", Facility = "ALL SAINTS...", DateOfService = new DateTime(2015, 05, 06), DateOfCreation = new DateTime(2015, 06, 08), CreatedBy = "McCarthy", EncounterType = "HMO", Status = "REJECTED" });

        }
        public async Task<List<CodingListViewModel>> GetCodingListByStatus(string status)
        {
           
            try
            {
                List<CodingListViewModel> result = null;  
                HttpResponseMessage response = await client.GetAsync("api/coding/status");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<List<CodingListViewModel>>();
                    
                }
                return result;
                
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        

        //****************To Get Counts of all tabs in Coding List" ****************************//
        public CodingListCountsViewModel GetCodingListStatusCount()
        {
            CodingListCountsViewModel EncountersListsCount = new CodingListCountsViewModel();
            EncountersListsCount.DraftEncountersCount = DraftLists.Count;
            EncountersListsCount.InactiveEncountersCount = InactiveLists.Count;
            EncountersListsCount.OnHoldEncountersCount = OnHoldLists.Count;
            EncountersListsCount.OpenEncountersCount = OpenLists.Count;
            EncountersListsCount.ReadytoAuditEncountersCount = ReadytoauditLists.Count;
            EncountersListsCount.RejectedEncountersCount = RejectedLists.Count;
            return EncountersListsCount;
        }

        public DeactivateEncounter GetDeactivatemodalData()
        {
            DeactivateEncounter DeactivateData = new DeactivateEncounter();
            DeactivateData.EncounterID = "116244953";
            DeactivateData.MemberID = "1607132022";
            DeactivateData.MemberFirstName = "Kameisha";
            DeactivateData.MemberLastName = "Oliver";
            DeactivateData.Gender = "MALE";
            DeactivateData.DOB = new DateTime(1985, 05, 06);
            DeactivateData.PlanName = "HUMANA";
            DeactivateData.PreviousStatus = "OPEN";
            DeactivateData.CurrentStatus = "DEACTIVATE";
            DeactivateData.DeactiveReason = "";
            return DeactivateData;
        }

        public Boolean DeactivateEncounter()
        {
            return true;
        }

        #endregion

        #region Create Coding
        public CreateCodingViewModel Create()
        {
            CreateCodingViewModel CreateCodingDetails = new CreateCodingViewModel();
            EncounterViewModel EncounterInfo = new EncounterViewModel();

            EncounterInfo.EncounterDetails.CheckInTime = "10:30 am";
            EncounterInfo.EncounterDetails.CheckOutTime = "11:30 am";
            EncounterInfo.EncounterDetails.DOSFrom = new DateTime(2016, 10, 09);
            EncounterInfo.EncounterDetails.DOSTo = new DateTime(2016, 10, 10);
            EncounterInfo.EncounterDetails.EncounterId = "116244951";
            //EncounterInfo.EncounterDetails.EncounterNotes = "PROGRESS NOTES NOT ATTACHED";
            EncounterInfo.EncounterDetails.EncounterStatus = "OPEN";
            EncounterInfo.EncounterDetails.EncounterType = "HMO";
            EncounterInfo.EncounterDetails.NextAppointmentDate = new DateTime(2016, 12, 12);
            EncounterInfo.EncounterDetails.PatientBirthDate = new DateTime(1985, 10, 10);
            EncounterInfo.EncounterDetails.PatientCity = "AMIDON";
            EncounterInfo.EncounterDetails.PatientFirstAddress = "";
            EncounterInfo.EncounterDetails.PatientFirstName = "SMITH";
            EncounterInfo.EncounterDetails.PatientGender = "MALE";
            EncounterInfo.EncounterDetails.PatientLastOrOrganizationName = "LUCAS";
            EncounterInfo.EncounterDetails.PatientMiddleName = "";
            EncounterInfo.EncounterDetails.PatientSecondAddress = "";
            EncounterInfo.EncounterDetails.PatientState = "FLORIDA";
            EncounterInfo.EncounterDetails.PatientZip = "100482";
            EncounterInfo.EncounterDetails.PlanName = "HUMANA";
            EncounterInfo.EncounterDetails.ReferringProvider = "PARIKSITH SINGH";
            EncounterInfo.EncounterDetails.BillingProvider = "PARIKSITH SINGH";
            EncounterInfo.EncounterDetails.ServiceFacility = "1094 NORTH CLIFFE DLVE";
            EncounterInfo.EncounterDetails.DOSFrom = new DateTime(2016, 10, 10);
            EncounterInfo.EncounterDetails.DOSTo = new DateTime(2016, 10, 11);
            EncounterInfo.EncounterDetails.PlaceOfService = "21-INPATIENT HOSPITAL";
            EncounterInfo.EncounterDetails.RenderingProviderFirstName = "PARIKSITH";
            EncounterInfo.EncounterDetails.RenderingProviderLastOrOrganizationName = "SINGH";
            EncounterInfo.EncounterDetails.RenderingProviderMiddleName = "";
            EncounterInfo.EncounterDetails.RenderingProviderNPI = "1417989625";
            EncounterInfo.EncounterDetails.RenderingProviderSpeciality = "INTERNAL MEDICINE";
            EncounterInfo.EncounterDetails.RenderingProviderTaxonomy = "207R00000X";
            EncounterInfo.EncounterDetails.SubscriberID = "K277057937";
            EncounterInfo.EncounterDetails.VisitLength = "30 MINUTES";
            EncounterInfo.EncounterDetails.VisitReason = "MVA";

            //List<CategoryViewModel> CategoryDetails = new List<CategoryViewModel>();
            //CategoryViewModel category1 = new CategoryViewModel { CategoryName = "HISTORY OF PRESENT ILLNESS", Remarks = "PATIENT HAS HISTORY OF ILLNESS", CategoryCode = "001", selected = true };
            //CategoryViewModel category2 = new CategoryViewModel { CategoryName = "CHIEF COMPLAINT", Remarks = "THERE IS NO CHEIF COMPLAINT AVAILABLE", CategoryCode = "000", selected = true };
            //CategoryViewModel category3 = new CategoryViewModel { CategoryName = "HISTORY REFERENCES", Remarks = "THERE ARE NO REFERENCES FOR PATIENT HISTORY", CategoryCode = "004", selected = true };
            //CategoryDetails.Add(category1);
            //CategoryDetails.Add(category2);
            //CategoryDetails.Add(category3);

            //EncounterInfo.EncounterDecision.Categories = CategoryDetails;
            EncounterInfo.EncounterDecision.IsAgree = true;

            CreateCodingDetails.EncounterDetails = EncounterInfo;

            //*************Coding Details***************//
            CodingDetailsViewModel CodingInfo = new CodingDetailsViewModel();
            //*************ICD CODES***************//
            ICDCodeDetailsViewModel ICDCodeInfo = new ICDCodeDetailsViewModel();
            List<ICDCodeViewModel> icds = new List<ICDCodeViewModel>();
            List<HCCCodeViewModel> HCCs = new List<HCCCodeViewModel>();
            HCCs.Add(new HCCCodeViewModel { });
            ICDCodeViewModel ICDCode = new ICDCodeViewModel();
            ICDCode.HCCCodes = HCCs;
            icds.Add(ICDCode);
            ICDCodeInfo.ICDCodes = icds;
            ICDCodeInfo.IsICD10 = false;
            //****************CPT CODES******************//
            CPTCodeDetailsViewModel CPTCodeInfo = new CPTCodeDetailsViewModel();
            List<ICDCPTCodemappingViewModel> cpts = new List<ICDCPTCodemappingViewModel>();
            cpts.Add(new ICDCPTCodemappingViewModel { });
            CPTCodeInfo.CPTCodes = cpts;

            CodingInfo.ICDCodeDetails = ICDCodeInfo;
            CodingInfo.CPTCodeDetails = CPTCodeInfo;

            CreateCodingDetails.CodingDetails = CodingInfo;


            DocumentListViewModel documentList = new DocumentListViewModel();

            DocumentListViewModel ProgressNote = new DocumentListViewModel();
            DocumentListViewModel LabReport = new DocumentListViewModel();
            DocumentListViewModel Sonogram = new DocumentListViewModel();
            DocumentListViewModel DocHistoryLabReport = new DocumentListViewModel();

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

            CreateCodingDetails.EncounterDetails.EncounterDetails.DocumentDetails = documentList;

            List<CodingNotesViewModel> CodingNotes = new List<CodingNotesViewModel>();
            CodingNotes.Add(new CodingNotesViewModel { Title = "Notes", Description = "This patient is illness", Category = "NotesCategory", Module = "Encounter", Notify = "Maccarthy", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Patient History", Description = "This Encounter has to be referenced for future purpose", Category = "NotesCategory", Module = "Encounter", Notify = "Swathi", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Illness", Description = "Coding Details has to be update", Category = "NotesCategory", Module = "Encounter", Notify = "Sharanya", AddedBy = "Maccarthy", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Reports", Description = "Auditing Information has to be updated", Category = "NotesCategory", Module = "Encounter", Notify = "Amoore", AddedBy = "Finch", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Documents", Description = "Documents has to be updated", Category = "NotesCategory", Module = "Encounter", Notify = "Maccarthy", AddedBy = "Daneil", AddedOn = new DateTime(2015, 10, 10) });
            NotesViewModel CodingNotesInfo = new NotesViewModel();
            CodingNotesInfo.CodingNotes = CodingNotes;
            CreateCodingDetails.CodingDetails.Notes = CodingNotesInfo;

            List<EncounterNotesViewModel> EncounterNotes = new List<EncounterNotesViewModel>();
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Notes", Description = "This patient is illness", Category = "NotesCategory", Module = "Encounter", Notify = "Maccarthy", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Patient History", Description = "This Encounter has to be referenced for future purpose", Category = "NotesCategory", Module = "Encounter", Notify = "Swathi", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Illness", Description = "Coding Details has to be update", Category = "NotesCategory", Module = "Coding", Notify = "Sharanya", AddedBy = "Maccarthy", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Reports", Description = "Auditing Information has to be updated", Category = "NotesCategory", Module = "Auditing", Notify = "Amoore", AddedBy = "Finch", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Documents", Description = "Documents has to be updated", Category = "NotesCategory", Module = "Coding", Notify = "Maccarthy", AddedBy = "Daneil", AddedOn = new DateTime(2015, 10, 10) });

            CreateCodingDetails.EncounterDetails.EncounterDetails.EncounterNotes = EncounterNotes;

            return CreateCodingDetails;
        }

        public ICDCodeHistoryDetailsViewModel GetICDCodeHistory()
        {
            List<HCCCodeViewModel> HccCodeList1 = new List<HCCCodeViewModel>();
            List<HCCCodeViewModel> HccCodeList2 = new List<HCCCodeViewModel>();
            HCCCodeViewModel HccCode1 = new HCCCodeViewModel();
            HCCCodeViewModel HccCode2 = new HCCCodeViewModel();
            HccCode1.Code = "23";
            HccCode1.Description = "Septicemia/Shock";
            HccCode1.Type = "Medical";
            HccCode1.Version = "v22";
            HccCode1.Weight = "0.2";

            HccCode2.Code = "23";
            HccCode2.Description = "Septicemia/Shock";
            HccCode2.Type = "Medical";
            HccCode2.Version = "v22";
            HccCode2.Weight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);
            HccCodeList2.Add(HccCode2);

            ICDCodeHistoryDetailsViewModel ICDCodesHisory = new ICDCodeHistoryDetailsViewModel();
            List<ICDCodeViewModel> ICDCodeLists = new List<ICDCodeViewModel>();
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1, IsChronic = true, ChronicCount = 2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = false });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 0 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });
            ICDCodesHisory.ICDCodesHistory = ICDCodeLists;
            return ICDCodesHisory;
        }

        public List<ICDCodeViewModel> GetIcdHistoryData()
        {
            List<HCCCodeViewModel> HccCodeList1 = new List<HCCCodeViewModel>();
            List<HCCCodeViewModel> HccCodeList2 = new List<HCCCodeViewModel>();
            HCCCodeViewModel HccCode1 = new HCCCodeViewModel();
            HCCCodeViewModel HccCode2 = new HCCCodeViewModel();
            HccCode1.Code = "23";
            HccCode1.Description = "Septicemia/Shock";
            HccCode1.Type = "Medical";
            HccCode1.Version = "v22";
            HccCode1.Weight = "0.2";

            HccCode2.Code = "23";
            HccCode2.Description = "Septicemia/Shock";
            HccCode2.Type = "Medical";
            HccCode2.Version = "v22";
            HccCode2.Weight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);

            HccCodeList2.Add(HccCode2);

            List<ICDCodeViewModel> ICDCodeLists = new List<ICDCodeViewModel>();
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            return ICDCodeLists;

        }

        public List<CPTCodeViewModel> GetCPTCodeHistory()
        {
            List<CPTCodeViewModel> CPTCodeLists = new List<CPTCodeViewModel>();
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "00218", Description = "ANESTH SPECIAL HEAD SURGERY", Fee = 35.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "00216", Description = "ANESTH HEAD VESSEL SURGERY", Fee = 52.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "00215", Description = "ANESTH SKULL REPAIR/FRACT", Fee = 85.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "00214", Description = "ANESTH SKULL DRAINAGE", Fee = 45.66 });
            CPTCodeLists.Add(new CPTCodeViewModel { Code = "00212", Description = "ANESTH SKULL DRAINAGE", Fee = 28.66 });
            return CPTCodeLists;
        }

        public List<ICDCPTCodemappingViewModel> GetCptHistoryData()
        {
            List<ICDCPTCodemappingViewModel> CPTCodeLists = new List<ICDCPTCodemappingViewModel>();
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77852", Description = "CELL ENEMERATION ID", Modifier1 = "10", Modifier2 = "15", Modifier3 = "11", Modifier4 = "12", DiagnosisPointer1 = "A207", DiagnosisPointer2 = "C204", DiagnosisPointer3 = "B407", DiagnosisPointer4 = "A206", Fee = 35.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77853", Description = "CELL ENUMERATION PHYS THERAPY", Modifier1 = "8", Modifier2 = "25", Modifier3 = "51", Modifier4 = "12", DiagnosisPointer1 = "A208", DiagnosisPointer2 = "C205", DiagnosisPointer3 = "B408", DiagnosisPointer4 = "A209", Fee = 32.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77854", Description = "AUTOLOGOUS BLOOD PROCESS", Modifier1 = "9", Modifier2 = "20", Modifier3 = "19", Modifier4 = "19", DiagnosisPointer1 = "A209", DiagnosisPointer2 = "C206", DiagnosisPointer3 = "B409", DiagnosisPointer4 = "A208", Fee = 45.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "99201", Description = "", Fee = 75.66, isEnM = true });
            return CPTCodeLists;
        }
        #endregion

        #region View Coding
        public CreateCodingViewModel ViewDetails()
        {
            CreateCodingViewModel ViewCodingDetails = new CreateCodingViewModel();
            EncounterViewModel EncounterInfo = new EncounterViewModel();

            EncounterInfo.EncounterDetails.CheckInTime = "10:30 am";
            EncounterInfo.EncounterDetails.CheckOutTime = "11:30 am";
            EncounterInfo.EncounterDetails.DOSFrom = new DateTime(2016, 10, 09);
            EncounterInfo.EncounterDetails.DOSTo = new DateTime(2016, 10, 10);
            EncounterInfo.EncounterDetails.EncounterId = "116244951";
            //EncounterInfo.EncounterDetails.EncounterNotes = "PROGRESS NOTES NOT ATTACHED";
            EncounterInfo.EncounterDetails.EncounterStatus = "OPEN";
            EncounterInfo.EncounterDetails.EncounterType = "HMO";
            EncounterInfo.EncounterDetails.NextAppointmentDate = new DateTime(2016, 12, 12);
            EncounterInfo.EncounterDetails.PatientBirthDate = new DateTime(1985, 10, 10);
            EncounterInfo.EncounterDetails.PatientCity = "AMIDON";
            EncounterInfo.EncounterDetails.PatientFirstAddress = "";
            EncounterInfo.EncounterDetails.PatientFirstName = "SMITH";
            EncounterInfo.EncounterDetails.PatientGender = "MALE";
            EncounterInfo.EncounterDetails.PatientLastOrOrganizationName = "LUCAS";
            EncounterInfo.EncounterDetails.PatientMiddleName = "";
            EncounterInfo.EncounterDetails.PatientSecondAddress = "";
            EncounterInfo.EncounterDetails.PatientState = "FLORIDA";
            EncounterInfo.EncounterDetails.PatientZip = "100482";
            EncounterInfo.EncounterDetails.PlanName = "HUMANA";
            EncounterInfo.EncounterDetails.ReferringProvider = "PARIKSITH SINGH";
            EncounterInfo.EncounterDetails.BillingProvider = "PARIKSITH SINGH";
            EncounterInfo.EncounterDetails.ServiceFacility = "1094 NORTH CLIFFE DLVE";
            EncounterInfo.EncounterDetails.DOSFrom = new DateTime(2016, 10, 10);
            EncounterInfo.EncounterDetails.DOSTo = new DateTime(2016, 10, 11);
            EncounterInfo.EncounterDetails.PlaceOfService = "21-INPATIENT HOSPITAL";
            EncounterInfo.EncounterDetails.RenderingProviderFirstName = "PARIKSITH";
            EncounterInfo.EncounterDetails.RenderingProviderLastOrOrganizationName = "SINGH";
            EncounterInfo.EncounterDetails.RenderingProviderMiddleName = "";
            EncounterInfo.EncounterDetails.RenderingProviderNPI = "1417989625";
            EncounterInfo.EncounterDetails.RenderingProviderSpeciality = "INTERNAL MEDICINE";
            EncounterInfo.EncounterDetails.RenderingProviderTaxonomy = "207R00000X";
            EncounterInfo.EncounterDetails.SubscriberID = "K277057937";
            EncounterInfo.EncounterDetails.VisitLength = "30 MINUTES";
            EncounterInfo.EncounterDetails.VisitReason = "MVA";

            List<CategoryViewModel> CategoryDetails = new List<CategoryViewModel>();
            CategoryViewModel category1 = new CategoryViewModel { CategoryName = "HISTORY OF PRESENT ILLNESS", Remarks = "PATIENT HAS HISTORY OF ILLNESS", CategoryCode = "001", selected = true };
            CategoryViewModel category2 = new CategoryViewModel { CategoryName = "CHIEF COMPLAINT", Remarks = "THERE IS NO CHEIF COMPLAINT AVAILABLE", CategoryCode = "000", selected = true };
            CategoryViewModel category3 = new CategoryViewModel { CategoryName = "HISTORY REFERENCES", Remarks = "THERE ARE NO REFERENCES FOR PATIENT HISTORY", CategoryCode = "004", selected = true };
            CategoryDetails.Add(category1);
            CategoryDetails.Add(category2);
            CategoryDetails.Add(category3);

            EncounterInfo.EncounterDecision.Categories = CategoryDetails;
            EncounterInfo.EncounterDecision.IsAgree = true;

            ViewCodingDetails.EncounterDetails = EncounterInfo;

            CodingDetailsViewModel CodingInfo = new CodingDetailsViewModel();

            //*****************ICD CODES***************//
            ICDCodeDetailsViewModel ICDCodeInfo = new ICDCodeDetailsViewModel();
            List<HCCCodeViewModel> HccCodeList1 = new List<HCCCodeViewModel>();
            List<HCCCodeViewModel> HccCodeList2 = new List<HCCCodeViewModel>();
            HCCCodeViewModel HccCode1 = new HCCCodeViewModel();
            HCCCodeViewModel HccCode2 = new HCCCodeViewModel();
            HccCode1.Code = "23";
            HccCode1.Description = "Septicemia/Shock";
            HccCode1.Type = "Medical";
            HccCode1.Version = "v22";
            HccCode1.Weight = "0.2";

            HccCode2.Code = "23";
            HccCode2.Description = "Septicemia/Shock";
            HccCode2.Type = "Medical";
            HccCode2.Version = "v22";
            HccCode2.Weight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);

            HccCodeList2.Add(HccCode2);

            List<ICDCodeViewModel> ICDCodeLists = new List<ICDCodeViewModel>();
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeInfo.ICDCodes = ICDCodeLists;

            //****************CPT CODES******************//
            CPTCodeDetailsViewModel CPTCodeInfo = new CPTCodeDetailsViewModel();
            List<ICDCPTCodemappingViewModel> CPTCodeLists = new List<ICDCPTCodemappingViewModel>();
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77852", Description = "CELL ENEMERATION ID", Modifier1 = "10", Modifier2 = "15", Modifier3 = "11", Modifier4 = "12", DiagnosisPointer1 = "A207", DiagnosisPointer2 = "C204", DiagnosisPointer3 = "B407", DiagnosisPointer4 = "A206", Fee = 35.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77853", Description = "CELL ENUMERATION PHYS THERAPY", Modifier1 = "8", Modifier2 = "25", Modifier3 = "51", Modifier4 = "12", DiagnosisPointer1 = "A208", DiagnosisPointer2 = "C205", DiagnosisPointer3 = "B408", DiagnosisPointer4 = "A209", Fee = 32.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77854", Description = "AUTOLOGOUS BLOOD PROCESS", Modifier1 = "9", Modifier2 = "20", Modifier3 = "19", Modifier4 = "19", DiagnosisPointer1 = "A209", DiagnosisPointer2 = "C206", DiagnosisPointer3 = "B409", DiagnosisPointer4 = "A208", Fee = 45.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "99201", Description = "", Fee = 75.66, isEnM = true });
            CPTCodeInfo.CPTCodes = CPTCodeLists;

            CodingInfo.ICDCodeDetails = ICDCodeInfo;
            CodingInfo.CPTCodeDetails = CPTCodeInfo;

            ViewCodingDetails.CodingDetails = CodingInfo;

            DocumentListViewModel documentList = new DocumentListViewModel();

            DocumentListViewModel ProgressNote = new DocumentListViewModel();
            DocumentListViewModel LabReport = new DocumentListViewModel();
            DocumentListViewModel Sonogram = new DocumentListViewModel();
            DocumentListViewModel DocHistoryLabReport = new DocumentListViewModel();

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

            ViewCodingDetails.EncounterDetails.EncounterDetails.DocumentDetails = documentList;
            List<CodingNotesViewModel> CodingNotes = new List<CodingNotesViewModel>();
            CodingNotes.Add(new CodingNotesViewModel { Title = "Notes", Description = "This patient is illness", Category = "NotesCategory", Module = "Encounter", Notify = "Maccarthy", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Patient History", Description = "This Encounter has to be referenced for future purpose", Category = "NotesCategory", Module = "Encounter", Notify = "Swathi", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Illness", Description = "Coding Details has to be update", Category = "NotesCategory", Module = "Coding", Notify = "Sharanya", AddedBy = "Maccarthy", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Reports", Description = "Auditing Information has to be updated", Category = "NotesCategory", Module = "Auditing", Notify = "Amoore", AddedBy = "Finch", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Documents", Description = "Documents has to be updated", Category = "NotesCategory", Module = "Coding", Notify = "Maccarthy", AddedBy = "Daneil", AddedOn = new DateTime(2015, 10, 10) });
            NotesViewModel CodingNotesInfo = new NotesViewModel();
            CodingNotesInfo.CodingNotes = CodingNotes;
            ViewCodingDetails.CodingDetails.Notes = CodingNotesInfo;

            List<EncounterNotesViewModel> EncounterNotes = new List<EncounterNotesViewModel>();
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Notes", Description = "This patient is illness", Category = "NotesCategory", Module = "Encounter", Notify = "Maccarthy", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Patient History", Description = "This Encounter has to be referenced for future purpose", Category = "NotesCategory", Module = "Encounter", Notify = "Swathi", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Illness", Description = "Coding Details has to be update", Category = "NotesCategory", Module = "Coding", Notify = "Sharanya", AddedBy = "Maccarthy", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Reports", Description = "Auditing Information has to be updated", Category = "NotesCategory", Module = "Auditing", Notify = "Amoore", AddedBy = "Finch", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Documents", Description = "Documents has to be updated", Category = "NotesCategory", Module = "Coding", Notify = "Maccarthy", AddedBy = "Daneil", AddedOn = new DateTime(2015, 10, 10) });

            ViewCodingDetails.EncounterDetails.EncounterDetails.EncounterNotes = EncounterNotes;
            return ViewCodingDetails;
        }

        #endregion

        #region Edit Coding
        public CreateCodingViewModel EditDetails()
        {
            CreateCodingViewModel EditCodingDetails = new CreateCodingViewModel();
            EncounterViewModel EncounterInfo = new EncounterViewModel();

            EncounterInfo.EncounterDetails.CheckInTime = "10:30 am";
            EncounterInfo.EncounterDetails.CheckOutTime = "11:30 am";
            EncounterInfo.EncounterDetails.DOSFrom = new DateTime(2016, 10, 09);
            EncounterInfo.EncounterDetails.DOSTo = new DateTime(2016, 10, 10);
            EncounterInfo.EncounterDetails.EncounterId = "116244951";
            //EncounterInfo.EncounterDetails.EncounterNotes = "PROGRESS NOTES NOT ATTACHED";
            EncounterInfo.EncounterDetails.EncounterStatus = "OPEN";
            EncounterInfo.EncounterDetails.EncounterType = "HMO";
            EncounterInfo.EncounterDetails.NextAppointmentDate = new DateTime(2016, 12, 12);
            EncounterInfo.EncounterDetails.PatientBirthDate = new DateTime(1985, 10, 10);
            EncounterInfo.EncounterDetails.PatientCity = "AMIDON";
            EncounterInfo.EncounterDetails.PatientFirstAddress = "";
            EncounterInfo.EncounterDetails.PatientFirstName = "SMITH";
            EncounterInfo.EncounterDetails.PatientGender = "MALE";
            EncounterInfo.EncounterDetails.PatientLastOrOrganizationName = "LUCAS";
            EncounterInfo.EncounterDetails.PatientMiddleName = "";
            EncounterInfo.EncounterDetails.PatientSecondAddress = "";
            EncounterInfo.EncounterDetails.PatientState = "FLORIDA";
            EncounterInfo.EncounterDetails.PatientZip = "100482";
            EncounterInfo.EncounterDetails.PlanName = "HUMANA";
            EncounterInfo.EncounterDetails.ReferringProvider = "PARIKSITH SINGH";
            EncounterInfo.EncounterDetails.BillingProvider = "PARIKSITH SINGH";
            EncounterInfo.EncounterDetails.ServiceFacility = "1094 NORTH CLIFFE DLVE";
            EncounterInfo.EncounterDetails.DOSFrom = new DateTime(2016, 10, 10);
            EncounterInfo.EncounterDetails.DOSTo = new DateTime(2016, 10, 11);
            EncounterInfo.EncounterDetails.PlaceOfService = "21-INPATIENT HOSPITAL";
            EncounterInfo.EncounterDetails.RenderingProviderFirstName = "PARIKSITH";
            EncounterInfo.EncounterDetails.RenderingProviderLastOrOrganizationName = "SINGH";
            EncounterInfo.EncounterDetails.RenderingProviderMiddleName = "";
            EncounterInfo.EncounterDetails.RenderingProviderNPI = "1417989625";
            EncounterInfo.EncounterDetails.RenderingProviderSpeciality = "INTERNAL MEDICINE";
            EncounterInfo.EncounterDetails.RenderingProviderTaxonomy = "207R00000X";
            EncounterInfo.EncounterDetails.SubscriberID = "K277057937";
            EncounterInfo.EncounterDetails.VisitLength = "30 MINUTES";
            EncounterInfo.EncounterDetails.VisitReason = "MVA";

            //List<CategoryViewModel> CategoryDetails = new List<CategoryViewModel>();
            //CategoryViewModel category1 = new CategoryViewModel { CategoryName = "HISTORY OF PRESENT ILLNESS", Remarks = "PATIENT HAS HISTORY OF ILLNESS", CategoryCode = "001", selected = true };
            //CategoryViewModel category2 = new CategoryViewModel { CategoryName = "CHIEF COMPLAINT", Remarks = "THERE IS NO CHEIF COMPLAINT AVAILABLE", CategoryCode = "000", selected = true };
            //CategoryViewModel category3 = new CategoryViewModel { CategoryName = "HISTORY REFERENCES", Remarks = "THERE ARE NO REFERENCES FOR PATIENT HISTORY", CategoryCode = "004", selected = true };
            //CategoryDetails.Add(category1);
            //CategoryDetails.Add(category2);
            //CategoryDetails.Add(category3);

            //EncounterInfo.EncounterDecision.Categories = CategoryDetails;
            EncounterInfo.EncounterDecision.IsAgree = false;

            EditCodingDetails.EncounterDetails = EncounterInfo;

            //*************Coding Details***************//
            CodingDetailsViewModel CodingInfo = new CodingDetailsViewModel();
            //*****************ICD CODES***************//
            ICDCodeDetailsViewModel ICDCodeInfo = new ICDCodeDetailsViewModel();
            List<HCCCodeViewModel> HccCodeList1 = new List<HCCCodeViewModel>();
            List<HCCCodeViewModel> HccCodeList2 = new List<HCCCodeViewModel>();
            HCCCodeViewModel HccCode1 = new HCCCodeViewModel();
            HCCCodeViewModel HccCode2 = new HCCCodeViewModel();
            HccCode1.Code = "23";
            HccCode1.Description = "Septicemia/Shock";
            HccCode1.Type = "Medical";
            HccCode1.Version = "v22";
            HccCode1.Weight = "0.2";

            HccCode2.Code = "23";
            HccCode2.Description = "Septicemia/Shock";
            HccCode2.Type = "Medical";
            HccCode2.Version = "v22";
            HccCode2.Weight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);

            HccCodeList2.Add(HccCode2);

            List<ICDCodeViewModel> ICDCodeLists = new List<ICDCodeViewModel>();
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1, IsChronic = true, ChronicCount = 2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = false });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 0 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "F207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "G207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "H207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });
            ICDCodeLists.Add(new ICDCodeViewModel { ICDCode = "I207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });
            ICDCodeInfo.ICDCodes = ICDCodeLists;
            ICDCodeInfo.IsICD10 = true;

            //****************CPT CODES******************//
            CPTCodeDetailsViewModel CPTCodeInfo = new CPTCodeDetailsViewModel();
            List<ICDCPTCodemappingViewModel> CPTCodeLists = new List<ICDCPTCodemappingViewModel>();
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77852", Description = "CELL ENEMERATION ID", Modifier1 = "10", Modifier2 = "15", Modifier3 = "11", Modifier4 = "12", DiagnosisPointer1 = "A207", DiagnosisPointer2 = "B207", DiagnosisPointer3 = "C207", DiagnosisPointer4 = "D207", Fee = 35.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77853", Description = "CELL ENUMERATION PHYS THERAPY", Modifier1 = "8", Modifier2 = "25", Modifier3 = "51", Modifier4 = "12", DiagnosisPointer1 = "A208", DiagnosisPointer2 = "C205", DiagnosisPointer3 = "B408", DiagnosisPointer4 = "A209", Fee = 32.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "77854", Description = "AUTOLOGOUS BLOOD PROCESS", Modifier1 = "9", Modifier2 = "20", Modifier3 = "19", Modifier4 = "19", DiagnosisPointer1 = "A209", DiagnosisPointer2 = "C206", DiagnosisPointer3 = "B409", DiagnosisPointer4 = "A208", Fee = 45.66 });
            CPTCodeLists.Add(new ICDCPTCodemappingViewModel { Code = "99201", Description = "", Fee = 75.66, isEnM = true });
            CPTCodeInfo.CPTCodes = CPTCodeLists;

            CodingInfo.ICDCodeDetails = ICDCodeInfo;
            CodingInfo.CPTCodeDetails = CPTCodeInfo;

            EditCodingDetails.CodingDetails = CodingInfo;
            DocumentListViewModel documentList = new DocumentListViewModel();

            DocumentListViewModel ProgressNote = new DocumentListViewModel();
            DocumentListViewModel LabReport = new DocumentListViewModel();
            DocumentListViewModel Sonogram = new DocumentListViewModel();
            DocumentListViewModel DocHistoryLabReport = new DocumentListViewModel();

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

            EditCodingDetails.EncounterDetails.EncounterDetails.DocumentDetails = documentList;

            List<CodingNotesViewModel> CodingNotes = new List<CodingNotesViewModel>();
            CodingNotes.Add(new CodingNotesViewModel { Title = "Notes", Description = "This patient is illness", Category = "NotesCategory", Module = "Encounter", Notify = "Maccarthy", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Patient History", Description = "This Encounter has to be referenced for future purpose", Category = "NotesCategory", Module = "Encounter", Notify = "Swathi", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Illness", Description = "Coding Details has to be update", Category = "NotesCategory", Module = "Coding", Notify = "Sharanya", AddedBy = "Maccarthy", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Reports", Description = "Auditing Information has to be updated", Category = "NotesCategory", Module = "Auditing", Notify = "Amoore", AddedBy = "Finch", AddedOn = new DateTime(2015, 10, 10) });
            CodingNotes.Add(new CodingNotesViewModel { Title = "Documents", Description = "Documents has to be updated", Category = "NotesCategory", Module = "Coding", Notify = "Maccarthy", AddedBy = "Daneil", AddedOn = new DateTime(2015, 10, 10) });
            NotesViewModel CodingNotesInfo = new NotesViewModel();
            CodingNotesInfo.CodingNotes = CodingNotes;
            EditCodingDetails.CodingDetails.Notes = CodingNotesInfo;

            List<EncounterNotesViewModel> EncounterNotes = new List<EncounterNotesViewModel>();
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Notes", Description = "This patient is illness", Category = "NotesCategory", Module = "Encounter", Notify = "Maccarthy", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Patient History", Description = "This Encounter has to be referenced for future purpose", Category = "NotesCategory", Module = "Encounter", Notify = "Swathi", AddedBy = "AMoore", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Illness", Description = "Coding Details has to be update", Category = "NotesCategory", Module = "Coding", Notify = "Sharanya", AddedBy = "Maccarthy", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Reports", Description = "Auditing Information has to be updated", Category = "NotesCategory", Module = "Auditing", Notify = "Amoore", AddedBy = "Finch", AddedOn = new DateTime(2015, 10, 10) });
            EncounterNotes.Add(new EncounterNotesViewModel { Title = "Documents", Description = "Documents has to be updated", Category = "NotesCategory", Module = "Coding", Notify = "Maccarthy", AddedBy = "Daneil", AddedOn = new DateTime(2015, 10, 10) });

            EditCodingDetails.EncounterDetails.EncounterDetails.EncounterNotes = EncounterNotes;

            return EditCodingDetails;
        }
        #endregion
        public async Task<bool> SaveCoding(SaveCreateCodingDTO SaveCoding)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/coding/SaveCodedEncounter", SaveCoding);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                }
                return response.IsSuccessStatusCode;
            }
            catch(Exception e){
                throw;
            }
            
        }
        //******************Get the list of Categories*******************//
        #region Categories List
        public List<CategoryViewModel> GetAllCategories()
        {
            List<CategoryViewModel> AllCategories = new List<CategoryViewModel>();
            AllCategories.Add(new CategoryViewModel { CategoryCode = "000", CategoryName = "Chief Complaint" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "001", CategoryName = "History of present illness" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "002", CategoryName = "Review of Systems" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "003", CategoryName = "Past, Family and/or Social History" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "004", CategoryName = "History References" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "005", CategoryName = "Examination" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "006", CategoryName = "Medical Decision Making Level" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "007", CategoryName = "Modifier" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "008", CategoryName = "Incomplete Notes" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "009", CategoryName = "Missing Signature" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "100", CategoryName = "No E&amp;M Code" });
            AllCategories.Add(new CategoryViewModel { CategoryCode = "101", CategoryName = "Others" });
            return AllCategories;
        }
        #endregion
        //******************Get the list of Categories*******************//

        public List<ICDCodeViewModel> GetICDCodeList()
        {
            List<ICDCodeViewModel> ICDCodeList = new List<ICDCodeViewModel>();
            List<HCCCodeViewModel> HccCodeList1 = new List<HCCCodeViewModel>();
            List<HCCCodeViewModel> HccCodeList2 = new List<HCCCodeViewModel>();
            HCCCodeViewModel HccCode1 = new HCCCodeViewModel();
            HCCCodeViewModel HccCode2 = new HCCCodeViewModel();
            HccCode1.Code = "23";
            HccCode1.Description = "Septicemia/Shock";
            HccCode1.Type = "Medical";
            HccCode1.Version = "v22";
            HccCode1.Weight = "0.2";

            HccCode2.Code = "23";
            HccCode2.Description = "Septicemia/Shock";
            HccCode2.Type = "Medical";
            HccCode2.Version = "v22";
            HccCode2.Weight = "0.2";

            HccCodeList1.Add(HccCode1);
            HccCodeList1.Add(HccCode2);

            HccCodeList2.Add(HccCode2);
            ICDCodeList.Add(new ICDCodeViewModel { ICDCode = "A207", Description = "Septicemic Plague", HCCCodes = HccCodeList1, IsChronic = true, ChronicCount = 2 });
            ICDCodeList.Add(new ICDCodeViewModel { ICDCode = "B207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = false });
            ICDCodeList.Add(new ICDCodeViewModel { ICDCode = "C207", Description = "Septicemic Plague", HCCCodes = HccCodeList2 });
            ICDCodeList.Add(new ICDCodeViewModel { ICDCode = "D207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 0 });
            ICDCodeList.Add(new ICDCodeViewModel { ICDCode = "E207", Description = "Septicemic Plague", HCCCodes = HccCodeList2, IsChronic = true, ChronicCount = 1 });

            return ICDCodeList;
        }

       
        public void GetActiveDiagnosisBySearch(object SearchObject)
        {
            throw new NotImplementedException();
        }

        public void GetActiveProceduresBySearch(object SearchObject)
        {
            throw new NotImplementedException();
        }

        public void GetEncountersReportAsPDF(object PDFOptions)
        {
            throw new NotImplementedException();
        }

        public void GetEncountersReportAsExcel(object ExcelOptions)
        {
            throw new NotImplementedException();
        }

        public void GetReadyToCodeEncountersOnSearch(object SerachObject)
        {
            throw new NotImplementedException();
        }

        public void DeActivateCodedEncounter(int CodedEncounterId)
        {
            throw new NotImplementedException();
        }

        public void UploadDocument(object FileDetails)
        {
            throw new NotImplementedException();
        }

        public void GetFeeSchedules()
        {
            throw new NotImplementedException();
        }

        public void GetHCCCodes()
        {
            throw new NotImplementedException();
        }

        public void GetICDCodesOnSearch()
        {
            throw new NotImplementedException();
        }

        public void GetCPTCodesOnSearch()
        {
            throw new NotImplementedException();
        }
    }
}