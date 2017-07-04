using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.TaskTracker;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository.TaskTracker
{
    internal class TaskTrackerEFRepository : ITaskTrackerRepository
    {
        IGenericRepository<AHC.CD.Entities.TaskTracker.TaskTracker> taskRepo = null;
        IGenericRepository<AHC.CD.Entities.MasterProfile.Profile> profileRepo = null;
        IGenericRepository<AHC.CD.Entities.TaskTracker.TaskReminder> reminderRepo = null;

        public TaskTrackerEFRepository()
        {
            this.taskRepo = new EFGenericRepository<AHC.CD.Entities.TaskTracker.TaskTracker>();
            this.profileRepo = new EFGenericRepository<AHC.CD.Entities.MasterProfile.Profile>();
            this.reminderRepo = new EFGenericRepository<AHC.CD.Entities.TaskTracker.TaskReminder>();
        }

        public AHC.CD.Entities.TaskTracker.TaskTracker AddTask(Entities.TaskTracker.TaskTracker taskTracker)
        {
            try
            {
                taskRepo.Create(taskTracker);
                taskRepo.Save();
                return taskTracker;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public AHC.CD.Entities.TaskTracker.TaskTracker UpdateTask(Entities.TaskTracker.TaskTracker taskTracker)
        {
            try
            {
                Entities.TaskTracker.TaskTracker task = taskRepo.Find(t => t.TaskTrackerId == taskTracker.TaskTrackerId && ((t.Status == AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN.ToString()) || (t.Status == AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.REOPEN.ToString())));

                task = AutoMapper.Mapper.Map<Entities.TaskTracker.TaskTracker, Entities.TaskTracker.TaskTracker>(taskTracker, task);

                taskRepo.Update(taskTracker);
                taskRepo.Save();
                return taskTracker;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InactiveTask(int taskTrackerId)
        {
            try
            {
                Entities.TaskTracker.TaskTracker task = taskRepo.Find(t => t.TaskTrackerId == taskTrackerId && (t.Status == AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN.ToString() || t.Status == AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.REOPEN.ToString()));
                task.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.CLOSED;
                taskRepo.Update(task);
                taskRepo.Save();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void ReactiveTask(int taskTrackerId, int taskID, string ActionPerformedBy)
        {
            try
            {
                Entities.TaskTracker.TaskTracker task = taskRepo.Find(t => t.TaskTrackerId == taskTrackerId && t.Status == AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.CLOSED.ToString());
                if (task.AssignedById == taskID)
                {
                    task.AssignedToId = task.AssignedById;
                    task.LastUpdatedBy = ActionPerformedBy;
                }
                else
                {
                    task.AssignedById = taskID;
                    task.AssignedToId = taskID;
                    task.LastUpdatedBy = ActionPerformedBy;
                }
                task.StatusType = AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.REOPEN;
                taskRepo.Update(task);
                taskRepo.Save();
            }
            catch (Exception)
            {
                throw;
            }

        }


        public Entities.TaskTracker.TaskTracker GetTaskById(int taskTrackerId)
        {
            try
            {
                //var task = taskRepo.Find(t => t.TaskTrackerId == taskTrackerId && t.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString(), "TaskTrackerHistories,Hospital");
                var task = taskRepo.Find(t => t.TaskTrackerId == taskTrackerId && ((t.Status == AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.OPEN.ToString()) || (t.Status == AHC.CD.Entities.MasterData.Enums.TaskTrackerStatusType.REOPEN.ToString())), "TaskTrackerHistories");

                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public async Task<IEnumerable<object>> GetAllProvider()
        {
            List<object> providers = new List<object>();
            try
            {
                //var profiles = await profileRepo.GetAsync(p => p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), "PersonalDetail, OtherIdentificationNumber,HospitalPrivilegeInformation,HospitalPrivilegeInformation.HospitalPrivilegeDetails");
                var profiles = profileRepo.GetAll("PersonalDetail, OtherIdentificationNumber,HospitalPrivilegeInformation,HospitalPrivilegeInformation.HospitalPrivilegeDetails");

                foreach (var profile in profiles)
                {
                    if (profile != null && !profile.PersonalDetail.FirstName.Contains("Test_"))
                    {
                        if (profile.PersonalDetail != null && profile.PersonalDetail.MiddleName != null)
                            providers.Add(new { ProfileId = profile.ProfileID, Status = profile.Status, FirstName = profile.PersonalDetail.FirstName.Trim(), Name = profile.PersonalDetail.FirstName.Trim() + " " + profile.PersonalDetail.MiddleName.Trim() + " " + profile.PersonalDetail.LastName.Trim() + "-" + profile.OtherIdentificationNumber.NPINumber, HospitalInfo = profile.HospitalPrivilegeInformation });
                        else
                            providers.Add(new { ProfileId = profile.ProfileID, Status = profile.Status, FirstName = profile.PersonalDetail.FirstName.Trim(), Name = profile.PersonalDetail.FirstName.Trim() + " " + profile.PersonalDetail.LastName.Trim() + "-" + profile.OtherIdentificationNumber.NPINumber, HospitalInfo = profile.HospitalPrivilegeInformation });
                    }

                }

                return providers;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksWithUserId(string userAuthId)
        {
            try
            {
                //var tasks = await taskRepo.GetAsync(t => t.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), "AssignedTo,InsuranceCompanyName");
                var tasks = (await taskRepo.GetAsync(x => x.AssignedTo != null && x.AssignedTo.AuthenicateUserId == userAuthId, "AssignedTo,TaskTrackerHistories,InsuranceCompanyName")).ToList().OrderBy(c => c.NextFollowUpDate);
                //var tasksForUser = tasks.Where(s => s.AssignedTo != null && s.AssignedTo.AuthenicateUserId == userAuthId).ToList();

                return tasks;
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
                //var tasks = await taskRepo.GetAsync(t => t.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), "AssignedTo,PlanName");
                //var tasksForUser = tasks.Where(s => s.AssignedTo != null && s.AssignedTo.AuthenicateUserId == userAuthId).ToList();
                var tasks = (await taskRepo.GetAllAsync("AssignedTo,TaskTrackerHistories,PlanName,Provider")).ToList().OrderBy(c => c.NextFollowUpDate);
                return tasks.Where(t=>(!t.Provider.PersonalDetail.FirstName.Contains("Test_")));
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
                var tasks = (await taskRepo.GetAsync(x => x.ProfileID == profileid, "AssignedTo,TaskTrackerHistories,PlanName")).ToList().OrderBy(c => c.NextFollowUpDate);
                return tasks;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> SetReminder(List<Entities.TaskTracker.TaskReminder> reminders)
        {
            try
            {
                foreach (var reminder in reminders)
                {
                    reminderRepo.Create(reminder);
                }
                await reminderRepo.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<Entities.TaskTracker.TaskReminder>> GetReminders(int scheduledByID)
        {
            try
            {
                var taskReminders = await reminderRepo.GetAsync(m => m.ScheduledByID == scheduledByID && m.Status == StatusType.Active.ToString());
                return taskReminders.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> DismissReminder(int taskID, int scheduledByID)
        {
            try
            {
                var taskReminder = await reminderRepo.FindAsync(m => m.TaskReminderID == taskID && m.ScheduledByID == scheduledByID);
                taskReminder.StatusType = StatusType.Inactive;
                reminderRepo.Update(taskReminder);
                reminderRepo.Save();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> RescheduleReminder(int taskID, DateTime scheduledDateTime, int scheduledByID)
        {
            try
            {
                var taskReminder = await reminderRepo.FindAsync(m => m.TaskReminderID == taskID && m.ScheduledByID == scheduledByID);
                taskReminder.ScheduledDateTime = scheduledDateTime;
                reminderRepo.Update(taskReminder);
                reminderRepo.Save();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DismissAllReminder(int[] taskIDs, int scheduledByID)
        {
            try
            {
                var taskReminders = await reminderRepo.GetAsync(m => taskIDs.Contains(m.TaskReminderID) && m.ScheduledByID == scheduledByID);

                foreach (var taskReminder in taskReminders)
                {
                    taskReminder.StatusType = StatusType.Inactive;
                    reminderRepo.Update(taskReminder);
                }
                reminderRepo.Save();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
