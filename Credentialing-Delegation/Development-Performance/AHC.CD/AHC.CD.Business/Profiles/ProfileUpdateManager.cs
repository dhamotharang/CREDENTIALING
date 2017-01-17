using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.MasterData;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.CredentialingRequestTracker;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using AHC.CD.Exceptions.Credentialing;
using AHC.CD.Resources.Document;
using AHC.CD.Resources.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace AHC.CD.Business.Profiles
{
    internal class ProfileUpdateManager : IProfileUpdateManager
    {
        private IProfileManager profileManager = null;
        ProfileDocumentManager profileDocumentManager = null;
        IDocumentsManager documentManager = null;
        private IRepositoryManager repositoryManager = null;
        IProfileRepository profileRepository = null;
        private IMasterDataManager masterDataManager = null;

        IUnitOfWork uow = null;

        public ProfileUpdateManager(IUnitOfWork uow, IProfileManager profileManager, IDocumentsManager documentManager, IRepositoryManager repositoryManager, IMasterDataManager masterDataManager)
        {
            this.uow = uow;
            this.profileRepository = uow.GetProfileRepository();
            this.profileManager = profileManager;
            this.documentManager = documentManager;
            this.repositoryManager = repositoryManager;
            this.profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
            this.masterDataManager = masterDataManager;
        }

        public List<ProfileUpdatesTracker> GetAllUpdates()
        {
            List<ProfileUpdatesTracker> updates = new List<ProfileUpdatesTracker>();
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
            var updatedData = trackerRepo.GetAll("Profile.OtherIdentificationNumber, Profile.PersonalDetail").ToList();

            var pendingUpdates = updatedData.Where(p => p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString() || p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.OnHold.ToString()).ToList();
            //var updatesGrouped = from profileUpdates in pendingUpdates
            //                     group profileUpdates by
            //                     new
            //                     {
            //                         profileUpdates.Section,
            //                         profileUpdates.SubSection,
            //                         profileUpdates.ApprovalStatus,
            //                         profileUpdates.Modification,
            //                         profileUpdates.ProfileId
            //                     }
            //                         into g
            //                         select new ProfileUpdatesTracker
            //                         {
            //                             ProfileUpdatesTrackerId = g.Select(c => c.ProfileUpdatesTrackerId).Last(),
            //                             Section = g.Key.Section,
            //                             SubSection = g.Key.SubSection,
            //                             Url = g.Select(c => c.Url).Last(),
            //                             ApprovalStatus = g.Key.ApprovalStatus,
            //                             Modification = g.Select(c => c.Modification).Last(),
            //                             RejectionReason = g.Select(c => c.RejectionReason).Last(),
            //                             ProfileId = g.Key.ProfileId,
            //                             Profile = g.Select(c => c.Profile).Last(),
            //                             LastModifiedBy = g.Select(c => c.LastModifiedBy).Last(),
            //                             LastModifiedDate = g.Select(c => c.LastModifiedDate).Last(),
            //                             NewData = g.Select(c => c.NewData).Last(),
            //                             oldData = g.Select(c => c.oldData).Last(),
            //                             NewConvertedData = g.Select(c => c.NewConvertedData).Last()
            //                         };
            var updatesGrouped = from profileUpdates in pendingUpdates
                                 group profileUpdates by
                                 new
                                 {
                                     profileUpdates.Section,
                                     profileUpdates.SubSection,
                                     profileUpdates.ApprovalStatus,
                                     profileUpdates.Modification,
                                     profileUpdates.ProfileId,
                                     UpdatedDataId = profileUpdates.RespectiveObjectId
                                 }
                                     into g
                                     select new ProfileUpdatesTracker
                                     {
                                         ProfileUpdatesTrackerId = g.Select(c => c.ProfileUpdatesTrackerId).Last(),
                                         Section = g.Key.Section,
                                         SubSection = g.Key.SubSection,
                                         Url = g.Select(c => c.Url).Last(),
                                         ApprovalStatus = g.Key.ApprovalStatus,
                                         Modification = g.Select(c => c.Modification).Last(),
                                         RejectionReason = g.Select(c => c.RejectionReason).Last(),
                                         ProfileId = g.Key.ProfileId,
                                         Profile = g.Select(c => c.Profile).Last(),
                                         LastModifiedBy = g.Select(c => c.LastModifiedBy).Last(),
                                         LastModifiedDate = g.Select(c => c.LastModifiedDate).Last(),
                                         NewData = g.Select(c => c.NewData).Last(),
                                         oldData = g.Select(c => c.oldData).Last(),
                                         NewConvertedData = g.Select(c => c.NewConvertedData).Last(),
                                         RespectiveObjectId = g.Select(c => c.RespectiveObjectId).Last()
                                     };
            updates = updatesGrouped.ToList();
            //var onHoldUpdates = updatedData.Where(p => p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.OnHold.ToString()).ToList();

            //updates = pendingUpdates;

            //foreach (var item in onHoldUpdates)
            //{
            //    updates.Add(item);
            //}
            //var updatedData = trackerRepo.Get(d => d.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString() &&
            //    d.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.OnHold.ToString(),
            //    "Profile.OtherIdentificationNumber, Profile.PersonalDetail").ToList();


            return updates;
        }

        public List<ProfileUpdatesTracker> GetAllUpdatesHistory()
        {
            List<ProfileUpdatesTracker> rejectedData = new List<ProfileUpdatesTracker>();
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
            var updatedData = trackerRepo.GetAll("Profile.OtherIdentificationNumber, Profile.PersonalDetail").ToList();
            rejectedData = updatedData.Where(p => p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString() || p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Approved.ToString()).ToList();


            return rejectedData;
        }

        public List<ProfileUpdatesTracker> GetUpdatesHistoryById(int profileId)
        {
            List<ProfileUpdatesTracker> rejectedData = new List<ProfileUpdatesTracker>();
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
            var updatedData = trackerRepo.GetAll("Profile.OtherIdentificationNumber, Profile.PersonalDetail").ToList();
            rejectedData = updatedData.Where(p => (p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Rejected.ToString() || p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Approved.ToString()) && p.ProfileId == profileId).ToList();

            return rejectedData;
        }

        public List<ProfileUpdatesTracker> GetUpdatesById(int profileId)
        {
            try
            {
                List<ProfileUpdatesTracker> updates = new List<ProfileUpdatesTracker>();
                var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
                var pendingUpdates = trackerRepo.GetAll().Where(c => c.ProfileId == profileId).ToList();


                var updatesGrouped = from profileUpdates in pendingUpdates
                                     group profileUpdates by
                                     new
                                     {
                                         profileUpdates.Section,
                                         profileUpdates.SubSection,
                                         profileUpdates.ApprovalStatus,
                                         profileUpdates.Modification,
                                         profileUpdates.ProfileId,
                                         UpdatedDataId = profileUpdates.RespectiveObjectId
                                     }
                                         into g  
                                         select new ProfileUpdatesTracker
                                         {
                                             ProfileUpdatesTrackerId = g.Select(c => c.ProfileUpdatesTrackerId).Last(),
                                             Section = g.Key.Section,
                                             SubSection = g.Key.SubSection,
                                             Url = g.Select(c => c.Url).Last(),
                                             ApprovalStatus = g.Key.ApprovalStatus,
                                             Modification = g.Select(c => c.Modification).Last(),
                                             RejectionReason = g.Select(c => c.RejectionReason).Last(),
                                             ProfileId = g.Key.ProfileId,
                                             Profile = g.Select(c => c.Profile).Last(),
                                             LastModifiedBy = g.Select(c => c.LastModifiedBy).Last(),
                                             LastModifiedDate = g.Select(c => c.LastModifiedDate).Last(),
                                             NewData = g.Select(c => c.NewData).Last(),
                                             oldData = g.Select(c => c.oldData).Last(),
                                             NewConvertedData = g.Select(c => c.NewConvertedData).Last(),
                                             RespectiveObjectId = g.Select(c => c.RespectiveObjectId).Last()
                                         };
                
                updates = updatesGrouped.Where(x=>x.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.OnHold.ToString() || x.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString()).ToList();


                return updates;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProfileUpdatesTracker> GetUpdatesForProfileById(int profileId)
        {
            try
            {
                List<ProfileUpdatesTracker> updates = new List<ProfileUpdatesTracker>();
                var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
                var pendingUpdates = trackerRepo.GetAll().Where(c => c.ProfileId == profileId && (c.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.OnHold.ToString() || c.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString())).ToList();


                var updatesGrouped = from profileUpdates in pendingUpdates
                                     group profileUpdates by
                                     new
                                     {
                                         profileUpdates.Section,
                                         profileUpdates.SubSection,
                                         profileUpdates.ApprovalStatus,
                                         profileUpdates.Modification,
                                         profileUpdates.ProfileId
                                     }
                                         into g
                                         select new ProfileUpdatesTracker
                                         {
                                             ProfileUpdatesTrackerId = g.Select(c => c.ProfileUpdatesTrackerId).Last(),
                                             Section = g.Key.Section,
                                             SubSection = g.Key.SubSection,
                                             Url = g.Select(c => c.Url).Last(),
                                             ApprovalStatus = g.Key.ApprovalStatus,
                                             Modification = g.Select(c => c.Modification).Last(),
                                             RejectionReason = g.Select(c => c.RejectionReason).Last(),
                                             ProfileId = g.Key.ProfileId,
                                             Profile = g.Select(c => c.Profile).Last(),
                                             LastModifiedBy = g.Select(c => c.LastModifiedBy).Last(),
                                             LastModifiedDate = g.Select(c => c.LastModifiedDate).Last(),
                                             NewData = g.Select(c => c.NewData).Last(),
                                             oldData = g.Select(c => c.oldData).Last(),
                                             NewConvertedData = g.Select(c => c.NewConvertedData).Last()
                                         };
                updates = updatesGrouped.ToList();


                return updates;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProfileUpdatesTracker> GetUpdatesByIdForAllStatus(int profileId)
        {
            try
            {
                List<ProfileUpdatesTracker> updates = new List<ProfileUpdatesTracker>();
                var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
                var updatedData = trackerRepo.GetAll().Where(c => c.ProfileId == profileId).ToList();

                return updatedData;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public List<BusinessModels.ProfileUpdates.ProfileUpdatedData> GetDataById(int trackerId)
        {
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();



            List<ProfileUpdatedData> updateFinalOldDataList = new List<ProfileUpdatedData>();
            List<ProfileUpdatedData> updateFinalNewDataList = new List<ProfileUpdatedData>();
            List<ProfileUpdatedData> finalDataList = new List<ProfileUpdatedData>();
            List<ProfileUpdatedData> filteredFinalDataList = new List<ProfileUpdatedData>();


            var updatedData = trackerRepo.Find(p => p.ProfileUpdatesTrackerId == trackerId);

            switch (updatedData.SubSection)
            {
                case "Federal DEA": return filteredFinalDataList = ConstructChangesForFederalDEA(updatedData.oldData, updatedData.NewConvertedData);
                case "Profile Disclosure": return filteredFinalDataList = ConstructChangesForDisclosure(updatedData.oldData, updatedData.NewConvertedData);
                case "Personal Detail": return filteredFinalDataList = ConstructChangesForPersonalDetail(updatedData.oldData, updatedData.NewConvertedData);
                case "Language Info": return filteredFinalDataList = ConstructChangesForLanguages(updatedData.oldData, updatedData.NewConvertedData);
                case "Contact Detail": return filteredFinalDataList = ConstructChangesForContact(updatedData.oldData, updatedData.NewConvertedData);
                case "Facility": return filteredFinalDataList = ConstructChangesForPracticeLocation(updatedData.oldData, updatedData.NewConvertedData);
                case "Office Hours": return filteredFinalDataList = ConstructChangesForOfficeHours(updatedData.oldData, updatedData.NewConvertedData);
                case "Open Practice Status": return filteredFinalDataList = ConstructChangesForPracitceStatus(updatedData.oldData, updatedData.NewConvertedData);
                case "CoveringColleague": return filteredFinalDataList = ConstructChangesForCoveringCollegues(updatedData.oldData, updatedData.NewConvertedData);
                case "Payment and Remittance": return filteredFinalDataList = ConstructChangesForPaymentAndRemittance(updatedData.oldData, updatedData.NewConvertedData);
                default: break;
            }

            dynamic oldData = new JavaScriptSerializer().Deserialize<dynamic>(updatedData.oldData);
            dynamic newData = new JavaScriptSerializer().Deserialize<dynamic>(updatedData.NewConvertedData);


            foreach (var old in oldData)
            {
                foreach (var val in newData)
                {
                    if (old.Key == val.Key)
                    {
                        if (old.Value is System.Collections.ICollection && val.Value is System.Collections.ICollection)
                        {
                            var DataList = ConstructDataForCCo(old.Value, val.Value, ref updateFinalOldDataList);
                        }
                        else
                        {
                            ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                            profileNewData.FieldName = old.Key;

                            if (old.Value != null && old.Value.GetType().Equals(typeof(DateTime)))
                            {
                                DateTime oldDate = old.Value.ToLocalTime();
                                profileNewData.OldValue = ConvertToDateString(oldDate);
                            }
                            else
                                profileNewData.OldValue = old.Value != null ? old.Value.ToString() : null;


                            if (val.Value != null && val.Value.GetType().Equals(typeof(DateTime)))
                            {
                                DateTime newDate = val.Value.ToLocalTime();
                                profileNewData.NewValue = ConvertToDateString(newDate);
                            }
                            else
                                profileNewData.NewValue = val.Value != null ? val.Value.ToString() : null;

                            updateFinalOldDataList.Add(profileNewData);
                        }
                    }

                }
            }

            int flag = 0;
            if (updatedData.SubSection.Equals("Medicaid Information"))
            {
                foreach (var item in updateFinalOldDataList)
                {
                    if (item.FieldName.Equals("State"))
                    {
                        if (item.NewValue.Contains("?") && item.OldValue.Contains("?"))
                        {
                            flag = 3;
                            break;
                        }
                        else
                        {
                            if (item.OldValue.Contains("?"))
                            {
                                flag = 1;
                                break;
                            }
                            if (item.NewValue.Contains("?"))
                            {
                                flag = 2;
                                break;
                            }
                        }
                    }
                }
                if (flag == 1)
                {
                    foreach (var item in updateFinalOldDataList)
                    {
                        item.OldValue = null;
                    }
                }
                else if (flag == 2)
                {
                    foreach (var item in updateFinalOldDataList)
                    {
                        item.NewValue = null;
                    }
                }
                else if (flag == 3)
                {
                    foreach (var item in updateFinalOldDataList)
                    {
                        item.NewValue = null;
                        item.OldValue = null;
                    }
                }
            }

            foreach (var item in updateFinalOldDataList)
            {


                if (item != null && item.NewValue != item.OldValue && (!item.FieldName.Contains("Modified")) && (!item.FieldName.Contains("History")))
                {
                    if (item.FieldName == "DateOfBirth")
                    {
                        DateTime oldDate = Convert.ToDateTime(item.OldValue.Split(' ')[0]);
                        DateTime newDate = Convert.ToDateTime(item.NewValue.Split(' ')[0]);
                        item.OldValue = ConvertToDateString(oldDate);
                        item.NewValue = ConvertToDateString(newDate);
                    }

                    if (item.FieldName != "GraduateType" && item.FieldName != "ResidencyInternshipProgramType" && item.FieldName != "CurrentlyWorking" && item.FieldName != "PaymentAndRemittancePerson" && item.FieldName != "HospitalPrivilegeDetails" && item.FieldName != "Status" && item.FieldName != "StatusType" && !item.FieldName.Contains("IsCurrentlyPracting") && !item.FieldName.Contains("VisaInfo") && !item.FieldName.Contains("SocialSecurityNumber") && !item.FieldName.Contains("Option") && !item.FieldName.Contains("CountryCode") && !item.FieldName.Contains("PreferenceType") && !item.FieldName.Contains("Stored"))
                        filteredFinalDataList.Add(item);
                }
            }

            return filteredFinalDataList;
        }

        public List<ProfileUpdatesTracker> GetUpdatesByTrackerId(int trackerId, string status, string modificationType)
        {
            List<ProfileUpdatesTracker> updates = new List<ProfileUpdatesTracker>();
            ProfileUpdatesTracker update = new ProfileUpdatesTracker();
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
            update = trackerRepo.GetAll().Where(c => c.ProfileUpdatesTrackerId == trackerId).FirstOrDefault();
            updates = trackerRepo.GetAll().Where(c => c.Section.Equals(update.Section) && c.RespectiveObjectId == update.RespectiveObjectId && c.SubSection.Equals(update.SubSection) && c.ApprovalStatus == status && c.Modification == modificationType && c.ProfileId == update.ProfileId).ToList();
            return updates;
        }


        public async Task SetApproval(List<ApprovalSubmission> trackers, string userAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
            foreach (var tracker in trackers)
            {
                var updatedData = trackerRepo.Find(p => p.ProfileUpdatesTrackerId == tracker.TrackerId);

                updatedData.ApprovalStatus = tracker.ApprovalStatus;
                updatedData.RejectionReason = tracker.RejectionReason;
                updatedData.LastModifiedBy = user.CDUserID;
                updatedData.LastModifiedDate = DateTime.Now;

                trackerRepo.Update(updatedData);

                trackerRepo.Save();
            }
        }


        public int AddProfileUpdateForProvider<TObject, T>(TObject t, T t1, ProfileUpdateTrackerBusinessModel tracker)
            where TObject : class
            where T : class
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == tracker.userAuthId);
                var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
                var a = t1.GetType();
                if (a.FullName == "AHC.CD.Entities.MasterProfile.PracticeLocation.ProviderPracticeOfficeHour")
                {
                    var sectionWiseRepoForOfficeHour = uow.GetGenericRepository<ProviderPracticeOfficeHour>();
                    //var oldData = sectionWiseRepo.Find(tracker.objId, tracker.IncludeProperties);
                    var oldDataForOfficeHour = sectionWiseRepoForOfficeHour.Find(x => x.PracticeOfficeHourID == tracker.objId);
                    var profileUpdateForOfficeHour = ConstructTrackerData(t, t1, tracker, user.CDUserID);

                    profileUpdateForOfficeHour.oldData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(oldDataForOfficeHour);
                    trackerRepo.Create(profileUpdateForOfficeHour);
                    trackerRepo.Save();
                    return profileUpdateForOfficeHour.ProfileUpdatesTrackerId;
                }
                var sectionWiseRepo = uow.GetGenericRepository<T>();
                //var oldData = sectionWiseRepo.Find(tracker.objId, tracker.IncludeProperties);
                var oldData = sectionWiseRepo.Find(tracker.objId);
                var profileUpdate = ConstructTrackerData(t, t1, tracker, user.CDUserID);

                profileUpdate.oldData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(oldData);

                trackerRepo.Create(profileUpdate);

                trackerRepo.Save();

                return profileUpdate.ProfileUpdatesTrackerId;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private ProfileUpdatesTracker ConstructTrackerData<TObject, T>(TObject t, T t1, ProfileUpdateTrackerBusinessModel trackerBuss, int userId)
            where TObject : class
            where T : class
        {
            ProfileUpdatesTracker tracker = new ProfileUpdatesTracker();
            try
            {
                //string updatedData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(t);
                string updatedData = JsonConvert.SerializeObject(t, new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
                tracker.NewData = updatedData;
                string convertedData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(t1);
                //string convertedData = JsonConvert.SerializeObject(t1, new JsonSerializerSettings
                //{
                //    DateTimeZoneHandling = DateTimeZoneHandling.Local
                //});
                tracker.NewConvertedData = convertedData;
                tracker.ApprovalStatus = AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString();
                tracker.Modification = trackerBuss.ModificationType;
                tracker.LastModifiedBy = userId;
                tracker.Section = trackerBuss.Section;
                tracker.SubSection = trackerBuss.SubSection;
                tracker.ProfileId = trackerBuss.ProfileId;
                tracker.Url = trackerBuss.url;
                tracker.RespectiveObjectId = trackerBuss.objId;
                //if (tracker.Documents == null)
                //{
                //    tracker.Documents = new List<ProfileDocumentUpdateTracker>();
                //}
                //foreach (var document in trackerBuss.Documents)
                //{
                //    ProfileDocumentUpdateTracker dataProfileDocumentUpdateTracker = AutoMapper.Mapper.Map<ProfileDocumentUpdateTrackerBusinessModel, ProfileDocumentUpdateTracker>(document);
                //    tracker.Documents.Add(dataProfileDocumentUpdateTracker);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tracker;
        }

        //private List<ProfileUpdatedData> ConstructDataForPracticeLocationOtherInfo(dynamic val, dynamic newVal)
        //{ 

        //}

        private List<ProfileUpdatedData> ConstructDataForCCo(dynamic val, dynamic newVal, ref List<ProfileUpdatedData> updateDataList)
        {
            //List<ProfileUpdatedData> updateDataList = new List<ProfileUpdatedData>();            

            foreach (var item in val)
            {
                foreach (var data in newVal)
                {
                    if (item is Dictionary<string, object> && data is Dictionary<string, object>)
                    {
                        foreach (var i in item)
                        {
                            foreach (var d in data)
                            {
                                if (i.Key == d.Key)
                                {
                                    if (i.Value is System.Collections.ICollection && d.Value is System.Collections.ICollection)
                                    {
                                        var DataList = ConstructDataForCCo(i.Value, d.Value, ref updateDataList);
                                    }
                                    else
                                    {
                                        ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                                        profileNewData.FieldName = i.Key;
                                        if (i.Value != null && i.Value.GetType().Equals(typeof(DateTime)))
                                        {
                                            DateTime oldDate = i.Value.ToLocalTime();
                                            profileNewData.OldValue = ConvertToDateString(oldDate);
                                        }
                                        else
                                            profileNewData.OldValue = i.Value != null ? i.Value.ToString() : null;

                                        if (d.Value != null && d.Value.GetType().Equals(typeof(DateTime)))
                                        {
                                            DateTime newDate = d.Value.ToLocalTime();
                                            profileNewData.NewValue = ConvertToDateString(newDate);
                                        }
                                        else
                                            profileNewData.NewValue = d.Value != null ? d.Value.ToString() : null;

                                        updateDataList.Add(profileNewData);
                                    }
                                }

                            }
                        }

                    }
                    else if (item.Value is System.Collections.ICollection && data.Value is System.Collections.ICollection)
                    {
                        if (item.Key == data.Key)
                        {
                            var DataList = ConstructDataForCCo(item.Value, data.Value, ref updateDataList);
                        }
                    }
                    else
                    {
                        if (item.Key == data.Key)
                        {
                            ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                            profileNewData.FieldName = item.Key;
                            if (item.Value != null && item.Value.GetType().Equals(typeof(DateTime)))
                            {
                                DateTime oldDate = item.Value.ToLocalTime();
                                profileNewData.OldValue = ConvertToDateString(oldDate);
                            }
                            else
                                profileNewData.OldValue = item.Value != null ? item.Value.ToString() : null;

                            if (data.Value != null && data.Value.GetType().Equals(typeof(DateTime)))
                            {
                                DateTime newDate = data.Value.ToLocalTime();
                                profileNewData.NewValue = ConvertToDateString(newDate);
                            }
                            else
                                profileNewData.NewValue = data.Value != null ? data.Value.ToString() : null;

                            updateDataList.Add(profileNewData);
                        }

                    }

                }

            }

            return updateDataList;
        }

        private List<ProfileUpdatedData> ConstructChangesForFederalDEA(dynamic oldDEAData, dynamic newDEAData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();

            FederalDEAInformation oldData = new JavaScriptSerializer().Deserialize<FederalDEAInformation>(oldDEAData);
            FederalDEAInformation newData = new JavaScriptSerializer().Deserialize<FederalDEAInformation>(newDEAData);

            if (oldData.DEANumber != newData.DEANumber)
            {
                ProfileUpdatedData DEA = new ProfileUpdatedData();
                DEA.FieldName = "DEANumber";
                DEA.OldValue = oldData.DEANumber;
                DEA.NewValue = newData.DEANumber;
                updatedList.Add(DEA);
            }
            if (oldData.ExpiryDate != newData.ExpiryDate)
            {
                ProfileUpdatedData DEA = new ProfileUpdatedData();
                DEA.FieldName = "ExpiryDate";

                if (oldData.ExpiryDate != null)
                {
                    DateTime oldDate = oldData.ExpiryDate.Value.ToLocalTime();
                    DEA.OldValue = ConvertToDateString(oldDate);
                }
                if (newData.ExpiryDate != null)
                {
                    DateTime newDate = newData.ExpiryDate.Value.ToLocalTime();
                    DEA.NewValue = ConvertToDateString(newDate);
                }

                updatedList.Add(DEA);
            }
            if (oldData.IsInGoodStanding != newData.IsInGoodStanding)
            {
                ProfileUpdatedData DEA = new ProfileUpdatedData();
                DEA.FieldName = "GoodStandingYesNoOption";
                DEA.OldValue = oldData.IsInGoodStanding;
                DEA.NewValue = newData.IsInGoodStanding;
                updatedList.Add(DEA);
            }
            if (oldData.IssueDate != newData.IssueDate)
            {
                ProfileUpdatedData DEA = new ProfileUpdatedData();
                DEA.FieldName = "IssueDate";

                if (oldData.IssueDate != null)
                {
                    DateTime oldDate = oldData.IssueDate.Value.ToLocalTime();
                    DEA.OldValue = ConvertToDateString(oldDate);
                }
                if (newData.IssueDate != null)
                {
                    DateTime newDate = newData.IssueDate.Value.ToLocalTime();
                    DEA.NewValue = ConvertToDateString(newDate);
                }

                updatedList.Add(DEA);
            }
            if (oldData.StateOfReg != newData.StateOfReg)
            {
                ProfileUpdatedData DEA = new ProfileUpdatedData();
                DEA.FieldName = "StateOfReg";
                DEA.OldValue = oldData.StateOfReg;
                DEA.NewValue = newData.StateOfReg;
                updatedList.Add(DEA);
            }
            if (oldData.DEALicenceCertPath != newData.DEALicenceCertPath)
            {
                ProfileUpdatedData DEA = new ProfileUpdatedData();
                DEA.FieldName = "DocumentPath";
                DEA.OldValue = oldData.DEALicenceCertPath;
                DEA.NewValue = newData.DEALicenceCertPath;
                updatedList.Add(DEA);
            }

            int count = oldData.DEAScheduleInfoes.Count;

            for (int i = 0; i < count; i++)
            {
                if (oldData.DEAScheduleInfoes.ElementAt(i).IsEligible != newData.DEAScheduleInfoes.ElementAt(i).IsEligible)
                {
                    ProfileUpdatedData DEA = new ProfileUpdatedData();
                    DEA.FieldName = oldData.DEAScheduleInfoes.ElementAt(i).DEASchedule.ScheduleTitle + "-" + oldData.DEAScheduleInfoes.ElementAt(i).DEASchedule.ScheduleTypeTitle;
                    DEA.OldValue = "Eligibility : " + oldData.DEAScheduleInfoes.ElementAt(i).IsEligible;
                    DEA.NewValue = "Eligibility : " + newData.DEAScheduleInfoes.ElementAt(i).IsEligible;
                    updatedList.Add(DEA);
                }
            }




            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForDisclosure(dynamic oldDisclosureData, dynamic newDisclosureData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            ProfileDisclosure data = new JavaScriptSerializer().Deserialize<ProfileDisclosure>(oldDisclosureData);
            var disclosureRepo = uow.GetGenericRepository<ProfileDisclosure>();

            ProfileDisclosure oldData = disclosureRepo.Find(d => d.ProfileDisclosureID == data.ProfileDisclosureID, "ProfileDisclosureQuestionAnswers, ProfileDisclosureQuestionAnswers.Question");

            ProfileDisclosure newData = new JavaScriptSerializer().Deserialize<ProfileDisclosure>(newDisclosureData);


            int count = oldData.ProfileDisclosureQuestionAnswers.Count;

            for (int i = 0; i < count; i++)
            {
                if (oldData.ProfileDisclosureQuestionAnswers.ElementAt(i).ProviderDisclousreAnswer != newData.ProfileDisclosureQuestionAnswers.ElementAt(i).ProviderDisclousreAnswer)
                {
                    ProfileUpdatedData disclosure = new ProfileUpdatedData();
                    disclosure.FieldName = oldData.ProfileDisclosureQuestionAnswers.ElementAt(i).Question.Title;

                    if (oldData.ProfileDisclosureQuestionAnswers.ElementAt(i).Reason != null)
                        disclosure.OldValue = oldData.ProfileDisclosureQuestionAnswers.ElementAt(i).ProviderDisclousreAnswer + "(Reason : " + oldData.ProfileDisclosureQuestionAnswers.ElementAt(i).Reason + ")";
                    else
                        disclosure.OldValue = oldData.ProfileDisclosureQuestionAnswers.ElementAt(i).ProviderDisclousreAnswer;

                    if (newData.ProfileDisclosureQuestionAnswers.ElementAt(i).Reason != null)
                        disclosure.NewValue = newData.ProfileDisclosureQuestionAnswers.ElementAt(i).ProviderDisclousreAnswer + "(Reason : " + newData.ProfileDisclosureQuestionAnswers.ElementAt(i).Reason + ")";
                    else
                        disclosure.NewValue = newData.ProfileDisclosureQuestionAnswers.ElementAt(i).ProviderDisclousreAnswer;

                    updatedList.Add(disclosure);
                }
            }




            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForPersonalDetail(dynamic oldPersonalData, dynamic newPersonalData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            var personalRepo = uow.GetGenericRepository<PersonalDetail>();
            PersonalDetail data = new JavaScriptSerializer().Deserialize<PersonalDetail>(oldPersonalData);
            PersonalDetail oldData = personalRepo.Find(d => d.PersonalDetailID == data.PersonalDetailID, "ProviderLevel, ProviderTitles, ProviderTitles.ProviderType");


            PersonalDetail newData = new JavaScriptSerializer().Deserialize<PersonalDetail>(newPersonalData);

            if (oldData.FirstName != newData.FirstName)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "FirstName";
                personal.OldValue = oldData.FirstName;
                personal.NewValue = newData.FirstName;
                updatedList.Add(personal);
            }
            if (oldData.MiddleName != newData.MiddleName)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "MiddleName";
                personal.OldValue = oldData.MiddleName;
                personal.NewValue = newData.MiddleName;
                updatedList.Add(personal);
            }
            if (oldData.LastName != newData.LastName)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "LastName";
                personal.OldValue = oldData.LastName;
                personal.NewValue = newData.LastName;
                updatedList.Add(personal);
            }
            if (oldData.MaidenName != newData.MaidenName)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "MaidenName";
                personal.OldValue = oldData.MaidenName;
                personal.NewValue = newData.MaidenName;
                updatedList.Add(personal);
            }
            if (oldData.Salutation != newData.Salutation)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "Salutation";
                personal.OldValue = oldData.Salutation;
                personal.NewValue = newData.Salutation;
                updatedList.Add(personal);
            }
            if (oldData.Suffix != newData.Suffix)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "Suffix";
                personal.OldValue = oldData.Suffix;
                personal.NewValue = newData.Suffix;
                updatedList.Add(personal);
            }
            if (oldData.Gender != newData.Gender)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "Gender";
                personal.OldValue = oldData.Gender;
                personal.NewValue = newData.Gender;
                updatedList.Add(personal);
            }
            if (oldData.MaritalStatus != newData.MaritalStatus)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "MaritalStatus";
                personal.OldValue = oldData.MaritalStatus;
                personal.NewValue = newData.MaritalStatus;
                updatedList.Add(personal);
            }
            if (oldData.SpouseName != newData.SpouseName)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "SpouseName";
                personal.OldValue = oldData.SpouseName;
                personal.NewValue = newData.SpouseName;
                updatedList.Add(personal);
            }
            if (oldData.ProviderLevelID != newData.ProviderLevelID)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "ProviderLevelID";
                personal.OldValue = oldData.ProviderLevelID.ToString();
                personal.NewValue = newData.ProviderLevelID.ToString();
                updatedList.Add(personal);
            }

            var titleRepo = uow.GetGenericRepository<ProviderType>();
            var ProviderTitles = titleRepo.GetAll();

            int oldCount = oldData.ProviderTitles.Count;
            int newCount = newData.ProviderTitles.Count;
            string oldValue = "";
            if (oldCount > 0)
            {
                if (oldData.ProviderTitles.ElementAt(0).ProviderType.Status == "Active")
                    oldValue = oldData.ProviderTitles.ElementAt(0).ProviderType.Title;

                for (int i = 1; i < oldCount; i++)
                {
                    if (oldData.ProviderTitles.ElementAt(i).ProviderType.Status == "Active")
                        oldValue = oldValue + "," + oldData.ProviderTitles.ElementAt(i).ProviderType.Title;

                }
            }

            string newValue = "";
            if (newCount > 0)
            {
                foreach (var item in ProviderTitles)
                {
                    if (item.ProviderTypeID == newData.ProviderTitles.ElementAt(0).ProviderTypeId && newData.ProviderTitles.ElementAt(0).Status == "Active")
                    {
                        newValue = item.Title;
                    }
                }

                for (int i = 1; i < newCount; i++)
                {
                    foreach (var item in ProviderTitles)
                    {
                        if (newValue != "")
                        {
                            if (item.ProviderTypeID == newData.ProviderTitles.ElementAt(i).ProviderTypeId && newData.ProviderTitles.ElementAt(i).Status == "Active")
                            {
                                newValue = newValue + "," + item.Title;
                            }
                        }
                        else
                        {
                            if (item.ProviderTypeID == newData.ProviderTitles.ElementAt(i).ProviderTypeId && newData.ProviderTitles.ElementAt(i).Status == "Active")
                            {
                                newValue = item.Title;
                            }
                        }
                    }

                }
            }

            if (oldValue != newValue)
            {
                ProfileUpdatedData personal = new ProfileUpdatedData();
                personal.FieldName = "ProviderTitles";
                personal.OldValue = oldValue;
                personal.NewValue = newValue;
                updatedList.Add(personal);
            }



            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForLanguages(dynamic oldLanguageData, dynamic newLanguageData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            LanguageInfo oldData = new JavaScriptSerializer().Deserialize<LanguageInfo>(oldLanguageData);
            LanguageInfo newData = new JavaScriptSerializer().Deserialize<LanguageInfo>(newLanguageData);

            if (oldData.CanSpeakAmericanSignLanguage != newData.CanSpeakAmericanSignLanguage)
            {
                ProfileUpdatedData language = new ProfileUpdatedData();
                language.FieldName = "CanSpeakAmericanSignLanguage";
                language.OldValue = oldData.CanSpeakAmericanSignLanguage;
                language.NewValue = newData.CanSpeakAmericanSignLanguage;
                updatedList.Add(language);
            }

            int oldCount = oldData.KnownLanguages.Count;
            int newCount = newData.KnownLanguages.Count;
            string oldValue = "";
            if (oldCount > 0)
            {
                oldValue = "Priority : " + oldData.KnownLanguages.ElementAt(0).ProficiencyIndex + " - " + oldData.KnownLanguages.ElementAt(0).Language;

                for (int i = 1; i < oldCount; i++)
                {
                    oldValue = oldValue + ", " + "Priority : " + oldData.KnownLanguages.ElementAt(i).ProficiencyIndex + " - " + oldData.KnownLanguages.ElementAt(i).Language;
                }
            }

            string newValue = "";
            if (newCount > 0)
            {
                newValue = "Priority : " + newData.KnownLanguages.ElementAt(0).ProficiencyIndex + " - " + newData.KnownLanguages.ElementAt(0).Language;

                for (int i = 1; i < newCount; i++)
                {
                    newValue = newValue + ", " + "Priority : " + newData.KnownLanguages.ElementAt(i).ProficiencyIndex + " - " + newData.KnownLanguages.ElementAt(i).Language;
                }
            }

            if (oldValue != newValue)
            {
                ProfileUpdatedData language = new ProfileUpdatedData();
                language.FieldName = "Languages";
                language.OldValue = oldValue;
                language.NewValue = newValue;
                updatedList.Add(language);
            }

            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForContact(dynamic oldContactData, dynamic newContactData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            ContactDetail oldData = new JavaScriptSerializer().Deserialize<ContactDetail>(oldContactData);
            ContactDetail newData = new JavaScriptSerializer().Deserialize<ContactDetail>(newContactData);

            if (oldData.PersonalPager != newData.PersonalPager)
            {
                ProfileUpdatedData contact = new ProfileUpdatedData();
                contact.FieldName = "PagerNumber";
                contact.OldValue = oldData.PersonalPager;
                contact.NewValue = newData.PersonalPager;
                updatedList.Add(contact);
            }


            #region Phone Detail

            var oldPhoneNumbers = oldData.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Home.ToString() && p.Status == "Active").ToList();
            var oldFaxNumbers = oldData.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString() && p.Status == "Active").ToList();
            var oldMobileNumbers = oldData.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString() && p.Status == "Active").ToList();

            var newPhoneNumbers = newData.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Home.ToString() && p.Status == "Active").ToList();
            var newFaxNumbers = newData.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Fax.ToString() && p.Status == "Active").ToList();
            var newMobileNumbers = newData.PhoneDetails.Where(p => p.PhoneType == AHC.CD.Entities.MasterData.Enums.PhoneTypeEnum.Mobile.ToString() && p.Status == "Active").ToList();

            int oldPhoneCount = oldPhoneNumbers.Count;
            int oldFaxCount = oldFaxNumbers.Count;
            int oldMobileCount = oldMobileNumbers.Count;

            int newPhoneCount = newPhoneNumbers.Count;
            int newFaxCount = newFaxNumbers.Count;
            int newMobileCount = newMobileNumbers.Count;

            string oldPhone = "";
            string oldFax = "";
            string oldMobile = "";
            if (oldPhoneCount > 0)
            {
                oldPhone = "Number : " + oldPhoneNumbers.ElementAt(0).PhoneNumber + " " + "Preference : " + oldPhoneNumbers.ElementAt(0).Preference;

                for (int i = 1; i < oldPhoneCount; i++)
                {
                    oldPhone = oldPhone + ", " + "Number : " + oldPhoneNumbers.ElementAt(i).PhoneNumber + " " + "Preference : " + oldPhoneNumbers.ElementAt(i).Preference;
                }

            }

            if (oldFaxCount > 0)
            {
                oldFax = "Number : " + oldFaxNumbers.ElementAt(0).PhoneNumber + " " + "Preference : " + oldFaxNumbers.ElementAt(0).Preference;

                for (int i = 1; i < oldFaxCount; i++)
                {
                    oldFax = oldFax + ", " + "Number : " + oldFaxNumbers.ElementAt(i).PhoneNumber + " " + "Preference : " + oldFaxNumbers.ElementAt(i).Preference;
                }
            }

            if (oldMobileCount > 0)
            {
                oldMobile = "Number : " + oldMobileNumbers.ElementAt(0).PhoneNumber + " " + "Preference : " + oldMobileNumbers.ElementAt(0).Preference;

                for (int i = 1; i < oldMobileCount; i++)
                {
                    oldMobile = oldMobile + ", " + "Number : " + oldMobileNumbers.ElementAt(i).PhoneNumber + " " + "Preference : " + oldMobileNumbers.ElementAt(i).Preference;
                }

            }

            string newPhone = "";
            string newFax = "";
            string newMobile = "";
            if (newPhoneCount > 0)
            {
                newPhone = "Number : " + newPhoneNumbers.ElementAt(0).PhoneNumber + " " + "Preference : " + newPhoneNumbers.ElementAt(0).Preference;

                for (int i = 1; i < newPhoneCount; i++)
                {
                    newPhone = newPhone + ", " + "Number : " + newPhoneNumbers.ElementAt(i).PhoneNumber + " " + "Preference : " + newPhoneNumbers.ElementAt(i).Preference;
                }
            }

            if (newFaxCount > 0)
            {
                newFax = "Number : " + newFaxNumbers.ElementAt(0).PhoneNumber + " " + "Preference : " + newFaxNumbers.ElementAt(0).Preference;

                for (int i = 1; i < newFaxCount; i++)
                {
                    newFax = newFax + ", " + "Number : " + newFaxNumbers.ElementAt(i).PhoneNumber + " " + "Preference : " + newFaxNumbers.ElementAt(i).Preference;
                }
            }

            if (newMobileCount > 0)
            {
                newMobile = "Number : " + newMobileNumbers.ElementAt(0).PhoneNumber + " " + "Preference : " + newMobileNumbers.ElementAt(0).Preference;

                for (int i = 1; i < newMobileCount; i++)
                {
                    newMobile = newMobile + ", " + "Number : " + newMobileNumbers.ElementAt(i).PhoneNumber + " " + "Preference : " + newMobileNumbers.ElementAt(i).Preference;
                }

            }

            if (oldPhone != newPhone)
            {
                ProfileUpdatedData phone = new ProfileUpdatedData();
                phone.FieldName = "Home Phone";
                phone.OldValue = oldPhone;
                phone.NewValue = newPhone;
                updatedList.Add(phone);
            }
            if (oldFax != newFax)
            {
                ProfileUpdatedData phone = new ProfileUpdatedData();
                phone.FieldName = "Home Fax";
                phone.OldValue = oldFax;
                phone.NewValue = newFax;
                updatedList.Add(phone);
            }
            if (oldMobile != newMobile)
            {
                ProfileUpdatedData phone = new ProfileUpdatedData();
                phone.FieldName = "Mobile Number";
                phone.OldValue = oldMobile;
                phone.NewValue = newMobile;
                updatedList.Add(phone);
            }

            #endregion

            #region Email Detail

            int oldEmailCount = oldData.EmailIDs.Count;
            int newEmailCount = newData.EmailIDs.Count;
            string oldEmail = "";
            if (oldEmailCount > 0)
            {
                if (oldData.EmailIDs.ElementAt(0).Status == "Active")
                    oldEmail = "Email : " + oldData.EmailIDs.ElementAt(0).EmailAddress + " " + "Preference : " + oldData.EmailIDs.ElementAt(0).Preference;

                for (int i = 1; i < oldEmailCount; i++)
                {
                    if (oldData.EmailIDs.ElementAt(i).Status == "Active")
                        oldEmail = oldEmail + ", " + "Email : " + oldData.EmailIDs.ElementAt(i).EmailAddress + " " + "Preference : " + oldData.EmailIDs.ElementAt(i).Preference;
                }

            }

            string newEmail = "";
            if (newEmailCount > 0)
            {
                if (newData.EmailIDs.ElementAt(0).Status == "Active")
                    newEmail = "Email : " + newData.EmailIDs.ElementAt(0).EmailAddress + " " + "Preference : " + newData.EmailIDs.ElementAt(0).Preference;

                for (int i = 1; i < newEmailCount; i++)
                {
                    if (newData.EmailIDs.ElementAt(i).Status == "Active")
                        newEmail = newEmail + ", " + "Email : " + newData.EmailIDs.ElementAt(i).EmailAddress + " " + "Preference : " + newData.EmailIDs.ElementAt(i).Preference;
                }

            }

            if (oldEmail != newEmail)
            {
                ProfileUpdatedData email = new ProfileUpdatedData();
                email.FieldName = "Email Id";
                email.OldValue = oldEmail;
                email.NewValue = newEmail;
                updatedList.Add(email);
            }

            #endregion

            #region Preferred Written Contact

            int oldPreferredCount = oldData.PreferredWrittenContacts.Count;
            int newPreferredCount = newData.PreferredWrittenContacts.Count;
            string oldPreferred = "";
            if (oldPreferredCount > 0)
            {
                if (oldData.PreferredWrittenContacts.ElementAt(0).Status == "Active")
                    oldPreferred = "Preferred Written Contact : " + oldData.PreferredWrittenContacts.ElementAt(0).ContactType + " " + "Preferred Index : " + oldData.PreferredWrittenContacts.ElementAt(0).PreferredIndex;

                for (int i = 1; i < oldPreferredCount; i++)
                {
                    if (oldData.PreferredWrittenContacts.ElementAt(i).Status == "Active")
                        oldPreferred = oldPreferred + ", " + "Preferred Written Contact : " + oldData.PreferredWrittenContacts.ElementAt(i).ContactType + " " + "Preferred Index : " + oldData.PreferredWrittenContacts.ElementAt(i).PreferredIndex;
                }

            }

            string newPreferred = "";
            if (newPreferredCount > 0)
            {
                if (newData.PreferredWrittenContacts.ElementAt(0).Status == "Active")
                    newPreferred = "Preferred Written Contact : " + newData.PreferredWrittenContacts.ElementAt(0).ContactType + " " + "Preferred Index : " + newData.PreferredWrittenContacts.ElementAt(0).PreferredIndex;

                for (int i = 1; i < newPreferredCount; i++)
                {
                    if (newData.PreferredWrittenContacts.ElementAt(i).Status == "Active")
                        newPreferred = newPreferred + ", " + "Preferred Written Contact : " + newData.PreferredWrittenContacts.ElementAt(i).ContactType + " " + "Preferred Index : " + newData.PreferredWrittenContacts.ElementAt(i).PreferredIndex;
                }

            }

            if (oldPreferred != newPreferred)
            {
                ProfileUpdatedData preferred = new ProfileUpdatedData();
                preferred.FieldName = "PreferredWrittenContact";
                preferred.OldValue = oldPreferred;
                preferred.NewValue = newPreferred;
                updatedList.Add(preferred);
            }

            #endregion

            #region Preferred Contact

            int oldPreferredContactCount = oldData.PreferredContacts.Count;
            int newPreferredContactCount = newData.PreferredContacts.Count;
            string oldPreferredContact = "";
            if (oldPreferredContactCount > 0)
            {
                if (oldData.PreferredContacts.ElementAt(0).Status == "Active")
                    //  oldPreferredContact = "Preferred Contact : " + oldData.PreferredContacts.ElementAt(0).ContactType + " " + "Preferred Index : " + oldData.PreferredContacts.ElementAt(0).PreferredIndex;
                    oldPreferredContact = oldData.PreferredContacts.ElementAt(0).ContactType;

                for (int i = 1; i < oldPreferredContactCount; i++)
                {
                    if (oldData.PreferredContacts.ElementAt(i).Status == "Active")
                        //oldPreferredContact = oldPreferredContact + ", " + "Preferred Contact : " + oldData.PreferredContacts.ElementAt(i).ContactType + " " + "Preferred Index : " + oldData.PreferredContacts.ElementAt(i).PreferredIndex;
                        oldPreferredContact = oldPreferredContact + ", " + oldData.PreferredContacts.ElementAt(i).ContactType;
                }

            }

            string newPreferredContact = "";
            if (newPreferredContactCount > 0)
            {
                if (newData.PreferredContacts.ElementAt(0).Status == "Active")
                    //newPreferredContact = "Preferred Contact : " + newData.PreferredContacts.ElementAt(0).ContactType + " " + "Preferred Index : " + newData.PreferredContacts.ElementAt(0).PreferredIndex;
                    newPreferredContact = newData.PreferredContacts.ElementAt(0).ContactType;

                for (int i = 1; i < newPreferredContactCount; i++)
                {
                    if (newData.PreferredContacts.ElementAt(i).Status == "Active")
                        //newPreferredContact = newPreferredContact + ", " + "Preferred Contact : " + newData.PreferredContacts.ElementAt(i).ContactType + " " + "Preferred Index : " + newData.PreferredContacts.ElementAt(i).PreferredIndex;
                        newPreferredContact = newPreferredContact + ", " + newData.PreferredContacts.ElementAt(i).ContactType;
                }

            }

            if (oldPreferredContact != newPreferredContact)
            {
                ProfileUpdatedData preferred = new ProfileUpdatedData();
                preferred.FieldName = "PreferredContact";
                preferred.OldValue = oldPreferredContact;
                preferred.NewValue = newPreferredContact;
                updatedList.Add(preferred);
            }

            #endregion

            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForPracticeLocation(dynamic oldLocationData, dynamic newLocationData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            Facility oldData = new JavaScriptSerializer().Deserialize<Facility>(oldLocationData);
            Facility newData = new JavaScriptSerializer().Deserialize<Facility>(newLocationData);

            #region General Information

            if (oldData.Building != newData.Building)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "Building";
                location.OldValue = oldData.Building;
                location.NewValue = newData.Building;
                updatedList.Add(location);
            }

            if (oldData.City != newData.City)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "City";
                location.OldValue = oldData.City;
                location.NewValue = newData.City;
                updatedList.Add(location);
            }

            if (oldData.Country != newData.Country)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "Country";
                location.OldValue = oldData.Country;
                location.NewValue = newData.Country;
                updatedList.Add(location);
            }

            if (oldData.County != newData.County)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "County";
                location.OldValue = oldData.County;
                location.NewValue = newData.County;
                updatedList.Add(location);
            }

            if (oldData.EmailAddress != newData.EmailAddress)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "EmailAddress";
                location.OldValue = oldData.EmailAddress;
                location.NewValue = newData.EmailAddress;
                updatedList.Add(location);
            }

            if (oldData.FacilityName != newData.FacilityName)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "FacilityName";
                location.OldValue = oldData.FacilityName;
                location.NewValue = newData.FacilityName;
                updatedList.Add(location);
            }

            if (oldData.FaxNumber != newData.FaxNumber)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "Fax ";
                location.OldValue = oldData.FaxNumber;
                location.NewValue = newData.FaxNumber;
                updatedList.Add(location);
            }

            if (oldData.MobileNumber != newData.MobileNumber)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "Telephone ";
                location.OldValue = oldData.MobileNumber;
                location.NewValue = newData.MobileNumber;
                updatedList.Add(location);
            }

            if (oldData.Name != newData.Name)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "Name";
                location.OldValue = oldData.Name;
                location.NewValue = newData.Name;
                updatedList.Add(location);
            }

            if (oldData.State != newData.State)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "State";
                location.OldValue = oldData.State;
                location.NewValue = newData.State;
                updatedList.Add(location);
            }

            if (oldData.Street != newData.Street)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "Street";
                location.OldValue = oldData.Street;
                location.NewValue = newData.Street;
                updatedList.Add(location);
            }

            if (oldData.ZipCode != newData.ZipCode)
            {
                ProfileUpdatedData location = new ProfileUpdatedData();
                location.FieldName = "ZipCode";
                location.OldValue = oldData.ZipCode;
                location.NewValue = newData.ZipCode;
                updatedList.Add(location);
            }

            #endregion

            #region Facility Detail

            #region Accessibilities

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(0).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(0).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this meet ADA accessibility requirements? *";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(0).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(0).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(1).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(1).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer handicapped access for the following : Building";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(1).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(1).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(2).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(2).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer handicapped access for the following : Parking ";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(2).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(2).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(3).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(3).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer handicapped access for the following : Restroom ";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(3).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(3).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.OtherHandicappedAccess != newData.FacilityDetail.Accessibility.OtherHandicappedAccess)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer handicapped access for the following : Other Handicapped Access ";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.OtherHandicappedAccess;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.OtherHandicappedAccess;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(4).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(4).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer other services for the disabled?";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(4).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(4).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(5).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(5).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer other services for the disabled? : Text telephony(TTY)";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(5).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(5).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(6).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(6).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer other services for the disabled? : Mental/Physical impairment services";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(6).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(6).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.OtherDisabilityServices != newData.FacilityDetail.Accessibility.OtherDisabilityServices)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Does this site offer other services for the disabled? : OtherDisabilityServices";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.OtherDisabilityServices;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.OtherDisabilityServices;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(7).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(7).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Accessible by public transportation?";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(7).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(7).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(8).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(8).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Accessible by public transportation? : Bus ";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(8).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(8).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(9).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(9).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Accessible by public transportation? : Subway";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(9).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(9).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(10).Answer != newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(10).Answer)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Accessible by public transportation?: Regional train";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(10).Answer;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.ElementAt(10).Answer;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Accessibility.OtherTransportationAccess != newData.FacilityDetail.Accessibility.OtherTransportationAccess)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Accessible by public transportation? : Other Transportation Access";
                accessibility.OldValue = oldData.FacilityDetail.Accessibility.OtherTransportationAccess;
                accessibility.NewValue = newData.FacilityDetail.Accessibility.OtherTransportationAccess;
                updatedList.Add(accessibility);
            }


            #endregion

            #region Services

            if (oldData.FacilityDetail.Service.LaboratoryServices != newData.FacilityDetail.Service.LaboratoryServices)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Laboratory Services?";
                accessibility.OldValue = oldData.FacilityDetail.Service.LaboratoryServices;
                accessibility.NewValue = newData.FacilityDetail.Service.LaboratoryServices;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.LaboratoryAccreditingCertifyingProgram != newData.FacilityDetail.Service.LaboratoryAccreditingCertifyingProgram)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Accrediting/Certifying Program";
                accessibility.OldValue = oldData.FacilityDetail.Service.LaboratoryAccreditingCertifyingProgram;
                accessibility.NewValue = newData.FacilityDetail.Service.LaboratoryAccreditingCertifyingProgram;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.RadiologyServices != newData.FacilityDetail.Service.RadiologyServices)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Radiology Services?";
                accessibility.OldValue = oldData.FacilityDetail.Service.RadiologyServices;
                accessibility.NewValue = newData.FacilityDetail.Service.RadiologyServices;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.RadiologyXRayCertificateType != newData.FacilityDetail.Service.RadiologyXRayCertificateType)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "X-Ray Certificate Type";
                accessibility.OldValue = oldData.FacilityDetail.Service.RadiologyXRayCertificateType;
                accessibility.NewValue = newData.FacilityDetail.Service.RadiologyXRayCertificateType;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.IsAnesthesiaAdministered != newData.FacilityDetail.Service.IsAnesthesiaAdministered)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Is Anesthesia Administered In Your Office?";
                accessibility.OldValue = oldData.FacilityDetail.Service.IsAnesthesiaAdministered;
                accessibility.NewValue = newData.FacilityDetail.Service.IsAnesthesiaAdministered;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.AnesthesiaCategory != newData.FacilityDetail.Service.AnesthesiaCategory)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "What Class/Category Do You Use";
                accessibility.OldValue = oldData.FacilityDetail.Service.AnesthesiaCategory;
                accessibility.NewValue = newData.FacilityDetail.Service.AnesthesiaCategory;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.AnesthesiaAdminFirstName != newData.FacilityDetail.Service.AnesthesiaAdminFirstName)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "First Name";
                accessibility.OldValue = oldData.FacilityDetail.Service.AnesthesiaAdminFirstName;
                accessibility.NewValue = newData.FacilityDetail.Service.AnesthesiaAdminFirstName;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.AnesthesiaAdminLastName != newData.FacilityDetail.Service.AnesthesiaAdminLastName)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Last Name";
                accessibility.OldValue = oldData.FacilityDetail.Service.AnesthesiaAdminLastName;
                accessibility.NewValue = newData.FacilityDetail.Service.AnesthesiaAdminLastName;
                updatedList.Add(accessibility);
            }

            if (oldData.FacilityDetail.Service.AdditionalOfficeProcedures != newData.FacilityDetail.Service.AdditionalOfficeProcedures)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "Additional Office Procedures Provided (Including Surgical Procedures)";
                accessibility.OldValue = oldData.FacilityDetail.Service.AdditionalOfficeProcedures;
                accessibility.NewValue = newData.FacilityDetail.Service.AdditionalOfficeProcedures;
                updatedList.Add(accessibility);
            }

            var typeRepo = uow.GetGenericRepository<FacilityPracticeType>();
            var providerTypes = typeRepo.GetAll();

            if (oldData.FacilityDetail.Service.PracticeTypeID != newData.FacilityDetail.Service.PracticeTypeID)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "PracticeType";
                foreach (var item in providerTypes)
                {
                    if (item.FacilityPracticeTypeID == oldData.FacilityDetail.Service.PracticeTypeID)
                        accessibility.OldValue = item.Title;
                }

                foreach (var item in providerTypes)
                {
                    if (item.FacilityPracticeTypeID == newData.FacilityDetail.Service.PracticeTypeID)
                        accessibility.NewValue = item.Title;
                }
                updatedList.Add(accessibility);
            }

            int count = oldData.FacilityDetail.Service.FacilityServiceQuestionAnswers.Count;

            var facilityServiceRepo = uow.GetGenericRepository<FacilityServiceQuestion>();
            var facilityQuestions = facilityServiceRepo.GetAll();

            for (int i = 0; i < count; i++)
            {
                if (oldData.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(i).Answer != newData.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(i).Answer)
                {
                    ProfileUpdatedData accessibility = new ProfileUpdatedData();
                    foreach (var item in facilityQuestions)
                    {
                        if (item.FacilityServiceQuestionID == oldData.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(i).FacilityServiceQuestionId)
                            accessibility.FieldName = item.ShortTitle;
                    }
                    accessibility.OldValue = oldData.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(i).Answer;
                    accessibility.NewValue = newData.FacilityDetail.Service.FacilityServiceQuestionAnswers.ElementAt(i).Answer;
                    updatedList.Add(accessibility);
                }

            }

            #endregion

            #region Language

            if (oldData.FacilityDetail.Language.AmericanSignLanguage != newData.FacilityDetail.Language.AmericanSignLanguage)
            {
                ProfileUpdatedData accessibility = new ProfileUpdatedData();
                accessibility.FieldName = "AmericanSignLanguage";
                accessibility.OldValue = oldData.FacilityDetail.Language.AmericanSignLanguage;
                accessibility.NewValue = newData.FacilityDetail.Language.AmericanSignLanguage;
                updatedList.Add(accessibility);
            }
            int oldCount = oldData.FacilityDetail.Language.NonEnglishLanguages.Count;
            int newCount = newData.FacilityDetail.Language.NonEnglishLanguages.Count;
            string oldValue = "";
            if (oldCount > 0)
            {
                oldValue = oldData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(0).Language + " - " + "InterpretersAvailable : " + oldData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(0).InterpretersAvailable;

                for (int i = 1; i < oldCount; i++)
                {
                    oldValue = oldValue + ", " + oldData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(i).Language + " - " + "InterpretersAvailable : " + oldData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(i).InterpretersAvailable;
                }
            }

            string newValue = "";
            if (newCount > 0)
            {
                newValue = newData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(0).Language + " - " + "InterpretersAvailable : " + newData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(0).InterpretersAvailable;

                for (int i = 1; i < newCount; i++)
                {
                    newValue = newValue + ", " + newData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(i).Language + " - " + "InterpretersAvailable : " + newData.FacilityDetail.Language.NonEnglishLanguages.ElementAt(i).InterpretersAvailable;
                }
            }

            if (oldValue != newValue)
            {
                ProfileUpdatedData language = new ProfileUpdatedData();
                language.FieldName = "Languages";
                language.OldValue = oldValue;
                language.NewValue = newValue;
                updatedList.Add(language);
            }

            #endregion

            #region Office Hours

            for (int j = 0; j < 7; j++)
            {
                int oldHourCount = oldData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.Count;
                int newHourCount = newData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.Count;
                string oldHourValue = "";
                if (oldHourCount > 0)
                {
                    oldHourValue = "Start Time : " + oldData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).StartTime + " - " + "End Time : " + oldData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).EndTime;

                    for (int i = 1; i < oldHourCount; i++)
                    {
                        oldHourValue = oldHourValue + ", " + "Start Time : " + oldData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).StartTime + " - " + "End Time : " + oldData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).EndTime;
                    }
                }

                string newHourValue = "";
                if (newHourCount > 0)
                {
                    newHourValue = "Start Time : " + newData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).StartTime + " - " + "End Time : " + newData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).EndTime;

                    for (int i = 1; i < newHourCount; i++)
                    {
                        newHourValue = newHourValue + ", " + "Start Time : " + newData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).StartTime + " - " + "End Time : " + newData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).EndTime;
                    }
                }

                if (oldHourValue != newHourValue)
                {
                    ProfileUpdatedData day = new ProfileUpdatedData();
                    day.FieldName = oldData.FacilityDetail.PracticeOfficeHour.PracticeDays.ElementAt(j).DayName;
                    day.OldValue = oldHourValue;
                    day.NewValue = newHourValue;
                    updatedList.Add(day);
                }
            }

            #endregion

            #region Midlevel Practitioners

            int oldMidCount = oldData.FacilityDetail.FacilityPracticeProviders.Count;
            int newMidCount = oldData.FacilityDetail.FacilityPracticeProviders.Count;

            if (oldMidCount == newMidCount)
            {
                for (int i = 0; i < oldMidCount; i++)
                {
                    if (oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID == newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID && oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FirstName != newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FirstName)
                    {
                        ProfileUpdatedData location = new ProfileUpdatedData();
                        location.FieldName = "FirstName";
                        location.OldValue = oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FirstName;
                        location.NewValue = newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FirstName;
                        updatedList.Add(location);
                    }

                    if (oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID == newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID && oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID == newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID && oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).LastName != newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).LastName)
                    {
                        ProfileUpdatedData location = new ProfileUpdatedData();
                        location.FieldName = "LastName";
                        location.OldValue = oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).LastName;
                        location.NewValue = newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).LastName;
                        updatedList.Add(location);
                    }

                    if (oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID == newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID && oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).MiddleName != newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).MiddleName)
                    {
                        ProfileUpdatedData location = new ProfileUpdatedData();
                        location.FieldName = "MiddleName";
                        location.OldValue = oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).MiddleName;
                        location.NewValue = newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).MiddleName;
                        updatedList.Add(location);
                    }

                    if (oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID == newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).FacilityPracticeProviderID && oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).NPINumber != newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).NPINumber)
                    {
                        ProfileUpdatedData location = new ProfileUpdatedData();
                        location.FieldName = "NPINumber";
                        location.OldValue = oldData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).NPINumber;
                        location.NewValue = newData.FacilityDetail.FacilityPracticeProviders.ElementAt(i).NPINumber;
                        updatedList.Add(location);
                    }
                }
            }

            #endregion

            #endregion

            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForOfficeHours(dynamic oldOfficeHoursData, dynamic newOfficeHoursData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            ProviderPracticeOfficeHour oldData = new JavaScriptSerializer().Deserialize<ProviderPracticeOfficeHour>(oldOfficeHoursData);
            ProviderPracticeOfficeHour newData = new JavaScriptSerializer().Deserialize<ProviderPracticeOfficeHour>(newOfficeHoursData);

            for (int j = 0; j < 7; j++)
            {
                int oldHourCount = oldData != null ? oldData.PracticeDays.ElementAt(j).DailyHours.Count : 0;
                int newHourCount = newData.PracticeDays.ElementAt(j).DailyHours.Count;
                string oldHourValue = "";
                if (oldHourCount > 0)
                {
                    oldHourValue = "Start Time : " + oldData.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).StartTime + " - " + "End Time : " + oldData.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).EndTime;

                    for (int i = 1; i < oldHourCount; i++)
                    {
                        oldHourValue = oldHourValue + ", " + "Start Time : " + oldData.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).StartTime + " - " + "End Time : " + oldData.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).EndTime;
                    }
                }

                string newHourValue = "";
                if (newHourCount > 0)
                {
                    newHourValue = "Start Time : " + newData.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).StartTime + " - " + "End Time : " + newData.PracticeDays.ElementAt(j).DailyHours.ElementAt(0).EndTime;

                    for (int i = 1; i < newHourCount; i++)
                    {
                        newHourValue = newHourValue + ", " + "Start Time : " + newData.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).StartTime + " - " + "End Time : " + newData.PracticeDays.ElementAt(j).DailyHours.ElementAt(i).EndTime;
                    }
                }

                if (oldHourValue != newHourValue)
                {
                    ProfileUpdatedData day = new ProfileUpdatedData();
                    day.FieldName = oldData != null ? oldData.PracticeDays.ElementAt(j).DayName : newData.PracticeDays.ElementAt(j).DayName;
                    day.OldValue = oldHourValue;
                    day.NewValue = newHourValue;
                    updatedList.Add(day);
                }
            }

            if (oldData == null || oldData.AnyTimePhoneCoverage != newData.AnyTimePhoneCoverage)
            {
                ProfileUpdatedData hour = new ProfileUpdatedData();
                hour.FieldName = "24/7 Phone Coverage ?";
                hour.OldValue = oldData != null ? oldData.AnyTimePhoneCoverage : "";
                hour.NewValue = newData.AnyTimePhoneCoverage;
                updatedList.Add(hour);
            }

            if (oldData == null || oldData.AnsweringService != newData.AnsweringService)
            {
                ProfileUpdatedData hour = new ProfileUpdatedData();
                hour.FieldName = "AnsweringService";
                hour.OldValue = oldData != null ? oldData.AnsweringService : "";
                hour.NewValue = newData.AnsweringService;
                updatedList.Add(hour);
            }
            if (oldData == null || oldData.VoiceMailOther != newData.VoiceMailOther)
            {
                ProfileUpdatedData hour = new ProfileUpdatedData();
                hour.FieldName = "Voice Mail With Instructions To Call Answering Service";
                hour.OldValue = oldData != null ? oldData.VoiceMailOther : "";
                hour.NewValue = newData.VoiceMailOther;
                updatedList.Add(hour);
            }
            if (oldData == null || oldData.VoiceMailToAnsweringService != newData.VoiceMailToAnsweringService)
            {
                ProfileUpdatedData hour = new ProfileUpdatedData();
                hour.FieldName = "Voice Mail With Other Instructions";
                hour.OldValue = oldData != null ? oldData.VoiceMailToAnsweringService : "";
                hour.NewValue = newData.VoiceMailToAnsweringService;
                updatedList.Add(hour);
            }
            if (oldData == null || oldData.AfterHoursTelephoneNumber != newData.AfterHoursTelephoneNumber)
            {
                ProfileUpdatedData hour = new ProfileUpdatedData();
                hour.FieldName = "After Hours Telephone Number";
                hour.OldValue = oldData != null ? oldData.AfterHoursTelephoneNumber : "";
                hour.NewValue = newData.AfterHoursTelephoneNumber;
                updatedList.Add(hour);
            }


            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForPracitceStatus(dynamic oldPracticeData, dynamic newPracticeData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            var practiceRepo = uow.GetGenericRepository<OpenPracticeStatus>();
            OpenPracticeStatus data = new JavaScriptSerializer().Deserialize<OpenPracticeStatus>(oldPracticeData);
            OpenPracticeStatus oldData = practiceRepo.Find(p => p.OpenPracticeStatusID == data.OpenPracticeStatusID, "PracticeQuestionAnswers");
            OpenPracticeStatus newData = new JavaScriptSerializer().Deserialize<OpenPracticeStatus>(newPracticeData);

            if (oldData.AnyInformationVariesByPlan != newData.AnyInformationVariesByPlan)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "If any of the above information varies by plan, Explain";
                practice.OldValue = oldData.AnyInformationVariesByPlan;
                practice.NewValue = newData.AnyInformationVariesByPlan;
                updatedList.Add(practice);
            }

            if (oldData.AnyPracticeLimitation != newData.AnyPracticeLimitation)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "Are There Any Practice Limitations";
                practice.OldValue = oldData.AnyPracticeLimitation;
                practice.NewValue = newData.AnyPracticeLimitation;
                updatedList.Add(practice);
            }

            if (oldData.GenderLimitation != newData.GenderLimitation)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "Gender Limitations";
                practice.OldValue = oldData.GenderLimitation;
                practice.NewValue = newData.GenderLimitation;
                updatedList.Add(practice);
            }

            if (oldData.MaximumAge != newData.MaximumAge)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "Age Limitations";
                practice.OldValue = "Maximum Age : " + oldData.MaximumAge.ToString();
                practice.NewValue = "Maximum Age : " + newData.MaximumAge.ToString();
                updatedList.Add(practice);
            }

            if (oldData.MinimumAge != newData.MinimumAge)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "Age Limitations ";
                practice.OldValue = "Minimum Age : " + oldData.MinimumAge.ToString();
                practice.NewValue = "Minimum Age : " + newData.MinimumAge.ToString();
                updatedList.Add(practice);
            }

            if (oldData.OtherLimitations != newData.OtherLimitations)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "List Other Limitations";
                practice.OldValue = oldData.OtherLimitations;
                practice.NewValue = newData.OtherLimitations;
                updatedList.Add(practice);
            }

            int count = oldData.PracticeQuestionAnswers.Count;

            var openStatusRepo = uow.GetGenericRepository<PracticeOpenStatusQuestion>();
            var openStatusQuestions = openStatusRepo.GetAll();

            for (int i = 0; i < count; i++)
            {
                if (oldData.PracticeQuestionAnswers.ElementAt(i).Answer != newData.PracticeQuestionAnswers.ElementAt(i).Answer)
                {
                    ProfileUpdatedData openStatus = new ProfileUpdatedData();
                    foreach (var item in openStatusQuestions)
                    {
                        if (item.PracticeOpenStatusQuestionID == oldData.PracticeQuestionAnswers.ElementAt(i).PracticeQuestionId)
                            openStatus.FieldName = item.Title;
                    }
                    openStatus.OldValue = oldData.PracticeQuestionAnswers.ElementAt(i).Answer;
                    openStatus.NewValue = newData.PracticeQuestionAnswers.ElementAt(i).Answer;
                    updatedList.Add(openStatus);
                }

            }


            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForCoveringCollegues(dynamic oldCollegueData, dynamic newCollegueData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            var collegueRepo = uow.GetGenericRepository<PracticeProvider>();
            PracticeProvider data = new JavaScriptSerializer().Deserialize<PracticeProvider>(oldCollegueData);
            PracticeProvider oldData = collegueRepo.Find(d => d.PracticeProviderID == data.PracticeProviderID, "PracticeProviderSpecialties, PracticeProviderTypes");
            PracticeProvider newData = new JavaScriptSerializer().Deserialize<PracticeProvider>(newCollegueData);

            if (oldData.FirstName != newData.FirstName)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "FirstName";
                practice.OldValue = oldData.FirstName;
                practice.NewValue = newData.FirstName;
                updatedList.Add(practice);
            }

            if (oldData.LastName != newData.LastName)
            {
                ProfileUpdatedData practice = new ProfileUpdatedData();
                practice.FieldName = "LastName";
                practice.OldValue = oldData.LastName;
                practice.NewValue = newData.LastName;
                updatedList.Add(practice);
            }

            var specialtyRepo = uow.GetGenericRepository<Specialty>();
            var specialties = specialtyRepo.GetAll();

            int oldCount = oldData.PracticeProviderSpecialties.Count;
            int newCount = newData.PracticeProviderSpecialties.Count;
            string oldValue = "";
            if (oldCount > 0)
            {
                if (oldData.PracticeProviderSpecialties.ElementAt(0).Status == "Active")
                    oldValue = oldData.PracticeProviderSpecialties.ElementAt(0).Specialty.Name;

                for (int i = 1; i < oldCount; i++)
                {
                    if (oldData.PracticeProviderSpecialties.ElementAt(i).Status == "Active")
                        oldValue = oldValue + "," + oldData.PracticeProviderSpecialties.ElementAt(i).Specialty.Name;

                }
            }

            string newValue = "";
            if (newCount > 0)
            {
                foreach (var item in specialties)
                {
                    if (item.SpecialtyID == newData.PracticeProviderSpecialties.ElementAt(0).SpecialtyID && newData.PracticeProviderSpecialties.ElementAt(0).Status == "Active")
                    {
                        newValue = item.Name;
                    }
                }

                for (int i = 1; i < newCount; i++)
                {
                    foreach (var item in specialties)
                    {
                        if (newValue != "")
                        {
                            if (item.SpecialtyID == newData.PracticeProviderSpecialties.ElementAt(i).SpecialtyID && newData.PracticeProviderSpecialties.ElementAt(i).Status == "Active")
                            {
                                newValue = newValue + "," + item.Name;
                            }
                        }
                        else
                        {
                            if (item.SpecialtyID == newData.PracticeProviderSpecialties.ElementAt(i).SpecialtyID && newData.PracticeProviderSpecialties.ElementAt(i).Status == "Active")
                            {
                                newValue = item.Name;
                            }
                        }
                    }

                }
            }

            if (oldValue != newValue)
            {
                ProfileUpdatedData collegue = new ProfileUpdatedData();
                collegue.FieldName = "Specialty";
                collegue.OldValue = oldValue;
                collegue.NewValue = newValue;
                updatedList.Add(collegue);
            }

            var providerTypeRepo = uow.GetGenericRepository<ProviderType>();
            var providerTypes = providerTypeRepo.GetAll();

            int oldTypeCount = oldData.PracticeProviderTypes.Count;
            int newTypeCount = newData.PracticeProviderTypes.Count;
            string oldTypeValue = "";
            if (oldTypeCount > 0)
            {
                if (oldData.PracticeProviderTypes.ElementAt(0).Status == "Active")
                    oldTypeValue = oldData.PracticeProviderTypes.ElementAt(0).ProviderType.Title;

                for (int i = 1; i < oldTypeCount; i++)
                {
                    if (oldData.PracticeProviderTypes.ElementAt(i).Status == "Active")
                        oldTypeValue = oldTypeValue + "," + oldData.PracticeProviderTypes.ElementAt(i).ProviderType.Title;

                }
            }

            string newTypeValue = "";
            if (newTypeCount > 0)
            {
                foreach (var item in providerTypes)
                {
                    if (item.ProviderTypeID == newData.PracticeProviderTypes.ElementAt(0).ProviderTypeID && newData.PracticeProviderTypes.ElementAt(0).Status == "Active")
                    {
                        newTypeValue = item.Title;
                    }
                }

                for (int i = 1; i < newTypeCount; i++)
                {
                    foreach (var item in providerTypes)
                    {
                        if (newTypeValue != "")
                        {
                            if (item.ProviderTypeID == newData.PracticeProviderTypes.ElementAt(i).ProviderTypeID && newData.PracticeProviderTypes.ElementAt(i).Status == "Active")
                            {
                                newTypeValue = newTypeValue + "," + item.Title;
                            }
                        }
                        else
                        {
                            if (item.ProviderTypeID == newData.PracticeProviderTypes.ElementAt(i).ProviderTypeID && newData.PracticeProviderTypes.ElementAt(i).Status == "Active")
                            {
                                newTypeValue = item.Title;
                            }
                        }
                    }

                }
            }

            if (oldTypeValue != newTypeValue)
            {
                ProfileUpdatedData collegue = new ProfileUpdatedData();
                collegue.FieldName = "Provider Type";
                collegue.OldValue = oldTypeValue;
                collegue.NewValue = newTypeValue;
                updatedList.Add(collegue);
            }

            return updatedList;
        }

        private List<ProfileUpdatedData> ConstructChangesForPaymentAndRemittance(dynamic oldPaymentData, dynamic newPaymentData)
        {
            List<ProfileUpdatedData> updatedList = new List<ProfileUpdatedData>();
            var practiceRepo = uow.GetGenericRepository<PracticePaymentAndRemittance>();
            PracticePaymentAndRemittance data = new JavaScriptSerializer().Deserialize<PracticePaymentAndRemittance>(oldPaymentData);
            PracticePaymentAndRemittance oldData = practiceRepo.Find(p => p.PracticePaymentAndRemittanceID == data.PracticePaymentAndRemittanceID, "PaymentAndRemittancePerson");
            PracticePaymentAndRemittance newData = new JavaScriptSerializer().Deserialize<PracticePaymentAndRemittance>(newPaymentData);

            if (oldData.ElectronicBillingCapability != newData.ElectronicBillingCapability)
            {
                ProfileUpdatedData payment = new ProfileUpdatedData();
                payment.FieldName = "Electronic Billing Capabilities";
                payment.OldValue = oldData.ElectronicBillingCapability;
                payment.NewValue = newData.ElectronicBillingCapability;
                updatedList.Add(payment);
            }

            if (oldData.BillingDepartment != newData.BillingDepartment)
            {
                ProfileUpdatedData payment = new ProfileUpdatedData();
                payment.FieldName = "Billing Department";
                payment.OldValue = oldData.BillingDepartment;
                payment.NewValue = newData.BillingDepartment;
                updatedList.Add(payment);
            }

            if (oldData.CheckPayableTo != newData.CheckPayableTo)
            {
                ProfileUpdatedData payment = new ProfileUpdatedData();
                payment.FieldName = "Check Payable To";
                payment.OldValue = oldData.CheckPayableTo;
                payment.NewValue = newData.CheckPayableTo;
                updatedList.Add(payment);
            }

            if (oldData.Office != newData.Office)
            {
                ProfileUpdatedData payment = new ProfileUpdatedData();
                payment.FieldName = "Office";
                payment.OldValue = oldData.Office;
                payment.NewValue = newData.Office;
                updatedList.Add(payment);
            }

            if (oldData.PaymentAndRemittancePerson != null && newData.PaymentAndRemittancePerson != null)
            {
                if (oldData.PaymentAndRemittancePerson.FirstName != newData.PaymentAndRemittancePerson.FirstName)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "First Name";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.FirstName;
                    payment.NewValue = newData.PaymentAndRemittancePerson.FirstName;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.MiddleName != newData.PaymentAndRemittancePerson.MiddleName)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Middle Name";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.MiddleName;
                    payment.NewValue = newData.PaymentAndRemittancePerson.MiddleName;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.LastName != newData.PaymentAndRemittancePerson.LastName)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Last Name";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.LastName;
                    payment.NewValue = newData.PaymentAndRemittancePerson.LastName;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.Street != newData.PaymentAndRemittancePerson.Street)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Street/P. O. Box";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.Street;
                    payment.NewValue = newData.PaymentAndRemittancePerson.Street;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.Building != newData.PaymentAndRemittancePerson.Building)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Suite/Building";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.Building;
                    payment.NewValue = newData.PaymentAndRemittancePerson.Building;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.City != newData.PaymentAndRemittancePerson.City)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "City";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.City;
                    payment.NewValue = newData.PaymentAndRemittancePerson.City;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.State != newData.PaymentAndRemittancePerson.State)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "State";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.State;
                    payment.NewValue = newData.PaymentAndRemittancePerson.State;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.ZipCode != newData.PaymentAndRemittancePerson.ZipCode)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Zip Code";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.ZipCode;
                    payment.NewValue = newData.PaymentAndRemittancePerson.ZipCode;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.Country != newData.PaymentAndRemittancePerson.Country)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Country";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.Country;
                    payment.NewValue = newData.PaymentAndRemittancePerson.Country;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.County != newData.PaymentAndRemittancePerson.County)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "County";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.County;
                    payment.NewValue = newData.PaymentAndRemittancePerson.County;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.MobileNumber != newData.PaymentAndRemittancePerson.MobileNumber)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Telephone";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.MobileNumber;
                    payment.NewValue = newData.PaymentAndRemittancePerson.MobileNumber;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.FaxNumber != newData.PaymentAndRemittancePerson.FaxNumber)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Fax";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.FaxNumber;
                    payment.NewValue = newData.PaymentAndRemittancePerson.FaxNumber;
                    updatedList.Add(payment);
                }
                if (oldData.PaymentAndRemittancePerson.EmailAddress != newData.PaymentAndRemittancePerson.EmailAddress)
                {
                    ProfileUpdatedData payment = new ProfileUpdatedData();
                    payment.FieldName = "Email Address";
                    payment.OldValue = oldData.PaymentAndRemittancePerson.EmailAddress;
                    payment.NewValue = newData.PaymentAndRemittancePerson.EmailAddress;
                    updatedList.Add(payment);
                }
            }



            return updatedList;
        }

        public string SaveDocumentTemporarily(DocumentDTO document, string documentSubPath, string documentTitle, int profileId)
        {
            string documentTemporaryPath = AddTemporaryDocument(documentSubPath, documentTitle, null, document, profileId);
            return documentTemporaryPath;
            throw new NotImplementedException();
        }

        private string AddTemporaryDocument(string documentSubPath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            //Create a profile document object
            ProfileDocument profileDocument = CreateTemporaryProfileDocumentObject(docTitle, documentSubPath, expiryDate);

            //Assign the Doc root path
            document.DocRootPath = profileDocument.DocPath;

            //Add the document if uploaded
            return profileDocumentManager.AddDocument(profileId, document, profileDocument);
        }


        private ProfileDocument CreateTemporaryProfileDocumentObject(string title, string docPath, DateTime? expiryDate)
        {
            return new ProfileDocument()
            {
                DocPath = docPath,
                Title = title,
                ExpiryDate = expiryDate
            };
        }

        private string ConvertToDateString(DateTime date)
        {
            try
            {
                if (date != null)
                {
                    string format = "MM-dd-yyyy";
                    System.Globalization.DateTimeFormatInfo dti = new System.Globalization.DateTimeFormatInfo();

                    //var stringDate = Convert.ToString(date);
                    //DateTime convertedDate = Convert.ToDateTime(stringDate).Date;
                    return date.Date.ToString(format, dti);
                }
                else
                {
                    return null;
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List<HospitalContactInfo> GetHospitalContactInfoByIds(int[] HospitalContactInfoIds)
        {
            List<HospitalContactInfo> hospitalContactInfo = new List<HospitalContactInfo>();
            var hospitalContactInfoRepo = uow.GetGenericRepository<HospitalContactInfo>();
            foreach (int item in HospitalContactInfoIds)
            {

                hospitalContactInfo.Add(hospitalContactInfoRepo.Find(c => c.HospitalContactInfoID == item));
            }
            return hospitalContactInfo;
        }

        public void CredentialingRequestTrackerSetApprovalAsync(Entities.Credentialing.CredentialingRequestTracker.CredentialingRequestTracker credentialingRequestTracker)
        {
            try
            {
                var credentialingRequesttrackerRepo = uow.GetGenericRepository<CredentialingRequestTracker>();
                credentialingRequesttrackerRepo.Create(credentialingRequestTracker);
                credentialingRequesttrackerRepo.Save();
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CredentialingRequestException(ExceptionMessage.CREDENTIALING_REQUEST_TRACKER_EXCEPTION, ex);
            }
        }
    }
}