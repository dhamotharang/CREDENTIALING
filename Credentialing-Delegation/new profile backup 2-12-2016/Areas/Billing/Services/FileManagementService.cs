using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Models.File_Management;
using PortalTemplate.Areas.Billing.Services.IServices;
using PortalTemplate.Areas.Billing.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Billing.Services
{
    public class FileManagementService : IFileManagementService
    {
        readonly IPowerDriveService _PowerDriveService;

        public FileManagementService()
        {
            _PowerDriveService = new PowerDriveService();
        }

        public List<File837ViewModel> Get837TableList()
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File837ViewModel>>("api/FileReports/GetFileList?Index=0&ConfigSt=File_Input_837&SortingColumn=default&SortOrder=true", null)).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<File837ViewModel> Get837TableByIndex(int index, bool sortingType, string sortBy, File837ViewModel SearchObject)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File837ViewModel>>("api/FileReports/GetFileListSearch?Index=" + index + "&ConfigSt=File_Input_837_Search&SortingColumn=" + sortBy + "&SortOrder=" + sortingType, SearchObject)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<File835> Get835TableList()
        {

            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File835>>("api/FileReports/GetEOBFileList?Index=0&ConfigSt=File_Input_835&SortingColumn=default&SortOrder=true", null)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<File835> Get835TableByIndex(int index, bool sortingType, string sortBy, File835 SearchObject)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File835>>("api/FileReports/GetEOBFileListSearch?Index=" + index + "&ConfigSt=File_Input_835_Search&SortingColumn=" + sortBy + "&SortOrder=" + sortingType, SearchObject)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<File277ViewModel> Get277FileList()
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File277ViewModel>>("api/FileReports/GetFileList?Index=0&ConfigSt=File_Input_277&SortingColumn=default&SortOrder=true", null)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<File277ViewModel> Get277TableByIndex(int index, bool sortingType, string sortBy, File277ViewModel SearchObject)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File277ViewModel>>("api/FileReports/GetFileListSearch?Index=" + index + "&ConfigSt=File_Input_277_Search&SortingColumn=" + sortBy + "&SortOrder=" + sortingType, SearchObject)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public List<File999ViewModel> Get999FileList()
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File999ViewModel>>("api/FileReports/GetFileList?Index=0&ConfigSt=File_Input_999&SortingColumn=default&SortOrder=true", null)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<File999ViewModel> Get999TableByIndex(int index, bool sortingType, string sortBy, File999ViewModel SearchObject)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File999ViewModel>>("api/FileReports/GetFileListSearch?Index=" + index + "&ConfigSt=File_Input_999_Search&SortingColumn=" + sortBy + "&SortOrder=" + sortingType, SearchObject)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClaimList> GetClaimList(string IncomeFileLoggerID)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<ClaimList>>("api/ClaimReport/GetClaimListById?Index=0&ConfigSt=Claim_Input_837_File&IncomingFileId=" + IncomeFileLoggerID + "&SortingColumn=default&SortOrder=true", null)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<ClaimList> Get837ClaimListTableListByIndex(int index, bool sortingType, string sortBy, ClaimList SearchObject, string IncomingFileId)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<ClaimList>>("api/ClaimReport/GetClaimListSearch?Index=" + index + "&ConfigSt=Claim_Input_837_Search&IncomingFileId=" + IncomingFileId + "&SortingColumn=" + sortBy + "&SortOrder=" + sortingType, SearchObject)).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<File835ProviderInfo> Get835ProviderList(int InterKey, string CheckNumber)
        {
            //List<File835ProviderInfo> File835ProviderInfoLists = new List<File835ProviderInfo>();

            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 1, NPI = "4578964784", FirstName = "SANDY", LastName = "AANONSEN", MiddleName = "", NumberOfClaims = "12", TotalClaimed = "554.00", TotalReceived = "554.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 2, NPI = "2457588887", FirstName = "RUBY", LastName = "ADAM", MiddleName = "", NumberOfClaims = "10", TotalClaimed = "254.00", TotalReceived = "10.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 3, NPI = "5745763188", FirstName = "LORENE", LastName = "JHONY", MiddleName = "", NumberOfClaims = "11", TotalClaimed = "457.00", TotalReceived = "142.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 4, NPI = "5727891015", FirstName = "LEONE", LastName = "ROBART", MiddleName = "", NumberOfClaims = "8", TotalClaimed = "124.00", TotalReceived = "123.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 5, NPI = "1420478552", FirstName = "LISA", LastName = "AARON", MiddleName = "", NumberOfClaims = "9", TotalClaimed = "620.00", TotalReceived = "524.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 6, NPI = "1458700545", FirstName = "TANIYA", LastName = "PAUL", MiddleName = "", NumberOfClaims = "1", TotalClaimed = "452.00", TotalReceived = "365.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 7, NPI = "1457105477", FirstName = "PEARL", LastName = "NICOLE", MiddleName = "", NumberOfClaims = "5", TotalClaimed = "224.00", TotalReceived = "224.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 8, NPI = "1577730257", FirstName = "SHIRLEY", LastName = "NEWMAN", MiddleName = "", NumberOfClaims = "23", TotalClaimed = "325.00", TotalReceived = "325.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 9, NPI = "7420014567", FirstName = "SMITH", LastName = "JEAMS", MiddleName = "", NumberOfClaims = "13", TotalClaimed = "153.00", TotalReceived = "144.00" });
            //File835ProviderInfoLists.Add(new File835ProviderInfo { ProviderID = 10, NPI = "2274520477", FirstName = "FORD", LastName = "KARLA", MiddleName = "", NumberOfClaims = "10", TotalClaimed = "214.00", TotalReceived = "198.00" });

            //return File835ProviderInfoLists;



            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File835ProviderInfo>>("api/EOBReport/GetProvidersInFile?InterKey=" + InterKey + "&CheckNumber=" + CheckNumber + "&ConfigString=835_Provider", null)).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<File835EOBList> Get835EobList(int InterKey, string HeaderKey, string NPI)
        {

            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<List<File835EOBList>>("api/EOBReport/GetEOBREport?InterKey=" + InterKey + "&HeaderKey=" + HeaderKey + "&NPI=" + NPI + "&ConfigString=835_Provider_EOBreport ", null)).Result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Cms1500ViewModels GetCms1500Form(int ClaimId)
        {
            Cms1500ViewModels Cms1500Form = new Cms1500ViewModels();

            Cms1500Form.PatientLastOrOrganizationName = "KJELL";
            Cms1500Form.PatientMiddleName = "";
            Cms1500Form.PatientFirstName = "AANONSEN";
            Cms1500Form.PatientBirthDate = new DateTime(1935, 04, 26);
            Cms1500Form.PatientFirstAddress = "6046";
            Cms1500Form.PatientSecondAddress = "NEWMARK ST";



            Cms1500Form.PayerName = "FREEDOM HEALTH INS";
            Cms1500Form.PayerFirstAddress = "P.O. BOX 151348";
            Cms1500Form.PayerSecondAddress = "SPRING HILL";
            Cms1500Form.PayerState = "AA";
            Cms1500Form.PayerCity = "TEMPA";
            Cms1500Form.PayerID = "41212";
            Cms1500Form.PayerZip = "336840401";

            Cms1500Form.BillingGroupNumber = "1851689947";
            Cms1500Form.BillingProviderLastOrOrganizationName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.BillingProviderPhoneNo = "587977444";
            Cms1500Form.BillingProviderZip = "346098102";
            Cms1500Form.BillingProviderTaxonomy = "207RG0300X";
            Cms1500Form.BillingProviderCity = "SPRING HILL";
            Cms1500Form.BillingProviderState = "FL";
            Cms1500Form.BillingProviderFirstAddress = "14690 SPRING HILL DR";
            Cms1500Form.ReferringProviderLastName = "BENSON";
            Cms1500Form.RenderingProviderFirstName = "DALTON";
            Cms1500Form.RenderingProviderTaxonomy = "207RG0300X";
            Cms1500Form.RenderingProviderNPI = "4125412451";
            Cms1500Form.ReferringProviderIdentifier = "1164477618";

            Cms1500Form.FacilityName = "ACCESS 2 HEALTHCARE";
            Cms1500Form.FacilityAddress1 = "1903 W HIGHWAY 44";
            Cms1500Form.FacilityCity = "INVERNESS";
            Cms1500Form.FacilityState = "FL";
            Cms1500Form.FacilityZip = "344533801";
            Cms1500Form.FacilityIdentifier1 = "1851689947";
            Cms1500Form.FacilityIdentifier2 = "454545544";

            Cms1500Form.PatientAccountNumber = "1851689947";
            Cms1500Form.SubscriberFirstAddress = "1230 SILVERTHORN LOOP";
            Cms1500Form.SubscriberCity = "HERNANDO";
            Cms1500Form.SubscriberState = "FL";
            Cms1500Form.SubscriberZip = "34442";
            Cms1500Form.SubscriberPhoneNo = "3524650375";

            Cms1500Form.CurrentServiceFrom = new DateTime(1935, 04, 26);
            Cms1500Form.CurrentServiceTo = new DateTime(1935, 04, 26);
            Cms1500Form.ClaimsNatureOfIllness1 = "V11.5XXA";
            Cms1500Form.PlaceOfService = "11";
            //----------------service line-----------------------------


            List<ServiceLineViewModels> serviceLines = new List<ServiceLineViewModels>();
            serviceLines.Add(new ServiceLineViewModels { claimsProcedure = "99214", Modifier1 = "11", UnitCharges = 0.3, Unit = 1, DiagnosisPointer1 = "1" });
            Cms1500Form.ServiceLines = serviceLines;


            return Cms1500Form;
        }



        public async Task<Stream> DownloadFile(string Path, Models.PowerDriveService.UserInfo User)
        {
            try
            {
                return await _PowerDriveService.DownLoadFile(Path, User);


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}