using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.JobSchedulingService
{
    /// <summary>
    /// Author: Venkat
    /// Date: 19/03/2015
    /// Runns on a specified interval of time based on the config file. Executes the attached job
    /// </summary>
    public class ChangeNotificationService
    {
        private ISchedulerFactory schedulerFactory = null;

        private IJobDetail changeEmailNotificationJobDetail = null;
        private ITrigger changeNotificationCornTrigger = null;
        private IJobDetail changeEmailNotificationJobDetailForCCM = null;
        private ITrigger changeNotificationCornTriggerForCCM = null;
        private IJobDetail emailNotificationSendingJobDetail = null;
        private ITrigger emailNotificationSendingCornTrigger = null;
        private IScheduler scheduler = null;

        public static readonly ChangeNotificationService Instance = new ChangeNotificationService();

        private ChangeNotificationService()
        {

        }

        public void StartService()
        {
            try
            {
                // construct a scheduler factory
                schedulerFactory = new StdSchedulerFactory();

                // get a scheduler
                scheduler = schedulerFactory.GetScheduler();

                scheduler.Start();
                changeEmailNotificationJobDetail = JobBuilder.Create<ChangeEmailNotificationJob>()
                        .WithIdentity("change-email-job-notification", "change-email-group")
                        .Build();

                changeNotificationCornTrigger = TriggerBuilder.Create()
                        .WithIdentity("changeCorntrigger", "change-email-group")
                        .WithCronSchedule(GetCornSchedule())
                        .ForJob("change-email-job-notification", "change-email-group")
                        .Build();


                changeEmailNotificationJobDetailForCCM = JobBuilder.Create<CCMMailNotification>()
                        .WithIdentity("change-email-job-notification-for-ccm", "change-email-group")
                        .Build();

                changeNotificationCornTriggerForCCM = TriggerBuilder.Create()
                        .WithIdentity("changeCorntriggerforccm", "change-email-group")
                        .WithCronSchedule(GetCornScheduleForCCM())
                        .ForJob("change-email-job-notification-for-ccm", "change-email-group")
                        .Build();

                #region Email Sending Job

                emailNotificationSendingJobDetail = JobBuilder.Create<EmailNotificationSchedulingJob>()
                        .WithIdentity("email-sending-job", "change-email-group")
                        .Build();

                emailNotificationSendingCornTrigger = TriggerBuilder.Create()
                        .WithIdentity("emailCorntrigger", "change-email-group")
                        .WithCronSchedule(GetCornScheduleForSendingEmail())
                        .ForJob("email-sending-job", "change-email-group")
                        .Build();

                #endregion
                scheduler.ScheduleJob(changeEmailNotificationJobDetailForCCM, changeNotificationCornTriggerForCCM);
                scheduler.ScheduleJob(changeEmailNotificationJobDetail, changeNotificationCornTrigger);
                scheduler.ScheduleJob(emailNotificationSendingJobDetail, emailNotificationSendingCornTrigger);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void PauseService()
        {
            scheduler.Standby();
        }

        private string GetCornScheduleForCCM()
        {
            string trigger = ConfigurationManager.AppSettings["changeNotificationForCCMCornScheduler"].ToString();
            return trigger;
        }

        private string GetCornSchedule()
        {
            string trigger = ConfigurationManager.AppSettings["changeNotificationCornScheduler"].ToString();
            return trigger;
        }

        private string GetCornScheduleForSendingEmail()
        {
            string trigger = ConfigurationManager.AppSettings["emailNotificationSendingCornScheduler"].ToString();
            return trigger;
        }

    }
}
