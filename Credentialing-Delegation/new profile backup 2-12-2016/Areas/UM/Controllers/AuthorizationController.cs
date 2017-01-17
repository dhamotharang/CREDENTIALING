using  Newtonsoft.Json;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using PortalTemplate.Areas.UM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.PowerDriveService;
using PortalTemplate.Areas.Portal.Services.ICD;
using PortalTemplate.Areas.UM.Models.MasterDataEntities;
using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using AutoMapper;
using PortalTemplate.Areas.Portal.Services.CPT;
using PortalTemplate.Areas.UM.Services.MasterData;
using PortalTemplate.Areas.Portal.Manager.Manager;
using PortalTemplate.Areas.Portal.IManager.Member;
using PortalTemplate.Areas.Portal.Models.MemberManager;
using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.CalypsoAI;
using PortalTemplate.Areas.UM.Services.Authorization;
using PortalTemplate.Areas.UM.Services.CalypsoAI;
using System.Threading.Tasks;
using PortalTemplate.Areas.Portal.IManager.ICD;
using PortalTemplate.Areas.Portal.IManager.CPT;

namespace PortalTemplate.Areas.UM.Controllers
{
    public class AuthorizationController : Controller
    {
        public AuthorizationController()
        {
            _powerDriveService = new PowerDriveService();
            masterDataService = new CMSService();
            authServices = new AuthorizationService();
        }

        readonly CMSService realservice = new CMSService();
        readonly CalypsoAIService calypsoService = new CalypsoAIService();
        readonly MasterDataService service = new MasterDataService();
        IPowerDriveService _powerDriveService;
        IMemberServiceManager profileManager = new MemberServiceManager();
        IMasterDataService masterDataService;
        PortalTemplate.Areas.UM.IServices.Authorization.IAuthorizationService authServices;

        public ActionResult Index(string SubscriberID)
        {
           // SetAllMasterDataFromService("CreateNewAuth");
            SetAllMasterData("CreateNewAuth");
            ViewData["MasterUMSvcGrps"] = realservice.GetUMServiceGroupByPOS(1);
            //MemberManagerModel memberManager = profileManager.GetMemberDetailsBySubsriberID(SubscriberID);
            //PortalTemplate.Areas.Portal.Models.Member.MemberViewModel member = (PortalTemplate.Areas.Portal.Models.Member.MemberViewModel)memberManager.ResultObject;

            IMemberViewService memberService = new PortalTemplate.Areas.Portal.Services.Member.MemberService();
            MemberViewModel member = memberService.GetMemberInfoBySubscriberID(SubscriberID);

            ViewBag.MemberData = member;
            return PartialView("~/Areas/UM/Views/Authorization/_CreateNewAuthFax.cshtml");
        }

        [HttpPost]
        public PartialViewResult POSAreaSelector(AuthorizationViewModel auth)
        {
            ViewBag.MemberData = auth.Member;
            //SetAllMasterDataFromService("CreateNewAuth");
            SetAllMasterData("CreateNewAuth");
            var pos = auth.PlaceOfService;
            var posNo = pos.Split('-')[0];
            ViewData["MasterUMSvcGrps"] = realservice.GetUMServiceGroupByPOS(authServices.GetPOSID(pos));
            return PartialView("~/Areas/UM/Views/Authorization/POS/_POSProviderFurtherDetailsAreaPOS" + posNo + ".cshtml", auth);
        }

        [HttpPost]
        public PartialViewResult OPTypeChange(AuthorizationViewModel auth)
        {
            ViewBag.MemberData = auth.Member;
            SetAllMasterData("CreateNewAuth");
            var pos = auth.PlaceOfService;
            var posNo = pos.Split('-')[0];
            if (auth.OutPatientType != null && (auth.OutPatientType.ToUpper() == "OP OBSERVATION" || auth.OutPatientType.ToUpper() == "OP IN A BED"))
                return PartialView("~/Areas/UM/Views/Authorization/POS/_POSProviderFurtherDetailsAreaPOS" + posNo + "OPOBS.cshtml", auth);
            else
                return PartialView("~/Areas/UM/Views/Authorization/POS/_POSProviderFurtherDetailsAreaPOS" + posNo + ".cshtml", auth);
        }

