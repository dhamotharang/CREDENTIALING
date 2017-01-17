using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.Billing.Models.File_Management;
using PortalTemplate.Areas.Billing.Models.CreateClaim;
using PortalTemplate.Areas.Billing.Services;
using PortalTemplate.Areas.Billing.Services.IServices;
using PortalTemplate.Models.PratianComponents;
using PortalTemplate.Areas.Billing.WebApi;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using System.Web.UI;
using PortalTemplate.Areas.Billing.Models.PowerDriveService;
using Newtonsoft.Json;
using PratAh.Component.Queue;
using System.Configuration;
using System.IO;
namespace PortalTemplate.Areas.Billing.Controllers
{
    public class FileManagementController : Controller
    {
        //
        // GET: /FileManagement/
        readonly IFileManagementService _FileManagementService;
        readonly IPowerDriveService _PowerDriveService;

        public FileManagementController()
        {
            _FileManagementService = new FileManagementService();
            _PowerDriveService = new PowerDriveService();
        }

        public ActionResult Index()
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/Index.cshtml");
        }

        public ActionResult ShowFileUploadForm()
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_FileUploadForm.cshtml");
        }

        public void UploadFile(FileUpload upload)
        {
            if (ModelState.IsValid)
            {
            FileService fileService = new FileService();
            fileService.UserInfo = new UserInfo { UserName = ConfigurationManager.AppSettings["RegisterUserName"], ApplicaionOrGroupName = ConfigurationManager.AppSettings["ApplicationOrGroupName"], Token = "" };
            fileService.DocumentAndStreams = new List<DocumentAndStream>();
            foreach (var file in upload.EdiFile)
            {
                if (file != null)
                {
                    DocumentAndStream DocumentAndStream = new DocumentAndStream { Document = new Document { FileName = file.FileName, Path = "Translation\\837", TransferType = TransferType.Stream }, InputStream = file.InputStream };
                    fileService.DocumentAndStreams.Add(DocumentAndStream);
                }

            }
            try
            {
                FileUploadResponse response = _PowerDriveService.UploadFileService(fileService);
                Result result = new Result();
                
                if (response.FileInfomations.Count() > 0)
                {
                    foreach (var file in response.FileInfomations)
                    {
                       PortalTemplate.Areas.Billing.Models.File_Management.FileInfo fileInfo = new PortalTemplate.Areas.Billing.Models.File_Management.FileInfo();
                       fileInfo.AccountName = "AccessHealthCareLLC";
                       fileInfo.UserName = User.Identity.Name;
                       fileInfo.Source = upload.InputSource;
                       fileInfo.ClaimType = upload.FileType;
                       fileInfo.RegisterUserName = ConfigurationManager.AppSettings["RegisterUserName"];
                       fileInfo.ApplicationOrGroupName = ConfigurationManager.AppSettings["ApplicationOrGroupName"];
                       fileInfo.FileKey = file.FileKey;
                       fileInfo.FileName = file.FileName;
                       fileInfo.UploadedDate = file.UploadedDate;
                       string fileresponse = JsonConvert.SerializeObject(fileInfo);

                       QueueManager.Enqueue(fileresponse, "EDITranslation");

                    }


                }

                


               
            }
            catch (Exception e)
            {

            }
        }
            else
            {
               
            }
        }

        public async Task<bool> DownloadFile(string FileKey, Models.PowerDriveService.UserInfo User, string FileName)
        {
            User.UserName = User.RegisterUserName;
            Stream fs = await _FileManagementService.DownloadFile(FileKey, User);
            //byte[] btFile = new byte[fs.Length];
            System.Web.HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=" + FileName);
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";





            byte[] fileBytes = null;
            byte[] buffer = new byte[fs.Length];
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            int chunkSize = 0;
            do
        {
                chunkSize = fs.Read(buffer, 0, buffer.Length);
                memoryStream.Write(buffer, 0, chunkSize);
            } while (chunkSize != 0);
            fileBytes = memoryStream.ToArray();

            //System.Web.HttpContext.Current.Response.Write(fileBytes);
            System.Web.HttpContext.Current.Response.BinaryWrite(fileBytes);
            System.Web.HttpContext.Current.Response.End();
            return true;
        }




        #region FIle - 837

        public ActionResult Get837TableList()
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_837ListTable.cshtml", _FileManagementService.Get837TableList());
        }


        [OutputCache(Duration = 100, Location = OutputCacheLocation.Server, VaryByParam = "*")]
        public ActionResult Get837TableListByIndex(int index, string CountOfList, string sortingType, string sortBy, File837ViewModel SearchObject, string ReceivedList837, string DispatchedList837)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_TBody837ListTable.cshtml", _FileManagementService.Get837TableByIndex(index, sortingType == "asc" ? true : sortingType == null ? true : false, sortBy == null ? "default" : sortBy, SearchObject));

        }

        public ActionResult GetClaimList(string IncomeFileLoggerID)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_ClaimList.cshtml", _FileManagementService.GetClaimList(IncomeFileLoggerID));
        }


        public ActionResult Get837ClaimListTableListByIndex(int index, string sortingType, string sortBy, ClaimList SearchObject, string IncomingFileId)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_TBody837ClaimList.cshtml", _FileManagementService.Get837ClaimListTableListByIndex(index, sortingType == "asc" ? true : sortingType == null ? true : false, sortBy == null ? "default" : sortBy, SearchObject, IncomingFileId));
        }

        #endregion

        #region File - 999

        public ActionResult Get999FileList()
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_999FileList.cshtml", _FileManagementService.Get999FileList());
        }


        public ActionResult Get999TableListByIndex(int index, string sortingType, string sortBy, File999ViewModel SearchObject, string ReceivedList999, string DispatchedList999)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_TBody999FileList.cshtml", _FileManagementService.Get999TableByIndex(index, sortingType == "asc" ? true : sortingType == null ? true : false, sortBy == null ? "default" : sortBy, SearchObject));
        }

        #endregion

        #region File - 277

        public ActionResult Get277FileList()
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_277FileList.cshtml", _FileManagementService.Get277FileList());
        }


        public ActionResult Get277TableListByIndex(int index, string sortingType, string sortBy, File277ViewModel SearchObject, string ClaimAkg277, string ClaimPending277)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_TBody277FileList.cshtml", _FileManagementService.Get277TableByIndex(index, sortingType == "asc" ? true : sortingType == null ? true : false, sortBy == null ? "default" : sortBy, SearchObject));
        }

        #endregion

        #region File - 835

        public ActionResult Get835FileList()
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_835FileListTable.cshtml", _FileManagementService.Get835TableList());
        }

        public ActionResult Get835TableListByIndex(int index, string sortingType, string sortBy, File835 SearchObject, string Received835, string Generated835)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_TBody835ListTable.cshtml", _FileManagementService.Get835TableByIndex(index, sortingType == "asc" ? true : sortingType == null ? true : false, sortBy == null ? "default" : sortBy, SearchObject));
        }

        public ActionResult Get835ProviderList(int InterKey, string CheckNumber)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_835ProviderList.cshtml", _FileManagementService.Get835ProviderList(InterKey, CheckNumber));
        }

        public ActionResult Get835ProviderListByIndex(int InterKey, string CheckNumber)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_TBody835ProviderList.cshtml", _FileManagementService.Get835ProviderList(InterKey, CheckNumber));
        }

        public ActionResult Get835EobList(int InterKey, string HeaderKey, string NPI)
        {
            return PartialView("~/Areas/Billing/Views/FileManagement/_835EOBList.cshtml", _FileManagementService.Get835EobList(InterKey, HeaderKey, NPI));
        }

        public void Generate835(GenerateProvider GenerateProvider)
        {
            try
            {
                string GenerationObj = JsonConvert.SerializeObject(GenerateProvider);
                QueueManager.Enqueue(GenerationObj, "EDIGeneration");       
            }
            catch (Exception)
            {
        
                throw;
            }
            
        }
          
        #endregion


        public ActionResult GetCms1500Form(int ClaimId)
           {
            return PartialView("~/Areas/Billing/Views/FileManagement/_EditCMS1500From.cshtml", _FileManagementService.GetCms1500Form(ClaimId));
        }


    }
}