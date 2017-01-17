using AHC.CD.Business.BusinessModels.TaskTracker;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.TaskTracker;
using AHC.CD.Entities;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.Notification;
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
                    task.HospitalID = taskTracker.HospitalID;
                    task.Notes = taskTracker.Notes;
                    task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                    task.Subject = taskTracker.Subject;
                    task.SubSectionName = taskTracker.SubSectionName;
                    task.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    task.ProfileID = taskTracker.ProfileID;
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
                    task.HospitalID = taskTracker.HospitalID;
                    task.Notes = taskTracker.Notes;
                    task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                    task.Subject = taskTracker.Subject;
                    task.SubSectionName = taskTracker.SubSectionName;
                    task.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    task.ProfileID = taskTracker.ProfileID;
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
            var task = taskRepo.GetTaskById(taskTracker.TaskTrackerId);
           
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
            if (GetUserId(taskTracker.AssignedToAuthId) == task.AssignedToId)
            {
                task.AssignedById = task.AssignedById;
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
                task.HospitalID = taskTracker.HospitalID;
                task.Notes = taskTracker.Notes;
                task.Subject = taskTracker.Subject;
                task.SubSectionName = taskTracker.SubSectionName;
                task.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                task.ProfileID = taskTracker.ProfileID;
                task.TaskTrackerId = taskTracker.TaskTrackerId;
                task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                task.LastModifiedDate = DateTime.Now;
            }
            //else if (GetUserId(taskTracker.AssignedToAuthId) != task.AssignedById && GetUserId(taskTracker.AssignedToAuthId) != task.AssignedToId)
            else if (GetUserId(taskTracker.AssignedToAuthId) != task.AssignedToId)
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
                task.HospitalID = taskTracker.HospitalID;
                task.Notes = taskTracker.Notes;
                task.Subject = taskTracker.Subject;
                task.SubSectionName = taskTracker.SubSectionName;
                task.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                task.ProfileID = taskTracker.ProfileID;
                task.TaskTrackerId = taskTracker.TaskTrackerId;
                task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                task.LastModifiedDate = DateTime.Now;
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
                task.AssignedToId = GetUserId(taskTracker.AssignedToAuthId);
                task.ModeOfFollowUp = taskTracker.ModeOfFollowUp;
                if (taskTracker.InsuranceCompanyName != null)
                {
                    task.InsuaranceCompanyNameID = GetInsuranceCompanyId(taskTracker.InsuranceCompanyName);
                }
                else {
                    task.InsuaranceCompanyNameID = null;
                }
                task.HospitalID = taskTracker.HospitalID;
                task.Notes = taskTracker.Notes;
                task.Subject = taskTracker.Subject;
                task.SubSectionName = taskTracker.SubSectionName;
                task.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                task.ProfileID = taskTracker.ProfileID;
                task.TaskTrackerId = taskTracker.TaskTrackerId;
                task.NextFollowUpDate = taskTracker.NextFollowUpDate;
                task.LastModifiedDate = DateTime.Now;
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
                if(task.AssignedById != task.AssignedToId)
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

        #region Private Methods

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

        #endregion

    }
}
