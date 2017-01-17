using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.MasterDataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Data.Entity;
using PortalTemplate.Areas.UM.Models.ServiceModels;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PortalTemplate.Areas.UM.Services
{
    public class MasterDataService : IMasterDataService
    {
        public List<ContactTypeViewModel> GetContactTypes()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ContactTypeViewModel> contactType = serial.Deserialize<List<ContactTypeViewModel>>(GetJSON("MasterContactTypes.json"));
            return contactType;
        }

        public List<AttachmentTypeViewModel> GetAttachmentTypeViewModel()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<AttachmentTypeViewModel> attachmentType = serial.Deserialize<List<AttachmentTypeViewModel>>(GetJSON("MasterDocumentTypes.json"));
            return attachmentType;
        }

        public List<AuthorizationRoleTextSnippetViewModel> GetAuthorizationRoleTextSnippet()
        {
            throw new NotImplementedException();
        }

        public List<AuthorizationRoleViewModel> GetAuthorizationRole()
        {
            throw new NotImplementedException();
        }

        public List<AuthorizationTypeViewModel> GetAuthorizationType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<AuthorizationTypeViewModel> authorizationType = serial.Deserialize<List<AuthorizationTypeViewModel>>(GetJSON("MasterAuthorizationTypes.json"));
            return authorizationType;
        }
        public List<DocumentNameViewModel> GetDocumentNames()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DocumentNameViewModel> documentName = serial.Deserialize<List<DocumentNameViewModel>>(GetJSON("MasterDocumentNames.json"));
            return documentName;
        }
        

        public List<AuthPlainLanguageViewModel> GetAuthPlainLanguage()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<AuthPlainLanguageViewModel> authPlainLanguage = serial.Deserialize<List<AuthPlainLanguageViewModel>>(GetJSON("MasterPlainLanguages.json"));
            return authPlainLanguage;
        }

        public List<ContactDirectionViewModel> GetContactDirection()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ContactDirectionViewModel> contactDirection = serial.Deserialize<List<ContactDirectionViewModel>>(GetJSON("MasterContactDirections.json"));
            return contactDirection;
        }

        public List<ContactEntityTypeViewModel> GetContactEntityType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ContactEntityTypeViewModel> contactEntityType = serial.Deserialize<List<ContactEntityTypeViewModel>>(GetJSON("MasterContactEntityTypes.json"));
            return contactEntityType;
        }

        public List<ContactEntityViewModel> GetContactEntity()
        {
            throw new NotImplementedException();
        }

        public List<ContactOutcomeViewModel> GetContactOutcome()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ContactOutcomeViewModel> contactOutcome = serial.Deserialize<List<ContactOutcomeViewModel>>(GetJSON("MasterContactOutcomesMapping.json"));
            return contactOutcome;
        }

        public List<CPTCodeViewModel> GetCPTCode()
        {
            throw new NotImplementedException();
        }

        public List<DenialLOSReasonViewModel> GetDenialLOSReason()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DenialLOSReasonViewModel> denialLOSReason = serial.Deserialize<List<DenialLOSReasonViewModel>>(GetJSON("MasterDeniedReasons.json"));
            return denialLOSReason;
        }

        public List<ReasonViewModel> GetContactReason()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ReasonViewModel> contactReason = serial.Deserialize<List<ReasonViewModel>>(GetJSON("MasterContactReasonsMapping.json"));
            return contactReason;
        }

        public List<DisciplineViewModel> GetDiscipline()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DisciplineViewModel> Discipline = serial.Deserialize<List<DisciplineViewModel>>(GetJSON("MasterDisciplines.json"));
            return Discipline;
        }

        public List<DocumentNameViewModel> GetDocumentName()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DocumentNameViewModel> DocumentName = serial.Deserialize<List<DocumentNameViewModel>>(GetJSON("MasterDocumentNames.json"));
            return DocumentName;
        }

        public List<DocumentTypeViewModel> GetDocumentType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<DocumentTypeViewModel> DocumentType = serial.Deserialize<List<DocumentTypeViewModel>>(GetJSON("MasterDocumentTypes.json"));
            return DocumentType;
           
        }

        public List<ICDCodesViewModel> GetICDCodes()
        {
            throw new NotImplementedException();
        }

        public List<LetterTemplateViewModel> GetLetterTemplate()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<LetterTemplateViewModel> LetterTemplate = serial.Deserialize<List<LetterTemplateViewModel>>(GetJSON("MasterLetterTemplates.json"));
            return LetterTemplate;
        }

        public List<LevelOfCareViewModel> GetLevelOfCare()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<LevelOfCareViewModel> LevelOfCare = serial.Deserialize<List<LevelOfCareViewModel>>(GetJSON("MasterLevelOfCares.json"));
            return LevelOfCare;
        }


        public List<MasterDataPlaceOfServiceViewModel> GetMasterDataPlaceOfService()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<MasterDataPlaceOfServiceViewModel> PlaceOfService = serial.Deserialize<List<MasterDataPlaceOfServiceViewModel>>(GetJSON("MasterPlacesOfService.json"));
            return PlaceOfService;
        }

        public List<NoteSubjectViewModel> GetNoteSubject()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<NoteSubjectViewModel> NoteSubject = serial.Deserialize<List<NoteSubjectViewModel>>(GetJSON("MasterNoteSubjects.json"));
            return NoteSubject;
        }

        public List<NoteTypeViewModel> GetNoteType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<NoteTypeViewModel> NoteType = serial.Deserialize<List<NoteTypeViewModel>>(GetJSON("MasterNoteTypes.json"));
            return NoteType;
        }

        public List<OutcomeViewModel> GetOutcome()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<OutcomeViewModel> Outcome = serial.Deserialize<List<OutcomeViewModel>>(GetJSON("MasterContactOutcomesMapping.json"));
            return Outcome;
        }

        public List<OutcomeTypeViewModel> GetOutcomeType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<OutcomeTypeViewModel> OutcomeType = serial.Deserialize<List<OutcomeTypeViewModel>>(GetJSON("MasterContactOutcomeTypes.json"));
            return OutcomeType;
        }

        public List<PlanURLViewModel> GetPlanURL()
        {
            throw new NotImplementedException();
        }

        public List<RangeViewModel> GetRange()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<RangeViewModel> range = serial.Deserialize<List<RangeViewModel>>(GetJSON("MasterRangeTypes.json"));
            return range;
        }

        public List<ReasonViewModel> GetReason()
        {
            throw new NotImplementedException();
        }

        public List<RequestTypeViewModel> GetRequestType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<RequestTypeViewModel> RequestType = serial.Deserialize<List<RequestTypeViewModel>>(GetJSON("MasterRequestTypes.json"));
            return RequestType;
        }

        public List<ServiceRequestViewModel> GetServiceRequest()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ServiceRequestViewModel> ServiceRequest = serial.Deserialize<List<ServiceRequestViewModel>>(GetJSON("MasterServicesRequested.json"));
            return ServiceRequest;
        }

        public List<ReviewLinkViewModel> GetReviewLink()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ReviewLinkViewModel> ReviewLink = serial.Deserialize<List<ReviewLinkViewModel>>(GetJSON("MasterReviewLinks.json"));
            return ReviewLink;
        }

        public List<TextSnippetViewModel> GetTextSnippet()
        {
            throw new NotImplementedException();
        }

        public List<TypeOfCareViewModel> GetTypeOfCare()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<TypeOfCareViewModel> typeOfCare = serial.Deserialize<List<TypeOfCareViewModel>>(GetJSON("MasterTypesOfCare.json"));
            return typeOfCare;
        }

        public List<TypeOfServiceViewModel> GetTypeOfService()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<TypeOfServiceViewModel> TypeOfService = serial.Deserialize<List<TypeOfServiceViewModel>>(GetJSON("MasterTypeOfServices.json"));
            return TypeOfService;
        }

        public List<UMServiceGroupViewModel> GetUMServiceGroup()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<UMServiceGroupViewModel> UMServiceGroup = serial.Deserialize<List<UMServiceGroupViewModel>>(GetJSON("MasterUMSvcGroups.json"));
            return UMServiceGroup;
        }

        public List<RoomTypeViewModel> GetRoomType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<RoomTypeViewModel> roomtype = serial.Deserialize<List<RoomTypeViewModel>>(GetJSON("MasterRoomType.json"));
            return roomtype;
        }

        public List<ReviewTypeViewModel> GetReviewType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ReviewTypeViewModel> reviewtype = serial.Deserialize<List<ReviewTypeViewModel>>(GetJSON("MasterReviewType.json"));
            return reviewtype;
        }

        public List<OPTypeViewModel> GetOPType()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<OPTypeViewModel> optype = serial.Deserialize<List<OPTypeViewModel>>(GetJSON("MasterOPType.json"));
            return optype;
        }

        public List<ICDServiceModel> GetICDCodesFromService()
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ICDServiceModel> icdCodes = serial.Deserialize<List<ICDServiceModel>>(GetJSON("ICD9ServiceData.json", "ServiceData"));
            return icdCodes;
        }
        public List<ODAGViewModel> GetMasterOdagDataFromService()
        {
            //JavaScriptSerializer serial = new JavaScriptSerializer();
            List<ODAGViewModel> odag = new List<ODAGViewModel>();
            Task<string> codagList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/UM/GetAllQuestions?sourceID=1");
                return msg;
            });

            if (codagList.Result != null)
            {
                odag = JsonConvert.DeserializeObject<List<ODAGViewModel>>(codagList.Result);
            }
            
            //List<ODAGViewModel> odag = serial.Deserialize<List<ODAGViewModel>>(GetJSON("MasterODAGQuestions.json"));
            return odag;
        }
        private string GetJSON(string SourceFile, string DataType = "MasterData")
        {
            string file = "";
            if (DataType == "MasterData")
            {
                file = HostingEnvironment.MapPath(GetMasterDataResourceLink(SourceFile));
                return System.IO.File.ReadAllText(file);
            }
            else if (DataType == "ServiceData")
            {
                file = HostingEnvironment.MapPath(GetServiceDataResourceLink(SourceFile));
                return System.IO.File.ReadAllText(file);
            }
            return file;
        }

        private string GetMasterDataResourceLink(string SourceFileName)
        {
            return "~/Areas/UM/Resources/JSONData/MasterData/" + SourceFileName;
        }
        private string GetServiceDataResourceLink(string SourceFileName)
        {
            return "~/Areas/UM/Resources/ServiceData/" + SourceFileName;
        }

        private static async Task<string> GetDataFromService(string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CMSServiceWebAPIURL"].ToString());
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return result;
            }
        }


       


        public List<UMServiceGroupViewModel> GetUMServiceGroupByPOS(int PlaceOfServiceID)
        {
            throw new NotImplementedException();
        }


        public List<Models.ViewModels.CPT.CPTViewModel> UMServiceGroupCPT(int UMServiceGroupID)
        {
            throw new NotImplementedException();
        }


        public List<RoomTypeViewModel> GetRoomTypeByPOS(string POScode)
        {
            throw new NotImplementedException();
        }
    }
}