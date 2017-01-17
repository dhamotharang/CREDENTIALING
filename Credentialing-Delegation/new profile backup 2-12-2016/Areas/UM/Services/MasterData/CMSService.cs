using Newtonsoft.Json;
using PortalTemplate.Areas.UM.CustomHelpers;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.MasterDataEntities;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;//
using System.Web.Hosting;//
using System.Web.Script.Serialization;//

namespace PortalTemplate.Areas.UM.Services.MasterData
{
    public class CMSService : IMasterDataService
    {
        private readonly string baseURL;
        private readonly string UMbaseURL;
        private readonly ServiceUtility serviceUtility;
        public CMSService()
        {
            this.baseURL = ConfigurationManager.AppSettings["CMSServiceWebAPIURL"].ToString();
            this.UMbaseURL = ConfigurationManager.AppSettings["UMService"].ToString();
            this.serviceUtility = new ServiceUtility();
        }

        public List<ODAGViewModel> GetMasterOdagDataFromService()
        {
           
            List<ODAGViewModel> odag = new List<ODAGViewModel>();
            Task<string> codagList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/CareManagement/GetODAGQuestionery");
                return msg;
            });

            if (codagList.Result != null)
            {
                odag = JsonConvert.DeserializeObject<List<ODAGViewModel>>(codagList.Result);
            }

            
            return odag;
        }

        public List<Models.MasterDataEntities.ContactTypeViewModel> GetContactTypes()
        {
            List<Models.MasterDataEntities.ContactTypeViewModel> contactTypes = new List<Models.MasterDataEntities.ContactTypeViewModel>();
            Task<string> contactTypeList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllContactEntityTypeAsync");
                return msg;
            });
            if (contactTypeList.Result != null)
            {
                contactTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ContactTypeViewModel>>(contactTypeList.Result);
            }
            return contactTypes;
        }

        public List<Models.MasterDataEntities.AttachmentTypeViewModel> GetAttachmentTypeViewModel()
        {
            List<Models.MasterDataEntities.AttachmentTypeViewModel> attachmentTypes = new List<Models.MasterDataEntities.AttachmentTypeViewModel>();
            Task<string> attachmentTypeList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllAttachmentTypeAsync");
                return msg;
            });
            if (attachmentTypeList.Result != null)
            {
                attachmentTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.AttachmentTypeViewModel>>(attachmentTypeList.Result);
            }
            return attachmentTypes;
        }

        public List<Models.MasterDataEntities.AuthorizationRoleTextSnippetViewModel> GetAuthorizationRoleTextSnippet()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.AuthorizationRoleViewModel> GetAuthorizationRole()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.AuthorizationTypeViewModel> GetAuthorizationType()
        {
            List<Models.MasterDataEntities.AuthorizationTypeViewModel> authorizationTypes = new List<Models.MasterDataEntities.AuthorizationTypeViewModel>();
            Task<string> authorizationTypeList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/CareManagement/GetAllAuthorizationTypes");
                return msg;
            });
            if (authorizationTypeList.Result != null)
            {
                authorizationTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.AuthorizationTypeViewModel>>(authorizationTypeList.Result);
            }
            return authorizationTypes;
        }

        public List<Models.MasterDataEntities.AuthPlainLanguageViewModel> GetAuthPlainLanguage()
        {
            List<Models.MasterDataEntities.AuthPlainLanguageViewModel> authPlainLanguages = new List<Models.MasterDataEntities.AuthPlainLanguageViewModel>();
            Task<string> authPlainLanguagesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllPlainLanguage");
                return msg;
            });
            if (authPlainLanguagesList.Result != null)
            {
                authPlainLanguages = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.AuthPlainLanguageViewModel>>(authPlainLanguagesList.Result);
            }
            return authPlainLanguages;
        }

        public List<Models.MasterDataEntities.ContactDirectionViewModel> GetContactDirection()
        {
            List<Models.MasterDataEntities.ContactDirectionViewModel> contactDirections = new List<Models.MasterDataEntities.ContactDirectionViewModel>();
            Task<string> contactDirectionsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllContactDirectionAsync");
                return msg;
            });
            if (contactDirectionsList.Result != null)
            {
                contactDirections = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ContactDirectionViewModel>>(contactDirectionsList.Result);
            }
            return contactDirections;
        }

        public List<Models.MasterDataEntities.ContactEntityTypeViewModel> GetContactEntityType()
        {
            List<Models.MasterDataEntities.ContactEntityTypeViewModel> contactDirections = new List<Models.MasterDataEntities.ContactEntityTypeViewModel>();
            Task<string> contactDirectionsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllContactDirectionAsync");
                return msg;
            });
            if (contactDirectionsList.Result != null)
            {
                contactDirections = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ContactEntityTypeViewModel>>(contactDirectionsList.Result);
            }
            return contactDirections;
        }

        public List<Models.MasterDataEntities.ContactEntityViewModel> GetContactEntity()
        {
            List<Models.MasterDataEntities.ContactEntityViewModel> contactEntities = new List<Models.MasterDataEntities.ContactEntityViewModel>();
            Task<string> contactEntitiesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetContactEntityAsync");
                return msg;
            });
            if (contactEntitiesList.Result != null)
            {
                contactEntities = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ContactEntityViewModel>>(contactEntitiesList.Result);
            }
            return contactEntities;
        }

        public List<Models.MasterDataEntities.ContactOutcomeViewModel> GetContactOutcome()
        {
            List<Models.MasterDataEntities.ContactOutcomeViewModel> contactOutcomes = new List<Models.MasterDataEntities.ContactOutcomeViewModel>();
            Task<string> contactOutcomesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllContactOutcomeAsync");
                return msg;
            });
            if (contactOutcomesList.Result != null)
            {
                contactOutcomes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ContactOutcomeViewModel>>(contactOutcomesList.Result);
            }
            return contactOutcomes;
        }

        public List<Models.MasterDataEntities.CPTCodeViewModel> GetCPTCode()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.DenialLOSReasonViewModel> GetDenialLOSReason()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.DisciplineViewModel> GetDiscipline()
        {
            List<Models.MasterDataEntities.DisciplineViewModel> disciplines = new List<Models.MasterDataEntities.DisciplineViewModel>();
            Task<string> disciplineList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllDiscipline");
                return msg;
            });
            if (disciplineList.Result != null)
            {
                disciplines = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.DisciplineViewModel>>(disciplineList.Result);
            }
            return disciplines;
        }

        public List<Models.MasterDataEntities.DocumentNameViewModel> GetDocumentName()
        {
            List<Models.MasterDataEntities.DocumentNameViewModel> documentNames = new List<Models.MasterDataEntities.DocumentNameViewModel>();
            Task<string> documentNameList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllDocumentNames");
                return msg;
            });
            if (documentNameList.Result != null)
            {
                documentNames = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.DocumentNameViewModel>>(documentNameList.Result);
            }
            return documentNames;
        }

        public List<Models.MasterDataEntities.DocumentTypeViewModel> GetDocumentType()
        {
            List<Models.MasterDataEntities.DocumentTypeViewModel> documentTypes = new List<Models.MasterDataEntities.DocumentTypeViewModel>();
            Task<string> documentTypesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllDocumentTypeAsync");
                return msg;
            });
            if (documentTypesList.Result != null)
            {
                documentTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.DocumentTypeViewModel>>(documentTypesList.Result);
            }
            return documentTypes;
        }

        public List<Models.MasterDataEntities.ICDCodesViewModel> GetICDCodes()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.LetterTemplateViewModel> GetLetterTemplate()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.LevelOfCareViewModel> GetLevelOfCare()
        {
            List<Models.MasterDataEntities.LevelOfCareViewModel> levelOfCares = new List<Models.MasterDataEntities.LevelOfCareViewModel>();
            Task<string> levelOfCaresList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllLevelOfCare");
                return msg;
            });
            if (levelOfCaresList.Result != null)
            {
                levelOfCares = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.LevelOfCareViewModel>>(levelOfCaresList.Result);
            }
            return levelOfCares;
        }

        public List<Models.MasterDataEntities.MasterDataPlaceOfServiceViewModel> GetMasterDataPlaceOfService()
        {
            List<Models.MasterDataEntities.MasterDataPlaceOfServiceViewModel> placeOfServices = new List<Models.MasterDataEntities.MasterDataPlaceOfServiceViewModel>();
            Task<string> placeOfServicesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(UMbaseURL, "api/MasterData/GetAllPOS");
                return msg;
            });
            if (placeOfServicesList.Result != null)
            {
                placeOfServices = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.MasterDataPlaceOfServiceViewModel>>(placeOfServicesList.Result);
            }
            return placeOfServices;


            //JavaScriptSerializer serial = new JavaScriptSerializer();
            //List<MasterDataPlaceOfServiceViewModel> PlaceOfService = serial.Deserialize<List<MasterDataPlaceOfServiceViewModel>>(GetJSON("MasterPlacesOfService.json"));
            //return PlaceOfService;
        }
        //
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
        //



        public List<Models.MasterDataEntities.NoteSubjectViewModel> GetNoteSubject()
        {
            List<Models.MasterDataEntities.NoteSubjectViewModel> noteSubjects = new List<Models.MasterDataEntities.NoteSubjectViewModel>();
            Task<string> noteSubjectsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllNoteSubjects");
                return msg;
            });
            if (noteSubjectsList.Result != null)
            {
                noteSubjects = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.NoteSubjectViewModel>>(noteSubjectsList.Result);
            }
            return noteSubjects;
        }

        public List<Models.MasterDataEntities.NoteTypeViewModel> GetNoteType()
        {
            List<Models.MasterDataEntities.NoteTypeViewModel> noteTypes = new List<Models.MasterDataEntities.NoteTypeViewModel>();
            Task<string> noteTypesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllNoteTypes");
                return msg;
            });
            if (noteTypesList.Result != null)
            {
                noteTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.NoteTypeViewModel>>(noteTypesList.Result);
            }
            return noteTypes;
        }

        public List<Models.MasterDataEntities.OutcomeTypeViewModel> GetOutcomeType()
        {
            List<Models.MasterDataEntities.OutcomeTypeViewModel> outcomeTypes = new List<Models.MasterDataEntities.OutcomeTypeViewModel>();
            Task<string> outcomeTypesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllOutcomeTypes");
                return msg;
            });
            if (outcomeTypesList.Result != null)
            {
                outcomeTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.OutcomeTypeViewModel>>(outcomeTypesList.Result);
            }
            return outcomeTypes;
        }

        public List<Models.MasterDataEntities.OutcomeViewModel> GetOutcome()
        {
            List<Models.MasterDataEntities.OutcomeViewModel> outcomes = new List<Models.MasterDataEntities.OutcomeViewModel>();
            Task<string> outcomesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllContactOutcomeAsync");
                return msg;
            });
            if (outcomesList.Result != null)
            {
                outcomes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.OutcomeViewModel>>(outcomesList.Result);
            }
            return outcomes;
        }

        public List<Models.MasterDataEntities.PlanURLViewModel> GetPlanURL()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.RangeViewModel> GetRange()
        {
            List<Models.MasterDataEntities.RangeViewModel> ranges = new List<Models.MasterDataEntities.RangeViewModel>();
            Task<string> rangesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllRange");
                return msg;
            });
            if (rangesList.Result != null)
            {
                ranges = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.RangeViewModel>>(rangesList.Result);
            }
            return ranges;
        }

        public List<Models.MasterDataEntities.ReasonViewModel> GetReason()
        {
            List<Models.MasterDataEntities.ReasonViewModel> reasons = new List<Models.MasterDataEntities.ReasonViewModel>();
            Task<string> reasonsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllContactReasons");
                return msg;
            });
            if (reasonsList.Result != null)
            {
                reasons = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ReasonViewModel>>(reasonsList.Result);
            }
            return reasons;
        }

        public List<Models.MasterDataEntities.RequestTypeViewModel> GetRequestType()
        {
            List<Models.MasterDataEntities.RequestTypeViewModel> requestTypes = new List<Models.MasterDataEntities.RequestTypeViewModel>();
            Task<string> requestTypesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllRequestTypes");
                return msg;
            });
            if (requestTypesList.Result != null)
            {
                requestTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.RequestTypeViewModel>>(requestTypesList.Result);
            }
            return requestTypes;
        }

        public List<Models.MasterDataEntities.ServiceRequestViewModel> GetServiceRequest()
        {
            List<Models.MasterDataEntities.ServiceRequestViewModel> serviceRequests = new List<Models.MasterDataEntities.ServiceRequestViewModel>();
            Task<string> serviceRequestsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllServiceRequest");
                return msg;
            });
            if (serviceRequestsList.Result != null)
            {
                serviceRequests = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ServiceRequestViewModel>>(serviceRequestsList.Result);
            }
            return serviceRequests;
        }

        public List<Models.MasterDataEntities.ReviewLinkViewModel> GetReviewLink()
        {
            List<Models.MasterDataEntities.ReviewLinkViewModel> reviewLinks = new List<Models.MasterDataEntities.ReviewLinkViewModel>();
            Task<string> reviewLinksList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllReviewLinkAsync");
                return msg;
            });
            if (reviewLinksList.Result != null)
            {
                reviewLinks = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ReviewLinkViewModel>>(reviewLinksList.Result);
            }
            return reviewLinks;
        }

        public List<Models.MasterDataEntities.TextSnippetViewModel> GetTextSnippet()
        {
            throw new NotImplementedException();
        }

        public List<Models.MasterDataEntities.TypeOfCareViewModel> GetTypeOfCare()
        {
            List<Models.MasterDataEntities.TypeOfCareViewModel> typeOfCares = new List<Models.MasterDataEntities.TypeOfCareViewModel>();
            Task<string> typeOfCaresList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/CareManagement/GetAllTypeOfCares");
                return msg;
            });
            if (typeOfCaresList.Result != null)
            {
                typeOfCares = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.TypeOfCareViewModel>>(typeOfCaresList.Result);
            }
            return typeOfCares;
        }

        public List<Models.MasterDataEntities.TypeOfServiceViewModel> GetTypeOfService()
        {
            List<Models.MasterDataEntities.TypeOfServiceViewModel> TypeOfServices = new List<Models.MasterDataEntities.TypeOfServiceViewModel>();
            Task<string> TypeOfServicesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllTypeOfServices");
                return msg;
            });
            if (TypeOfServicesList.Result != null)
            {
                TypeOfServices = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.TypeOfServiceViewModel>>(TypeOfServicesList.Result);
            }
            return TypeOfServices;
        }

        public List<Models.MasterDataEntities.UMServiceGroupViewModel> GetUMServiceGroup()
        {
            List<Models.MasterDataEntities.UMServiceGroupViewModel> UMServiceGroups = new List<Models.MasterDataEntities.UMServiceGroupViewModel>();
            Task<string> UMServiceGroupsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllUMServiceGroups");
                return msg;
            });
            if (UMServiceGroupsList.Result != null)
            {
                UMServiceGroups = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.UMServiceGroupViewModel>>(UMServiceGroupsList.Result);
            }
            return UMServiceGroups;
        }

        public List<Models.MasterDataEntities.RoomTypeViewModel> GetRoomType()
        {
            List<Models.MasterDataEntities.RoomTypeViewModel> roomTypes = new List<Models.MasterDataEntities.RoomTypeViewModel>();
            Task<string> roomTypesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllRoomTypes");
                return msg;
            });
            if (roomTypesList.Result != null)
            {
                roomTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.RoomTypeViewModel>>(roomTypesList.Result);
            }
            return roomTypes;
        }

        public List<RoomTypeViewModel> GetRoomTypeByPOS(string POScode)
        {
            List<Models.MasterDataEntities.RoomTypeViewModel> roomTypes = new List<Models.MasterDataEntities.RoomTypeViewModel>();
            Task<string> roomTypesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllRoomTypes");
                return msg;
            });
            if (roomTypesList.Result != null)
            {
                roomTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.RoomTypeViewModel>>(roomTypesList.Result);
            }
            return roomTypes;
        }

        public List<Models.MasterDataEntities.ReviewTypeViewModel> GetReviewType()
        {
            List<Models.MasterDataEntities.ReviewTypeViewModel> reviewTypes = new List<Models.MasterDataEntities.ReviewTypeViewModel>();
            Task<string> reviewTypesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllReviewType");
                return msg;
            });
            if (reviewTypesList.Result != null)
            {
                reviewTypes = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ReviewTypeViewModel>>(reviewTypesList.Result);
            }
            return reviewTypes;
        }

        public List<Models.MasterDataEntities.OPTypeViewModel> GetOPType()
        {
            List<Models.MasterDataEntities.OPTypeViewModel> OPTypeViewModel = new List<Models.MasterDataEntities.OPTypeViewModel>();
            return OPTypeViewModel;
        }

        public List<Models.MasterDataEntities.ReasonViewModel> GetContactReason()
        {
            List<Models.MasterDataEntities.ReasonViewModel> contactReasons = new List<Models.MasterDataEntities.ReasonViewModel>();
            Task<string> contactReasonsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllContactReasons");
                return msg;
            });
            if (contactReasonsList.Result != null)
            {
                contactReasons = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.ReasonViewModel>>(contactReasonsList.Result);
            }
            return contactReasons;
        }

        public List<Models.MasterDataEntities.DocumentNameViewModel> GetDocumentNames()
        {
            List<Models.MasterDataEntities.DocumentNameViewModel> documentNames = new List<Models.MasterDataEntities.DocumentNameViewModel>();
            Task<string> documentNamesList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/UM/GetAllDocumentNames");
                return msg;
            });
            if (documentNamesList.Result != null)
            {
                documentNames = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.DocumentNameViewModel>>(documentNamesList.Result);
            }
            return documentNames;
        }


        public List<Models.MasterDataEntities.UMServiceGroupViewModel> GetUMServiceGroupByPOS(int PlaceOfServiceID)
        {
            List<Models.MasterDataEntities.UMServiceGroupViewModel> UMServiceGroups = new List<Models.MasterDataEntities.UMServiceGroupViewModel>();
            Task<string> UMServiceGroupsList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(UMbaseURL, "/api/MasterData/GetAllUMServiceGroupByPOSCode/" + PlaceOfServiceID);
                return msg;
            });
            if (UMServiceGroupsList.Result != null)
            {
                UMServiceGroups = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.UMServiceGroupViewModel>>(UMServiceGroupsList.Result);
            }
            return UMServiceGroups;
        }


        public List<Models.ViewModels.CPT.CPTViewModel> UMServiceGroupCPT(int UMServiceGroupID)
        {
            List<Models.ViewModels.CPT.CPTViewModel> CPTViewModels = new List<Models.ViewModels.CPT.CPTViewModel>();
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(UMbaseURL, "/api/MasterData/GetAllCPTByUMServiceGroup/" + UMServiceGroupID);
                return msg;
            });
            if (cptList.Result != null)
            {
                CPTViewModels = JsonConvert.DeserializeObject<List<Models.ViewModels.CPT.CPTViewModel>>(cptList.Result);
            }
            return CPTViewModels;
        }


        
    }
}