        [HttpPost]
        public ActionResult Authorization(AuthorizationViewModel CreateNewAuth)
        {
            try
            {
                //CreateNewAuth.LOSs.AddRange(LOS);

                // -----For validations ------------
                //----------Use setmasterdata(); and the partial to be loaded with the view model -----------

                //// ------- This is for Power Drive - Should be moved later while referring an auth-----
                //FileService fileService = new FileService();
                //fileService.UserInfo = new UserInfo { UserName = "testprev@gmail.com", Path = "", ApplicaionOrGroupName = "TestPrev", Token = "" };
                //fileService.DocumentAndStreams = new List<DocumentAndStream>();
                //for (int i = 0; i < CreateNewAuth.Attachments.Count; i++)
                //{
                //    if (CreateNewAuth.Attachments[i].DocumentFile != null)
                //    {
                //        DocumentAndStream DocumentAndStream = new DocumentAndStream { Document = new Document { FileName = CreateNewAuth.Attachments[i].DocumentFile.FileName, TransferType = TransferType.Stream, Extension = CreateNewAuth.Attachments[i].Name }, InputStream = CreateNewAuth.Attachments[i].DocumentFile.InputStream };
                //        fileService.DocumentAndStreams.Add(DocumentAndStream);
                //    }

                //}
                //FileUploadResponse response = _powerDriveService.UploadFileService(fileService);
                //// ---- Place the response key in attachment view model ------
                //if (response.FileInfomations.Count() > 0)
                //{
                //    for (int i = 0; i < response.FileInfomations.Count(); i++)
                //    {
                //        CreateNewAuth.Attachments[i].DocKey = response.FileInfomations[i].FileKey;

                //    }
                //}
    
                // ------------ Setting Authorization Preview
                PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview.AuthPreviewViewModal AuthPreview = new PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview.AuthPreviewViewModal();
                AuthorizationPreviewController preview = new AuthorizationPreviewController();
           //     AuthPreview = preview.SetAuthPreviewData(CreateNewAuth);
             //   ViewBag.AuthPreviewContent = AuthPreview.Content;
               return PartialView("~/Areas/UM/Views/Common/AuthPreview/_AuthPreviewBody", AuthPreview);

            }
            catch (Exception)
            {
                return null;
            }
        }

       // List<LengthOfStayViewModel> LOS = new List<LengthOfStayViewModel>();
        [HttpPost]
        public AuthorizationViewModel SaveNegFee(AuthorizationViewModel CreateAuth)
        {
            
             //LOS.AddRange(CreateAuth.LOSs);
             return CreateAuth;
        }

        private void SetAllMasterData(string key)
        {
            var MasterData = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/UM/Views/web.config").AppSettings.Settings[key].Value.Split(',');
            foreach (string md in MasterData)
            {
                var method = md.Split(':')[0];
                var variable = md.Split(':')[1];
                ViewData[variable] = typeof(MasterDataService).GetMethod(method).Invoke(service, new object[0]);
            }
        }

        private void SetAllMasterDataFromService(string key)
        {
            var MasterData = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/UM/Views/web.config").AppSettings.Settings[key].Value.Split(',');
            foreach (string md in MasterData)
            {
                var method = md.Split(':')[0];
                var variable = md.Split(':')[1];
                ViewData[variable] = typeof(CMSService).GetMethod(method).Invoke(masterDataService, new object[0]);
            }
        }

