using Newtonsoft.Json;
using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.Portal.Models.Contact;
using PortalTemplate.Areas.Portal.Models.Member;
using PortalTemplate.Areas.UM.CustomHelpers;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Portal.Services.Member
{
    public class MemberService : IMemberHeaderService,IMemberViewService
    {
        private readonly ServiceUtility serviceUtil;
        private string baseURL;
        public MemberService()
        {
            this.serviceUtil = new ServiceUtility();
            this.baseURL = ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString();
        }
        public Models.Member.MemberHeaderViewModel GetMemberHeaderDetailsBySubscriberID(string SubscriberId)
        {
           
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetMemberHeaderDetailsBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            Models.Member.MemberHeaderViewModel memberHeader = JsonConvert.DeserializeObject<Models.Member.MemberHeaderViewModel>(memberList.Result);
            return memberHeader;
        }


        private static async Task<string> GetDataFromService(string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString());
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        //result = JsonConvert.DeserializeObject<object>(jsonAsString);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return result;
            }
        }

        public Models.Member.MemberViewModel GetMemberDetailsBySubsriberID(string SubscriberId)
        {
            List<MemberInformationViewModel> MemberInformation = new List<MemberInformationViewModel>();
            PortalTemplate.Areas.Portal.Models.Member.MemberViewModel MemberViewModel = new PortalTemplate.Areas.Portal.Models.Member.MemberViewModel();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetMemberInfoBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberViewModel = JsonConvert.DeserializeObject<Models.Member.MemberViewModel>(memberList.Result);

                Task<string> memberList1 = Task.Run(async () =>
                {
                    string msg = await GetDataFromService("api/MemberService/GetMemberInformationBySubscriberID?SubScriberID=" + SubscriberId);
                    return msg;
                });
                if (memberList1.Result != null)
                {
                    MemberInformation = JsonConvert.DeserializeObject<List<MemberInformationViewModel>>(memberList1.Result);
                }
                MemberViewModel.MemberInformation = MemberInformation.FirstOrDefault();
            }
            
            return MemberViewModel;
        }

        public object GetCOBInformationofMemberBySubscriberID(string SubscriberId)
        {
            List<Models.Member.MemberCOBViewModel> MembercobModel = new List<Models.Member.MemberCOBViewModel>();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetCOBInformationofMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MembercobModel = JsonConvert.DeserializeObject<List<Models.Member.MemberCOBViewModel>>(memberList.Result);
            }

            return MembercobModel;
        }

        public List<Models.Member.MemberRatingsOverrideViewModel> GetRatingOverRideInformationOfMemberBySubscriberID(string SubscriberId)
        {
            List<MemberRatingsOverrideViewModel> MemberModel = new List<MemberRatingsOverrideViewModel>();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetRatingOverRideInformationOfMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<List<MemberRatingsOverrideViewModel>>(memberList.Result);
            }
            
            return MemberModel;
        }

        public List<Models.Member.MemberImmunHealthScreeningViewModel> GetHealthScreeningInformationOfMemberBySubscriberID(string SubscriberId)
        {
            List<MemberImmunHealthScreeningViewModel> MemberModel = new List<MemberImmunHealthScreeningViewModel>();

            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetHealthScreeningInformationOfMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<List<MemberImmunHealthScreeningViewModel>>(memberList.Result);
            }
            
            return MemberModel;
        }

        public List<Models.Member.MemberAddressViewModel> GetAddressInformationOfMemberBySubscriberID(string SubscriberId)
        {
            List<MemberAddressViewModel> MemberModel = new List<MemberAddressViewModel>();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetAddressInformationOfMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<List<MemberAddressViewModel>>(memberList.Result);
            }
            
            return MemberModel;
        }

        public List<Models.Member.MemberEligibilityViewModel> GetEligibilityInformationOfMemberBySubscriberID(string SubscriberId)
        {
            List<MemberEligibilityViewModel> MemberModel = new List<MemberEligibilityViewModel>();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetEligibilityInformationOfMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<List<MemberEligibilityViewModel>>(memberList.Result);
            }
            return MemberModel;
        }

        public List<Models.Member.MemberMedicareViewModel> GetMedicareInformationBySubscriberID(string SubscriberId)
        {
            List<MemberMedicareViewModel> MemberModel = new List<MemberMedicareViewModel>();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetMedicareInformationBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<List<MemberMedicareViewModel>>(memberList.Result);
            }
            
            return MemberModel;
        }

        public List<Models.Member.MemberProviderViewModel> GetProviderInformationRelatedToMemberBySubscriberID(string SubscriberId)
        {
            List<MemberProviderViewModel> MemberModel = new List<MemberProviderViewModel>();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetProviderInformationRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<List<MemberProviderViewModel>>(memberList.Result);
            }
            
            return MemberModel;
        }

        public Models.Member.MemberOtherDemographicsViewModel GetDemographicInformationBySubscriberID(string SubscriberId)
        {
            MemberOtherDemographicsViewModel MemberModel = new MemberOtherDemographicsViewModel();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetDemographicInformationBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<MemberOtherDemographicsViewModel>(memberList.Result);
            }
            
            return MemberModel;
        }

        public Models.Member.MemberICEViewModel GetEmergencyContactInformationBySubscriberID(string SubscriberId)
        {
            MemberICEViewModel MemberModel = new MemberICEViewModel();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetDemographicInformationBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<MemberICEViewModel>(memberList.Result);
            }
            
            return MemberModel;
        }

        public Models.Member.MemberRepresentativeViewModel GetResponsiblePersonInformationBySubscriberID(string SubscriberId)
        {
            MemberRepresentativeViewModel MemberModel = new MemberRepresentativeViewModel();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetResponsiblePersonInformationBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });
            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<MemberRepresentativeViewModel>(memberList.Result);
            }
            return MemberModel;
        }

        public Models.Member.MemberEnrollmentViewModel GetEnrollementInformationOfMemberBySubscriberID(string SubscriberId)
        {
            MemberEnrollmentViewModel MemberModel = new MemberEnrollmentViewModel();
            Task<string> memberList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetEnrollementInformationOfMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });

            if (memberList.Result != null)
            {
                MemberModel = JsonConvert.DeserializeObject<MemberEnrollmentViewModel>(memberList.Result);
            }
            return MemberModel;
        }

        public ProviderViewModal GetPCPInfo(string SubscriberID)
        {
            ProviderViewModal providerViewModel = new ProviderViewModal();
            Task<string> pcp = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetProviderInformationRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberID);
                return msg;
            });

            if (pcp.Result != null)
            {
                providerViewModel = JsonConvert.DeserializeObject<ProviderViewModal>(pcp.Result);
            }
            return providerViewModel;
        }


        public UM.Models.ViewModels.Authorization.MemberViewModel GetMemberInfoBySubscriberID(string SubscriberID)
        {
            UM.Models.ViewModels.Authorization.MemberViewModel Member = new UM.Models.ViewModels.Authorization.MemberViewModel();
            Task<string> MemberViewModel = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetMemberInfoBySubscriberID?SubScriberID=" + SubscriberID);
                return msg;
            });

            if (MemberViewModel.Result != null)
            {
                Member = JsonConvert.DeserializeObject<UM.Models.ViewModels.Authorization.MemberViewModel>(MemberViewModel.Result);
            }
            return Member;
        }
        public List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel> GetNotesRelatedToMemberBySubscriberID(string SubscriberId)
        {
            List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel> notes = new List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>();
            Task<string> notesList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetAllNotesRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });
            if (notesList.Result != null)
            {
                notes = JsonConvert.DeserializeObject<List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>>(notesList.Result);
            }
            return notes;
        }
        public List<AuthorizationContactViewModel> GetContactsRelatedToMemberBySubscriberID(string SubscriberId)
        {
            List<AuthorizationContactViewModel> contacts = new List<AuthorizationContactViewModel>();
            Task<string> contactsList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetAllContactsRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });
            if (contactsList.Result != null)
            {
                contacts = JsonConvert.DeserializeObject<List<AuthorizationContactViewModel>>(contactsList.Result);
            }
            return contacts;
        }
        public List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel> GetAttachmentsRelatedToMemberBySubscriberID(string SubscriberId)
        {
            List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel> attachments = new List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel>();
            Task<string> attachmentsList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/MemberService/GetAllAttachementsRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberId);
                return msg;
            });
            if (attachmentsList.Result != null)
            {
                attachments = JsonConvert.DeserializeObject<List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel>>(attachmentsList.Result);
            }
            return attachments;
        }
        public object GetFilteredDataBySubsriberID(string module, List<string> moduleArray, string SubscriberID)
       {
            List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel> attachments = new List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel>();
            List<AuthorizationContactViewModel> contacts = new List<AuthorizationContactViewModel>();
            List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel> notes = new List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>();

            switch (module)
            {
                case "ATTACHMENT":
                    #region Attachment
                    for (int i = 0; i < moduleArray.Count; i++)
                    {
                        if (moduleArray[i] == "UM")
                        {
                            List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel> UMattachments = new List<Models.Attachment.AttachmentViewModel>();
                            this.baseURL = ConfigurationManager.AppSettings["UMService"].ToString();
                            Task<string> attachmentsList = Task.Run(async () =>
                            {
                                string msg = await serviceUtil.GetDataFromService(baseURL, "api/Member/GetAllDocumentsBySubscriberID?subscriberID=" + SubscriberID);
                                return msg;
                            });
                            if (attachmentsList.Result != null)
                            {
                                UMattachments = JsonConvert.DeserializeObject<List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel>>(attachmentsList.Result);
                                foreach (var attachment in UMattachments)
                                {
                                    attachment.ModuleName = "UM";
                                }
                            }
                            attachments.AddRange(UMattachments);
                        }
                        else if (moduleArray[i] == "MH")
                        {
                            List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel> UMattachments = new List<Models.Attachment.AttachmentViewModel>();
                            this.baseURL = ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString();
                            Task<string> attachmentsList = Task.Run(async () =>
                            {
                                string msg = await serviceUtil.GetDataFromService(baseURL, "api/MemberService/GetAllAttachementsRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberID);
                                return msg;
                            });
                            if (attachmentsList.Result != null)
                            {
                                UMattachments = JsonConvert.DeserializeObject<List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel>>(attachmentsList.Result);
                                foreach (var attachment in UMattachments)
                                {
                                    attachment.ModuleName = "MH";
                                }
                            }
                            attachments.AddRange(UMattachments);
                        }
                        
                    }
                    return attachments;
                    break;

                    #endregion
                case "CONTACT":
                    #region Contact
                    for (int i = 0; i < moduleArray.Count; i++)
                    {
                        if (moduleArray[i] == "MH")
                        {
                            List<AuthorizationContactViewModel> MHcontacts = new List<AuthorizationContactViewModel>();
                            this.baseURL = ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString();
                            Task<string> contactsList = Task.Run(async () =>
                            {
                                string msg = await serviceUtil.GetDataFromService(baseURL,"api/MemberService/GetAllContactsRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberID);
                                return msg;
                            });
                            if (contactsList.Result != null)
                            {
                                MHcontacts = JsonConvert.DeserializeObject<List<AuthorizationContactViewModel>>(contactsList.Result);
                                foreach (var contact in MHcontacts)
                                {
                                    contact.ModuleName = "MH";
                                }
                            }
                            contacts.AddRange(MHcontacts);
                        }
                        else if (moduleArray[i] == "UM")
                        {
                            List<AuthorizationContactViewModel> UMcontacts = new List<AuthorizationContactViewModel>();
                            this.baseURL = ConfigurationManager.AppSettings["UMService"].ToString();
                            Task<string> attachmentsList = Task.Run(async () =>
                            {
                                string msg = await serviceUtil.GetDataFromService(baseURL, "api/Member/GetAllContactsBySubscriberID?subscriberID=" + SubscriberID);
                                return msg;
                            });
                            if (attachmentsList.Result != null)
                            {
                                UMcontacts = JsonConvert.DeserializeObject<List<AuthorizationContactViewModel>>(attachmentsList.Result);
                                foreach (var contact in UMcontacts)
                                {
                                    contact.ModuleName = "UM";
                                }
                            }
                            contacts.AddRange(UMcontacts);
                        }

                    }
                    
                    return contacts;

                    #endregion
                    break;
                case "NOTE":
                    #region Note
                    for (int i = 0; i < moduleArray.Count; i++)
                    {
                        if (moduleArray[i] == "UM")
                        {
                            List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel> UMnotes = new List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>();
                            this.baseURL = ConfigurationManager.AppSettings["UMService"].ToString();
                            Task<string> contactsList = Task.Run(async () =>
                            {
                                string msg = await serviceUtil.GetDataFromService(baseURL,"api/Member/GetAllNotesBySubscriberID?subscriberID=" + SubscriberID);
                                return msg;
                            });
                            if (contactsList.Result != null)
                            {
                                UMnotes = JsonConvert.DeserializeObject<List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>>(contactsList.Result);
                                foreach (var note in UMnotes)
                                {
                                    note.ModuleName = "UM";
                                }
                            }
                            notes.AddRange(UMnotes);
                        }
                        else if (moduleArray[i] == "MH")
                        {
                            List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel> MHnotes = new List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>();
                            this.baseURL = ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString();
                            Task<string> attachmentsList = Task.Run(async () =>
                            {
                                string msg = await serviceUtil.GetDataFromService(baseURL, "api/MemberService/GetAllNotesRelatedToMemberBySubscriberID?SubScriberID=" + SubscriberID);
                                return msg;
                            });
                            if (attachmentsList.Result != null)
                            {
                                MHnotes = JsonConvert.DeserializeObject<List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>>(attachmentsList.Result);
                                foreach (var note in MHnotes)
                                {
                                    note.ModuleName = "MH";
                                }
                            }
                            notes.AddRange(MHnotes);
                        }

                    }
                    return notes;
                    #endregion
                    break;
                default:
                    return null;
                    break;
            }
        }


        public object GetFilteredServiceDataBySubsriberID(string module, string serviceName, int ID)
        {
            PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel attachment = new PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel();
            AuthorizationContactViewModel contact = new AuthorizationContactViewModel();
            PortalTemplate.Areas.Portal.Models.Note.NoteViewModel note = new PortalTemplate.Areas.Portal.Models.Note.NoteViewModel();

            switch (module)
            {
                case "ATTACHMENT":
                    #region Attachment
                    if (serviceName == "MH")
                    {
                        this.baseURL = ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString();
                        Task<string> attachmentsList = Task.Run(async () =>
                        {
                            string msg = await serviceUtil.GetDataFromService(baseURL, "api/MemberService/GetAllAttachementsRelatedToMemberByID?AttachmentID=" + ID);
                            return msg;
                        });
                        if (attachmentsList.Result != null)
                        {
                            attachment = JsonConvert.DeserializeObject<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel>(attachmentsList.Result);
                        }

                    }
                    else if (serviceName == "UM")
                    {

                    }
                    return attachment;
                    break;
                    #endregion

                case "CONTACT":
                    #region Contact
                    if (serviceName == "MH")
                    {
                        this.baseURL = ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString();
                        Task<string> attachmentsList = Task.Run(async () =>
                        {
                            string msg = await serviceUtil.GetDataFromService(baseURL, "api/MemberService/GetAllContactsRelatedToMemberByID?ContactID=" + ID);
                            return msg;
                        });
                        if (attachmentsList.Result != null)
                        {
                            contact = JsonConvert.DeserializeObject<AuthorizationContactViewModel>(attachmentsList.Result);
                        }
                    }
                    return contact;

                    #endregion

                case "NOTE":
                    if (serviceName == "MH")
                    {
                        this.baseURL = ConfigurationManager.AppSettings["MemberServiceWebAPIURL"].ToString();
                        Task<string> noteList = Task.Run(async () =>
                        {
                            string msg = await serviceUtil.GetDataFromService(baseURL, "api/MemberService/GetAllNotesRelatedToMemberByID?NoteID=" + ID);
                            return msg;
                        });
                        if (noteList.Result != null)
                        {
                            note = JsonConvert.DeserializeObject<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel>(noteList.Result);
                        }
                    }
                    return note;

                default:
                    return null;
                    break;
            }
        }
    }
}