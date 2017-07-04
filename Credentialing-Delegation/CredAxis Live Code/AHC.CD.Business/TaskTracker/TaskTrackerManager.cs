using AHC.CD.Business.BusinessModels.TaskTracker;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.TaskTracker;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.Notification;
using AHC.CD.Entities.TaskTracker;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.TaskTracker
{
    internal class TaskTrackerManager : ITaskTrackerManager
    {
        ITaskTrackerRepository taskRepo = null;
        private IGenericRepository<CDUser> cdUserRepo;
        IUnitOfWork uow = null;
        private IRepositoryManager repositoryManager = null;

        public TaskTrackerManager(IUnitOfWork uow, IRepositoryManager repositoryManager)
        {
            this.taskRepo = uow.GetTaskTrackerRepository();
            this.uow = uow;
            cdUserRepo = uow.GetGenericRepository<CDUser>();
            this.repositoryManager = repositoryManager;
        }

        public AHC.CD.Entities.TaskTracker.TaskTracker AddTask(TaskTrackerBusinessModel taskTracker, string ActionPerformedBy)
        {
            try
            {
                AHC.CD.Entities.TaskTracker.TaskTracker task = new AHC.CD.Entities.TaskTracker.TaskTracker();
                AHC.CD.Entities.TaskTracker.TaskTracker taskTemporary = new AHC.CD.Entities.TaskTracker.TaskTracker();
                if (GetUserId(taskTracker.AssignedByAuthId) == GetUserId(taskTracker.AssignedToAuthId))
                {
                    task.AssignedById = GetUserId(taskTracker.AssignedByAuthId);
                    task.AssignedToId = GetUserId(taskTracker.AssignedToAuthId);
                    task.ModeOfFollowUp = taskTracker.ModeOfFollowUp;
                    if (taskTracker.InsuranceCompanyName != null)
                    {
                        task.InsuaranceCompanyNameID = GetInsuranceCompanyId(taskTracker.InsuranceCompanyName);
                    }
                    else
                    {
                        task.InsuaranceCompanyNameID = null;
                    }
                    if (taskTracker.PlanName != null)
                    {
                        task.PlanID = GetPlanId(taskTracker.PlanName);
                    }
                    else
                    {
                        task.PlanID = null;
                    }
                    //task.InsuaranceCompanyNames = taskTracker.InsuranceCompanyName;
                    task.HospitalID = taskTracker.HospitalID;
                    task.Notes = taskTracker.Notes;
                    task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                    task.Subject = taskTracker.Subject;
                    task.SubSectionName = taskTracker.SubSectionName;
                    task.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN;
                    task.ProfileID = taskTracker.ProfileID;
                    task.LastUpdatedBy = ActionPerformedBy;
                    taskTemporary = taskRepo.AddTask(task);
                    return taskTemporary;
                }
                else
                {
                    task.AssignedById = GetUserId(taskTracker.AssignedByAuthId);
                    task.AssignedToId = GetUserId(taskTracker.AssignedToAuthId);
                    task.ModeOfFollowUp = taskTracker.ModeOfFollowUp;
                    if (taskTracker.InsuranceCompanyName != null)
                    {
                        task.InsuaranceCompanyNameID = GetInsuranceCompanyId(taskTracker.InsuranceCompanyName);
                    }
                    else
                    {
                        task.InsuaranceCompanyNameID = null;
                    }
                    if (taskTracker.PlanName != null)
                    {
                        task.PlanID = GetPlanId(taskTracker.PlanName);
                    }
                    else
                    {
                        task.PlanID = null;
                    }
                    //task.InsuaranceCompanyNames = taskTracker.InsuranceCompanyName;
                    task.HospitalID = taskTracker.HospitalID;
                    task.Notes = taskTracker.Notes;
                    task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                    task.Subject = taskTracker.Subject;
                    task.SubSectionName = taskTracker.SubSectionName;
                    task.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN;
                    task.ProfileID = taskTracker.ProfileID;
                    task.LastUpdatedBy = ActionPerformedBy;
                    taskTemporary = taskRepo.AddTask(task);
                    CDUser cdUser = cdUserRepo.Find(c => c.CDUserID == task.AssignedToId, "DashboardNotifications");
                    cdUser.DashboardNotifications.Add(new UserDashboardNotification
                    {
                        StatusType = Entities.MasterData.Enums.StatusType.Active,
                        AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                        Action = "Daily task",
                        ActionPerformed = "New Task Assigned.",
                        ActionPerformedByUser = ActionPerformedBy,
                        RedirectURL = "/TaskTracker/Index"
                    });
                    cdUserRepo.Update(cdUser);
                    cdUserRepo.Save();
                    return taskTemporary;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AHC.CD.Entities.TaskTracker.TaskTracker UpdateTask(TaskTrackerBusinessModel taskTracker, string ActionPerformedBy)
        {
            AHC.CD.Entities.TaskTracker.TaskTracker taskTemporary = new AHC.CD.Entities.TaskTracker.TaskTracker();
            var assignedtoauthid = GetUserId(taskTracker.AssignedToAuthId);
            var assignedbyauthid = GetUserId(taskTracker.AssignedByAuthId);
            var task = taskRepo.GetTaskById(taskTracker.TaskTrackerId);
            if (task.TaskTrackerHistories == null)
            {
                task.TaskTrackerHistories = new List<TaskTrackerHistory>();
            }
            task.TaskTrackerHistories.Add(GetTaskTrackerHistoryData(task, ActionPerformedBy));

            //if (GetUserId(taskTracker.AssignedToAuthId) == GetUserId(taskTracker.AssignedByAuthId))
            //{
            //    task.AssignedById = GetUserId(taskTracker.AssignedByAuthId);
            //    task.AssignedToId = GetUserId(taskTracker.AssignedToAuthId);
            //    task.ModeOfFollowUp = taskTracker.ModeOfFollowUp;
            //    if (taskTracker.InsuranceCompanyName != null)
            //    {
            //        task.InsuaranceCompanyNameID = GetInsuranceCompanyId(taskTracker.InsuranceCompanyName);
            //    }
            //    else {
            //        task.InsuaranceCompanyNameID = null;
            //    }
            //    task.HospitalID = taskTracker.HospitalID;
            //    task.Notes = taskTracker.Notes;
            //    task.Subject = taskTracker.Subject;
            //    task.SubSectionName = taskTracker.SubSectionName;
            //    task.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
            //    task.ProfileID = taskTracker.ProfileID;
            //    task.TaskTrackerId = taskTracker.TaskTrackerId;
            //    task.NextFollowUpDate = taskTracker.NextFollowUpDate;
            //}
            if (assignedtoauthid == task.AssignedToId)
            {
                task.AssignedById = task.AssignedById;
                task.AssignedToId = assignedtoauthid;
                task.ModeOfFollowUp = taskTracker.ModeOfFollowUp;
                if (taskTracker.InsuranceCompanyName != null)
                {
                    task.InsuaranceCompanyNameID = GetInsuranceCompanyId(taskTracker.InsuranceCompanyName);
                }
                else
                {
                    task.InsuaranceCompanyNameID = null;
                }

                if (taskTracker.PlanName != null)
                {
                    task.PlanID = GetPlanId(taskTracker.PlanName);
                }
                else
                {
                    task.PlanID = null;
                }
                //task.InsuaranceCompanyNames = taskTracker.InsuranceCompanyName;
                task.HospitalID = taskTracker.HospitalID;
                task.Notes = taskTracker.Notes;
                task.Subject = taskTracker.Subject;
                task.SubSectionName = taskTracker.SubSectionName;
                task.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN;
                task.ProfileID = taskTracker.ProfileID;
                task.TaskTrackerId = taskTracker.TaskTrackerId;
                task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                task.LastModifiedDate = DateTime.Now;
                task.LastUpdatedBy = ActionPerformedBy;
            }
            //else if (GetUserId(taskTracker.AssignedToAuthId) != task.AssignedById && GetUserId(taskTracker.AssignedToAuthId) != task.AssignedToId)
            else if (assignedtoauthid != task.AssignedToId && assignedtoauthid != assignedbyauthid)
            {
                task.AssignedById = assignedbyauthid;
                task.AssignedToId = assignedtoauthid;
                task.ModeOfFollowUp = taskTracker.ModeOfFollowUp;
                if (taskTracker.InsuranceCompanyName != null)
                {
                    task.InsuaranceCompanyNameID = GetInsuranceCompanyId(taskTracker.InsuranceCompanyName);
                }
                else
                {
                    task.InsuaranceCompanyNameID = null;
                }

                if (taskTracker.PlanName != null)
                {
                    task.PlanID = GetPlanId(taskTracker.PlanName);
                }
                else
                {
                    task.PlanID = null;
                }

                //task.InsuaranceCompanyNames = taskTracker.InsuranceCompanyName;
                task.HospitalID = taskTracker.HospitalID;
                task.Notes = taskTracker.Notes;
                task.Subject = taskTracker.Subject;
                task.SubSectionName = taskTracker.SubSectionName;
                task.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN;
                task.ProfileID = taskTracker.ProfileID;
                task.TaskTrackerId = taskTracker.TaskTrackerId;
                task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                task.LastModifiedDate = DateTime.Now;
                task.LastUpdatedBy = ActionPerformedBy;
                CDUser cdUser = cdUserRepo.Find(c => c.CDUserID == task.AssignedToId, "DashboardNotifications");
                cdUser.DashboardNotifications.Add(new UserDashboardNotification
                {
                    StatusType = Entities.MasterData.Enums.StatusType.Active,
                    AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                    Action = "Daily task",
                    ActionPerformed = "New Task Assigned.",
                    ActionPerformedByUser = ActionPerformedBy,
                    RedirectURL = "/TaskTracker/Index"
                });
                cdUserRepo.Update(cdUser);
                cdUserRepo.Save();
            }
            else
            {
                task.AssignedById = task.AssignedById;
                task.AssignedToId = assignedtoauthid;
                task.ModeOfFollowUp = taskTracker.ModeOfFollowUp;
                if (taskTracker.InsuranceCompanyName != null)
                {
                    task.InsuaranceCompanyNameID = GetInsuranceCompanyId(taskTracker.InsuranceCompanyName);
                }
                else
                {
                    task.InsuaranceCompanyNameID = null;
                }

                if (taskTracker.PlanName != null)
                {
                    task.PlanID = GetPlanId(taskTracker.PlanName);
                }
                else
                {
                    task.PlanID = null;
                }
                //task.InsuaranceCompanyNames = taskTracker.InsuranceCompanyName;
                task.HospitalID = taskTracker.HospitalID;
                task.Notes = taskTracker.Notes;
                task.Subject = taskTracker.Subject;
                task.SubSectionName = taskTracker.SubSectionName;
                task.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN;
                task.ProfileID = taskTracker.ProfileID;
                task.TaskTrackerId = taskTracker.TaskTrackerId;
                task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                task.LastModifiedDate = DateTime.Now;
                task.LastUpdatedBy = ActionPerformedBy;
                //CDUser cdUser = cdUserRepo.Find(c => c.CDUserID == task.AssignedToId, "DashboardNotifications");
                //cdUser.DashboardNotifications.Add(new UserDashboardNotification
                //{
                //    StatusType = Entities.MasterData.Enums.StatusType.Active,
                //    AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                //    Action = "Daily task",
                //    ActionPerformed = "New Task Assigned.",
                //    ActionPerformedByUser = ActionPerformedBy,
                //    RedirectURL = "/TaskTracker/Index"
                //});
                //cdUserRepo.Update(cdUser);
                //cdUserRepo.Save();
            }
            taskTemporary = taskRepo.UpdateTask(task);
            return taskTemporary;
        }

        public void InactiveTask(int trackerId, string ActionPerformedBy)
        {
            try
            {
                taskRepo.InactiveTask(trackerId);
                Entities.TaskTracker.TaskTracker task = uow.GetGenericRepository<Entities.TaskTracker.TaskTracker>().Find(t => t.TaskTrackerId == trackerId);
                if (task.AssignedById != task.AssignedToId)
                {
                    CDUser cdUser = cdUserRepo.Find(c => c.CDUserID == task.AssignedById, "DashboardNotifications");
                    cdUser.DashboardNotifications.Add(new UserDashboardNotification
                    {
                        StatusType = Entities.MasterData.Enums.StatusType.Active,
                        AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                        Action = "Daily task",
                        ActionPerformed = "Task Completed.",
                        ActionPerformedByUser = ActionPerformedBy,
                        RedirectURL = "/TaskTracker/Index"
                    });
                    cdUserRepo.Update(cdUser);
                    cdUserRepo.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ReactiveTask(int trackerId, string ActionPerformedBy, string authid)
        {
            try
            {
                int trackerID = GetUserId(authid);
                taskRepo.ReactiveTask(trackerId, trackerID, ActionPerformedBy);
                Entities.TaskTracker.TaskTracker task = uow.GetGenericRepository<Entities.TaskTracker.TaskTracker>().Find(t => t.TaskTrackerId == trackerId);
                if (task.AssignedById != task.AssignedToId)
                {
                    CDUser cdUser = cdUserRepo.Find(c => c.CDUserID == task.AssignedById, "DashboardNotifications");
                    cdUser.DashboardNotifications.Add(new UserDashboardNotification
                    {
                        StatusType = Entities.MasterData.Enums.StatusType.Active,
                        AcknowledgementStatusType = Entities.MasterData.Enums.AcknowledgementStatusType.Unread,
                        Action = "Daily task",
                        ActionPerformed = "Task Reopened.",
                        ActionPerformedByUser = ActionPerformedBy,
                        RedirectURL = "/TaskTracker/Index"
                    });
                    cdUserRepo.Update(cdUser);
                    cdUserRepo.Save();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetAllProviders()
        {
            try
            {
                return await taskRepo.GetAllProvider();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<object>> GetAllInsuranceCompanies()
        {
            try
            {
                return await repositoryManager.GetAllAsync<InsuaranceCompanyName>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksByUserId(string userAuthId)
        {
            try
            {
                return await taskRepo.GetAllTasksByUserId(userAuthId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //manideep
        public async Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksByProfileId(int profileid)
        {
            try
            {
                return await taskRepo.GetAllTasksByProfileId(profileid);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> SetReminder(List<TaskReminder> reminders, string userAuthID)
        {
            try
            {
                var userID = GetUserId(userAuthID);
                foreach (var reminder in reminders)
                {
                    reminder.ScheduledByID = userID;
                    reminder.StatusType = StatusType.Active;
                }

                return await taskRepo.SetReminder(reminders);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods

        private TaskTrackerHistory GetTaskTrackerHistoryData(Entities.TaskTracker.TaskTracker task, string ActionPerformedBy)
        {
            TaskTrackerHistory TaskTrackerHistoryDATA = new TaskTrackerHistory();
            TaskTrackerHistoryDATA.AssignedByCCOID = task.AssignedById;
            TaskTrackerHistoryDATA.AssignToCCOID = task.AssignedToId;
            TaskTrackerHistoryDATA.HospitalId = task.HospitalID;
            TaskTrackerHistoryDATA.LastModifiedDate = task.LastModifiedDate;
            //TaskTrackerHistoryDATA.InsuaranceCompanyNames = task.InsuaranceCompanyNames;
            TaskTrackerHistoryDATA.InsuaranceCompanyNameID = task.InsuaranceCompanyNameID;
            TaskTrackerHistoryDATA.PlanID = task.PlanID;
            TaskTrackerHistoryDATA.ModeOfFollowUp = task.ModeOfFollowUp;
            TaskTrackerHistoryDATA.NextFollowUpDate = task.NextFollowUpDate;
            TaskTrackerHistoryDATA.Notes = task.Notes;
            TaskTrackerHistoryDATA.ProviderID = task.ProfileID;
            TaskTrackerHistoryDATA.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN;
            TaskTrackerHistoryDATA.Subject = task.Subject;
            TaskTrackerHistoryDATA.SubSectionName = task.SubSectionName;
            TaskTrackerHistoryDATA.LastUpdatedBy = task.LastUpdatedBy;
            return TaskTrackerHistoryDATA;
        }


        public int GetUserId(string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();

                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                return user.CDUserID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetInsuranceCompanyId(string Name)
        {
            try
            {
                var insrepo = uow.GetGenericRepository<InsuaranceCompanyName>();
                var insurance = insrepo.Find(x => x.CompanyName == Name);
                return insurance.InsuaranceCompanyNameID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetPlanId(string Name)
        {
            try
            {
                var insrepo = uow.GetGenericRepository<Plan>();
                var plan = insrepo.Find(x => x.PlanName == Name);
                return plan.PlanID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion






        public async Task<List<TaskReminder>> GetReminders(string userAuthID)
        {
            try
            {
                var userID = GetUserId(userAuthID);

                return await taskRepo.GetReminders(userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DismissReminder(int taskID, string userAuthID)
        {
            try
            {
                var userID = GetUserId(userAuthID);
                bool status = await taskRepo.DismissReminder(taskID, userID);
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> RescheduleReminder(int taskID, DateTime scheduledDateTime, string userAuthID)
        {
            try
            {
                var userID = GetUserId(userAuthID);
                bool status = await taskRepo.RescheduleReminder(taskID, scheduledDateTime,userID);
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DismissAllReminder(int[] taskIDs, string userAuthID)
        {
            try
            {
                var userID = GetUserId(userAuthID);
                bool status = await taskRepo.DismissAllReminder(taskIDs,  userID);
                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public Entities.TaskTracker.TaskTracker GetTaskInfo(int TaskID)
        //{
        //    try
        //    {
        //        return taskRepo.GetTaskById(TaskID);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string GetUserEmail(string AssignedtoID)
        //{
        //    try
        //    {
        //        var email = cdUserRepo.Find(a => a.AuthenicateUserId == AssignedtoID).EmailId;
        //        return email;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