        public void SaveAuthorization(AuthorizationViewModel auth)
        {
            try
            {
                Console.WriteLine(auth);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region NegFee_POP_UP
        [HttpPost]
        public ActionResult GetNegFeePartial(string pos)
        {
            try
            {
                SetAllMasterData("CreateNewAuth");
                if (pos == "31")
                    return PartialView("/Areas/UM/Views/Common/Modal/_NegFeeModalFor31.cshtml");
                else
                    return PartialView("/Areas/UM/Views/Common/Modal/_NegFeeModalFor61.cshtml");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveNegFee(NEGFeeDetailViewModel NegFee)
        {

        }
        #endregion

        public ActionResult OpenContactModal()
        {
            SetAllMasterData("ContactMasterData");
            ViewData["MasterContactReasons"] = "";
            ViewData["MasterContactOutcomes"] = "";
            return PartialView("~/Areas/UM/Views/Common/Contact/Common/_ContactForm.cshtml");
        }

        public JsonResult SaveContact(AuthorizationContactViewModel contact)
        {
            try
            {
                var data1 = JsonConvert.SerializeObject(contact, new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                });
                return Json(new { Status = true, data = data1, Message = "New contact Added Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = false, data = "", Message = "Error Occurred While Adding Contact" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OpenNoteModal()
        {
            SetAllMasterData("NoteMasterData");
            return PartialView("~/Areas/UM/Views/Common/Note/Common/_NoteForm.cshtml");
        }

        public JsonResult SaveNote(NoteViewModel note)
        {
            if (note.NoteType.ToUpper() != "MD REVIEW")
                note.NoteStatus = "";
            try
            {
                return Json(new { Status = true, data = note, Message = "Edied Note Saved Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = false, data = "", Message = "Error Occurred While Saving Note" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDocumentNames()
        {
            var MasterData = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Areas/UM/Views/web.config").AppSettings.Settings["CreateNewAuth"].Value.Split(',');
            var names = typeof(MasterDataService).GetMethod("GetDocumentNames").Invoke(service, new object[0]);
            return Json(new { Status = true, data = names, Message = "New Note Added Successfully." }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IcdCodeData(string version, string Code, int limit)
        {
            ICDService service = new ICDService();
            List<ICDViewModel> ICDViewModel = new List<ICDViewModel>();
            ICDViewModel = service.GetAllIcdsByicdorcodeStringwithLimit(version, Code, limit);
            return Json(new { Status = true, data = ICDViewModel}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IcdDescriptionData(string version, string Desc, int limit)
        {
            ICDManager service = new ICDManager();
            List<ICDViewModel> ICDViewModel = new List<ICDViewModel>();
            ICDViewModel = service.GetICDCodesByDescWithLimit(version, Desc, limit);
            return Json(new { Status = true, data = ICDViewModel}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CptCodeData(string Code, int limit)
        {
            //CPTService service = new CPTService();
            CPTManager service = new CPTManager();
            List<CPTViewModel> CPTViewModel = new List<CPTViewModel>();
            CPTViewModel = service.GetAllCPTCodesByCodeString(Code, limit);
            return Json(new { Status = true, data = CPTViewModel, Message = "Getting CPT Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CptDescData(string Desc, int limit)
        {
            CPTService service = new CPTService();
            List<CPTViewModel> CPTViewModel = new List<CPTViewModel>();
            CPTViewModel = service.GetAllCPTCodesByCodeString(Desc, limit);
            return Json(new { Status = true, data = CPTViewModel, Message = "Getting CPT Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DRGCodeData(string Code, int limit)
        {
           DRGService service = new DRGService();
            List<DRGCodeViewModel> DRGViewModel = new List<DRGCodeViewModel>();
            DRGViewModel = service.GetAllDRGCodesByCode(Code, limit);
            return Json(new { Status = true, data = DRGViewModel, Message = "Getting DRG Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DRGDescData(string Code, int limit)
        {
            DRGService service = new DRGService();
            List<DRGCodeViewModel> DRGViewModel = new List<DRGCodeViewModel>();
            DRGViewModel = service.GetAllDRGCodesByDesc(Code, limit);
            return Json(new { Status = true, data = DRGViewModel, Message = "Getting DRG Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MDCCodeData(string Code, int limit)
        {
            MDCService service = new MDCService();
            List<MDCCodeViewModel> MDCViewModel = new List<MDCCodeViewModel>();
            MDCViewModel = service.GetAllMDCCodesByCode(Code, limit);
            return Json(new { Status = true, data = MDCViewModel, Message = "Getting MDC Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MDCDescData(string Code, int limit)
        {
            MDCService service = new MDCService();
            List<MDCCodeViewModel> MDCViewModel = new List<MDCCodeViewModel>();
            MDCViewModel = service.GetAllMDCCodesByDesc(Code, limit);
            return Json(new { Status = true, data = MDCViewModel, Message = "Getting MDC Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CptRangeData(string FromCode, string ToCode)
        {
            CPTService service = new CPTService();
            List<CPTViewModel> CPTViewModel = new List<CPTViewModel>();
            CPTViewModel = service.GetAllCPTCodesByRange(FromCode, ToCode);
            return Json(new { Status = true, data = CPTViewModel, Message = "Getting CPT Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UMServiceGroupCPT(int UMServiceGrpID)
        {
            //CMSService service = new CMSService();
            List<CPTViewModel> CPTViewModel = new List<CPTViewModel>();
            CPTViewModel = masterDataService.UMServiceGroupCPT(UMServiceGrpID);
            return Json(new { Status = true, data = CPTViewModel, Message = "Getting CPT Service." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FilterContactReasons(string ContactType, string ContactEntity, string Direction)
        {
            SetAllMasterData("ContactMasterData");
            List<ReasonViewModel> reasons = new List<ReasonViewModel>();
            reasons = (List<ReasonViewModel>)ViewData["MasterContactReasons"];
            var filterreasonList = ViewData["MasterContactReasons"] = FilterReason(reasons, ContactType, ContactEntity, Direction);
            return Json(new { Status = true, data = filterreasonList, Message = "Master Data For Reason retrieved successfully." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FilterContactOutcomes(string ContactType, string ContactEntity, string OutcomeType)
        {
            SetAllMasterData("ContactMasterData");
            List<OutcomeViewModel> outcomes = new List<OutcomeViewModel>();
            outcomes = (List<OutcomeViewModel>)ViewData["MasterContactOutcomes"];
            var filteroutcomelist = ViewData["MasterContactOutcomes"] = FilterOutcome(outcomes, ContactType, ContactEntity, OutcomeType);
            return Json(new { Status = true, data = filteroutcomelist, Message = "Master Data For Outcome retrieved successfully." }, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public List<ReasonViewModel> FilterReason(List<ReasonViewModel> reasons, string ContactType, string ContactEntity, string Direction)
        {
            List<ReasonViewModel> reasonList = new List<ReasonViewModel>();
            if (ContactType != null && ContactEntity != null && Direction != null)
            {
                reasonList = reasons.FindAll(x => x.ContactTypeName.ToUpper().Equals(ContactType.ToUpper()) && x.EntityName.ToUpper().Equals(ContactEntity.ToUpper()) && x.Direction.ToUpper().Equals(Direction.ToUpper()));
                return reasonList;
            }
            else
            {
                return null;
            }
        }

        [NonAction]
        public List<OutcomeViewModel> FilterOutcome(List<OutcomeViewModel> outcomes, string ContactType, string ContactEntity, string OutcomeTypeName)
        {
            List<OutcomeViewModel> outcomeList = new List<OutcomeViewModel>();
            if (ContactType != null && ContactEntity != null && OutcomeTypeName != null)
            {
                outcomeList = outcomes.FindAll(x => x.ContactTypeName.ToUpper().Equals(ContactType.ToUpper()) && x.EntityName.ToUpper().Equals(ContactEntity.ToUpper()) && x.OutcomeTypeName.ToUpper().Equals(OutcomeTypeName.ToUpper()));
                return outcomeList;
            }
            else
            {
                return null;
            }
        }

        //--- for plain languages ----
        [HttpPost]
        public ActionResult GetPlainLanguages(AuthorizationViewModel authobj)
        {
            try
            {
                List<AuthPlainLanguageViewModel> data = masterDataService.GetAuthPlainLanguage();
                ViewBag.MasterPlainLanguages= data;
                return PartialView("~/Areas/UM/Views/Authorization/ICDCPT/_PlainLanguageModal.cshtml", authobj);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPost]
        public int? CalculatePlainlanguage(AuthorizationViewModel auth,int index)
        {
            int?  data = authServices.CalculatePlainLanguageValues(auth,index);
            return data;
        }

        [HttpPost]
        public AuthorizationViewModel AddPlainlanguage(AuthorizationViewModel auth, List<string> PlainLanguages)
        {
            for (int i = 0; i < PlainLanguages.Count; i++)
            {
                auth.CPTCodes[i].PlainLang = PlainLanguages[i];
            }
            return auth;
        }

        public JsonResult GetPcpData(string SubscriberID)
        {
            PortalTemplate.Areas.Portal.Services.Member.MemberService memberService = new PortalTemplate.Areas.Portal.Services.Member.MemberService();
            MemberViewModel member = memberService.GetMemberInfoBySubscriberID(SubscriberID);
            return Json(member, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CalypsoAIDuplicateCPTS(string CptCodes, string SubscriberID, string ServiceOrAttendingNPI, string FacilityNPI, string ServiceOrAttendingSpeciality)
        {
            AIListViewModel calypsoAI = new AIListViewModel();
            AICalypsoInputViewModel input = new AICalypsoInputViewModel();
            input.CptCodes = (CptCodes!=null)?(CptCodes.Split(',').ToList()):null;
            input.SubscriberID = SubscriberID;
            input.ServiceOrAttendingNPI = ServiceOrAttendingNPI;
            input.ServiceOrAttendingSpeciality = ServiceOrAttendingSpeciality;
            input.FacilityNPI = FacilityNPI;
            calypsoAI = await calypsoService.CheckCPTDuplicacy(input);
            return PartialView("~/Areas/UM/Views/Common/Modal/_CalypsoAIModal.cshtml", calypsoAI);
        }
    }
}
