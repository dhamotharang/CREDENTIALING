using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
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
        
        IUnitOfWork uow = null;

        public ProfileUpdateManager(IUnitOfWork uow, IProfileManager profileManager)
        {
            this.uow = uow;            
            this.profileManager = profileManager;
        }

        //private string StoreToRedis(int profileId, string subSectionName, StateLicenseInformation stateLicense)
        //{

        //    string redisKey = "";
        //    var profileRepo = uow.GetGenericRepository<Profile>();

        //    var profile = profileRepo.Find(f => f.ProfileID == profileId, "StateLicenses");

        //    var licensesOldData = profile.StateLicenses.FirstOrDefault(s => s.StateLicenseStatusID == stateLicense.StateLicenseStatusID);

           
        //    using (var redis = new RedisClient())
        //    {
        //        var client = redis.As<StateLicenseInformation>();
        //        //bool oldDataAdded = redis.Add("OldData", licensesOldData);
        //        //redisKeys.Add("OldData");
        //        bool newDataAdded = redis.Add(subSectionName + profileId, stateLicense);
        //        redisKey = subSectionName + profileId;
        //    }

        //    return redisKey;
        //}


        public List<ProfileUpdatesTracker> GetAllUpdates()
        {
            List<ProfileUpdatesTracker> updates = new List<ProfileUpdatesTracker>();
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
            var updatedData = trackerRepo.GetAll("Profile.OtherIdentificationNumber, Profile.PersonalDetail").ToList();

            var pendingUpdates = updatedData.Where(p => p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString()).ToList();
            var onHoldUpdates = updatedData.Where(p => p.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.OnHold.ToString()).ToList();

            updates = pendingUpdates;

            foreach (var item in onHoldUpdates)
            {
                updates.Add(item);
            }
            //var updatedData = trackerRepo.Get(d => d.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString() &&
            //    d.ApprovalStatus == AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.OnHold.ToString(),
            //    "Profile.OtherIdentificationNumber, Profile.PersonalDetail").ToList();


            return updates;
        }


        public List<BusinessModels.ProfileUpdates.ProfileUpdatedData> GetDataById(int trackerId)
        {
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();

            
            
            List<ProfileUpdatedData> updateFinalOldDataList = new List<ProfileUpdatedData>();
            List<ProfileUpdatedData> updateFinalNewDataList = new List<ProfileUpdatedData>();            
            List<ProfileUpdatedData> finalDataList = new List<ProfileUpdatedData>();
            List<ProfileUpdatedData> filteredFinalDataList = new List<ProfileUpdatedData>();
            

            var updatedData = trackerRepo.Find(p => p.ProfileUpdatesTrackerId == trackerId);

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
                            var DataList = ConstructOldDataForCCo(old.Value, val.Value, ref updateFinalOldDataList);
                        }
                        else
                        {
                            ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                            profileNewData.FieldName = old.Key;
                            profileNewData.OldValue = old.Value != null ? old.Value.ToString() : null;
                            profileNewData.NewValue = val.Value != null ? val.Value.ToString() : null;
                            updateFinalOldDataList.Add(profileNewData);
                        }
                    }

                }
            }

            //foreach (var old in oldData)
            //{
            //    foreach (var val in newData)
            //    {
            //        if (old.Key == val.Key)
            //        {
            //            if (val.Value is System.Collections.ICollection)
            //            {
            //                var DataList = ConstructNewDataForCCo(val.Value, ref updateFinalNewDataList);
            //                //foreach (var item in updateNewList)
            //                //{
            //                //    updateFinalNewDataList.Add(item);
            //                //}
            //            }
            //            else
            //            {
            //                ProfileUpdatedData profileNewData = new ProfileUpdatedData();
            //                profileNewData.FieldName = val.Key;
            //                profileNewData.NewValue = val.Value != null ? val.Value.ToString() : null;
            //                updateFinalNewDataList.Add(profileNewData);
            //            }
            //        }
            //    }

            //}

            //foreach (var finalNew in updateFinalNewDataList)
            //{
            //    foreach (var finalOld in updateFinalOldDataList)
            //    {
            //        if(finalNew.FieldName == finalOld.FieldName){

            //            ProfileUpdatedData profileNewData = new ProfileUpdatedData();
            //            profileNewData.FieldName = finalOld.FieldName;
            //            profileNewData.OldValue = finalOld.OldValue;
            //            profileNewData.NewValue = finalNew.NewValue;                        
            //            finalDataList.Add(profileNewData);
            //        }
            //    }
            //}

            foreach (var item in updateFinalOldDataList)
            {
                if (item.NewValue != null && item.NewValue != item.OldValue && (!item.FieldName.Contains("Last")))
                {
                    if (!item.FieldName.Contains("Status"))
                        filteredFinalDataList.Add(item);
                }
            }

             return filteredFinalDataList;
        }


        public async Task SetApproval(ApprovalSubmission tracker, string userAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();
            var updatedData = trackerRepo.Find(p => p.ProfileUpdatesTrackerId == tracker.TrackerId);

            updatedData.ApprovalStatus = tracker.ApprovalStatus;
            updatedData.RejectionReason = tracker.RejectionReason;
            updatedData.LastModifiedBy = user.CDUserID;
            updatedData.LastModifiedDate = DateTime.Now;
   
            trackerRepo.Update(updatedData);

            await trackerRepo.SaveAsync();           
        }


        public int AddProfileUpdateForProvider<TObject, T>(TObject t, T t1, ProfileUpdateTrackerBusinessModel tracker) where TObject : class where T : class
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == tracker.userAuthId);
            var trackerRepo = uow.GetGenericRepository<ProfileUpdatesTracker>();

            var sectionWiseRepo = uow.GetGenericRepository<T>();
            //var oldData = sectionWiseRepo.Find(tracker.objId, tracker.IncludeProperties);
            var oldData = sectionWiseRepo.Find(tracker.objId); 
            var profileUpdate = ConstructTrackerData(t, t1, tracker, user.CDUserID);

            profileUpdate.oldData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(oldData);
            
            trackerRepo.Create(profileUpdate);

            trackerRepo.Save();

            return profileUpdate.ProfileUpdatesTrackerId;
        }

        private ProfileUpdatesTracker ConstructTrackerData<TObject, T>(TObject t, T t1, ProfileUpdateTrackerBusinessModel trackerBuss, int userId) where TObject : class where T : class
        {
            ProfileUpdatesTracker tracker = new ProfileUpdatesTracker();   
            string updatedData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(t);
            tracker.NewData = updatedData;
            string convertedData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(t1);
            tracker.NewConvertedData = convertedData;
            tracker.ApprovalStatus = AHC.CD.Entities.MasterData.Enums.ApprovalStatusType.Pending.ToString();
            tracker.Modification = trackerBuss.ModificationType;
            tracker.LastModifiedBy = userId;
            tracker.Section = trackerBuss.Section;
            tracker.SubSection = trackerBuss.SubSection;
            tracker.ProfileId = trackerBuss.ProfileId;
            tracker.Url = trackerBuss.url;


            return tracker;
        }

        private List<ProfileUpdatedData> ConstructOldDataForCCo(dynamic val, dynamic newVal, ref List<ProfileUpdatedData> updateDataList)
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
                                        var DataList = ConstructOldDataForCCo(i.Value, d.Value, ref updateDataList);
                                    }
                                    else
                                    {
                                        ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                                        profileNewData.FieldName = i.Key;
                                        profileNewData.OldValue = i.Value != null ? i.Value.ToString() : null;
                                        profileNewData.NewValue = d.Value != null ? d.Value.ToString() : null;
                                        updateDataList.Add(profileNewData);
                                    }
                                }

                            }
                        }

                    }
                    else if (item.Value is System.Collections.ICollection && data.Value is System.Collections.ICollection)
                    {
                        var DataList = ConstructOldDataForCCo(item.Value, data.Value, ref updateDataList);
                    }
                    else
                    {
                        ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                        profileNewData.FieldName = item.Key;
                        profileNewData.OldValue = item.Value != null ? item.Value.ToString() : null;
                        profileNewData.NewValue = data.Value != null ? data.Value.ToString() : null;
                        updateDataList.Add(profileNewData);

                    }
                    
                }
               
            }

            return updateDataList;
        }

        private List<ProfileUpdatedData> ConstructNewDataForCCo(dynamic val, ref List<ProfileUpdatedData> updateDataList)
        {
            //List<ProfileUpdatedData> updateDataList = new List<ProfileUpdatedData>();
            //List<ProfileUpdatedData> finalList = new List<ProfileUpdatedData>();

            foreach (var item in val)
            {
                if (item is Dictionary<string, object>)
                {
                    foreach (var i in item)
                    {
                        if (i.Value is System.Collections.ICollection)
                        {
                            var DataList = ConstructNewDataForCCo(i.Value, ref updateDataList);
                            //foreach (var item1 in DataList)
                            //{
                            //    updateDataList.Add(item1);
                            //}
                        }
                        else
                        {
                            ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                            profileNewData.FieldName = i.Key;
                            profileNewData.NewValue = i.Value != null ? i.Value.ToString() : null;
                            updateDataList.Add(profileNewData);
                        }
                    }
                    
                }
                else if (item.Value is System.Collections.ICollection)
                {
                    var DataList = ConstructNewDataForCCo(item.Value, ref updateDataList);
                    //foreach (var item1 in DataList)
                    //{
                    //    updateDataList.Add(item1);
                    //}
                }
                else
                {
                    ProfileUpdatedData profileNewData = new ProfileUpdatedData();
                    profileNewData.FieldName = item.Key;
                    profileNewData.NewValue = item.Value != null ? item.Value.ToString() : null;
                    updateDataList.Add(profileNewData);

                }

            }

            return updateDataList;
        }

        
    }
}
