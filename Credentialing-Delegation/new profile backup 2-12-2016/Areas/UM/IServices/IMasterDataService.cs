using PortalTemplate.Areas.UM.Models.MasterDataEntities;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IMasterDataService
    {
        //It will call cms services in future
         List<ContactTypeViewModel> GetContactTypes();
         List<AttachmentTypeViewModel> GetAttachmentTypeViewModel();
         List<AuthorizationRoleTextSnippetViewModel> GetAuthorizationRoleTextSnippet();
         List<AuthorizationRoleViewModel> GetAuthorizationRole();
         List<AuthorizationTypeViewModel> GetAuthorizationType();
         List<AuthPlainLanguageViewModel> GetAuthPlainLanguage();
         List<ContactDirectionViewModel> GetContactDirection();
         List<ContactEntityTypeViewModel> GetContactEntityType();
         List<ContactEntityViewModel> GetContactEntity();
         List<ContactOutcomeViewModel> GetContactOutcome();
         List<CPTCodeViewModel> GetCPTCode();
         List<DenialLOSReasonViewModel> GetDenialLOSReason();
         List<DisciplineViewModel> GetDiscipline();
         List<DocumentNameViewModel> GetDocumentName();
         List<DocumentTypeViewModel> GetDocumentType();
         List<ICDCodesViewModel> GetICDCodes();
         List<LetterTemplateViewModel> GetLetterTemplate();
         List<LevelOfCareViewModel> GetLevelOfCare();
         List<MasterDataPlaceOfServiceViewModel> GetMasterDataPlaceOfService();
         List<NoteSubjectViewModel> GetNoteSubject();
         List<NoteTypeViewModel> GetNoteType();
         List<OutcomeTypeViewModel> GetOutcomeType();
         List<OutcomeViewModel> GetOutcome();
         List<PlanURLViewModel> GetPlanURL();
         List<RangeViewModel> GetRange();
         List<ReasonViewModel> GetReason();
         List<RequestTypeViewModel> GetRequestType();
         List<ServiceRequestViewModel> GetServiceRequest();
         List<ReviewLinkViewModel> GetReviewLink();
         List<TextSnippetViewModel> GetTextSnippet();
         List<TypeOfCareViewModel> GetTypeOfCare();
         List<TypeOfServiceViewModel> GetTypeOfService();
         List<UMServiceGroupViewModel> GetUMServiceGroup();
         List<RoomTypeViewModel> GetRoomType();
         List<RoomTypeViewModel> GetRoomTypeByPOS(string POScode);
         List<ReviewTypeViewModel> GetReviewType();
         List<OPTypeViewModel> GetOPType();
         List<ReasonViewModel> GetContactReason();
         List<DocumentNameViewModel> GetDocumentNames();
         List<UMServiceGroupViewModel> GetUMServiceGroupByPOS(int PlaceOfServiceID);
         List<CPTViewModel> UMServiceGroupCPT(int UMServiceGroupID);
      }
}