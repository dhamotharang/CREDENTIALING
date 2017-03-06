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
    /// Date: 18/03/2015
    /// Used for sending the license expiration notification emails to providers.
    /// </summary>
    public class LicenseExpiryNotificationService
    {
        private ISchedulerFactory schedulerFactory = null;
        private IJobDetail expiryEmailNotificationJobDetail = null;
        private ITrigger expiryNotificationCornTrigger = null;
        private IJobDetail expiryDataCollectionJobDetail = null;
        private ITrigger expiryDataCollectionCornTrigger = null;
        private IJobDetail CCOTaskExpiryDataCollection = null;
        private ITrigger CCOTaskExpiryDataCollectionTrigger = null;
        private IJobDetail CCOTaskExpiryEmailSender = null;
        private ITrigger CCOTaskExpiryEmailSenderTrigger = null;
        private IScheduler scheduler = null;
        
        public static readonly LicenseExpiryNotificationService Instance = new LicenseExpiryNotificationService();

        private LicenseExpiryNotificationService()
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

                // Expiry Data Collection Job and Trigger
                expiryDataCollectionJobDetail = JobBuilder.Create<ExpiryDataCollectionJob>()
                        .WithIdentity("expiry-data-collection-job", "expiry-email-group1")
                        .Build();

                expiryDataCollectionCornTrigger = TriggerBuilder.Create()
                        .WithIdentity("expiry-data-collection-corntrigger", "expiry-email-group1")
                        .WithCronSchedule(getExpiryNotificationDataCollectCornScheduler())
                        .ForJob("expiry-data-collection-job", "expiry-email-group1")
                        .Build();

                // Expiry Email Notification Job and Trigger
                expiryEmailNotificationJobDetail = JobBuilder.Create<ExpiryEmailNotificationJob>()
                        .WithIdentity("expiry-email-job-notification", "expiry-email-group2")
                        .Build();

                expiryNotificationCornTrigger = TriggerBuilder.Create()
                        .WithIdentity("expiry-notification-corntrigger", "expiry-email-group2")
                        .WithCronSchedule(getExpiryNotificationSendCornScheduler())
                        .ForJob("expiry-email-job-notification", "expiry-email-group2")
                        .Build();
                CCOTaskExpiryDataCollection = JobBuilder.Create<CCOTaskExpiryDataCollection>()
                        .WithIdentity("task-expiry-data-collection-job", "task-expiry-email-group1")
                        .Build();

                CCOTaskExpiryDataCollectionTrigger = TriggerBuilder.Create()
                        .WithIdentity("task-expiry-data-collection-corntrigger", "task-expiry-email-group1")
                        .WithCronSchedule(getTaskExpiryNotificationDataCollectCornScheduler())
                        .ForJob("task-expiry-data-collection-job", "task-expiry-email-group1")
                        .Build();

                // Expiry Email Notification Job and Trigger
                CCOTaskExpiryEmailSender = JobBuilder.Create<CCOTaskExpiryEmailSender>()
                        .WithIdentity("task-expiry-email-job-notification", "task-expiry-email-group2")
                        .Build();

                CCOTaskExpiryEmailSenderTrigger = TriggerBuilder.Create()
                        .WithIdentity("task-expiry-notification-corntrigger", "task-expiry-email-group2")
                        .WithCronSchedule(getTaskExpiryNotificationSendCornScheduler())
                        .ForJob("task-expiry-email-job-notification", "task-expiry-email-group2")
                        .Build();
                // Schedule the jobs
                scheduler.ScheduleJob(expiryEmailNotificationJobDetail, expiryNotificationCornTrigger);
                scheduler.ScheduleJob(expiryDataCollectionJobDetail, expiryDataCollectionCornTrigger);
                scheduler.ScheduleJob(CCOTaskExpiryEmailSender, CCOTaskExpiryEmailSenderTrigger);
                scheduler.ScheduleJob(CCOTaskExpiryDataCollection, CCOTaskExpiryDataCollectionTrigger);
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

        private string getExpiryNotificationDataCollectCornScheduler()
        {
            // Every Day @1:20AM
            return ConfigurationManager.AppSettings["expiryNotificationDataCollectCornScheduler"].ToString();
        }

        private string getExpiryNotificationSendCornScheduler()
        {
            // Every Day @1:30AM
            return ConfigurationManager.AppSettings["expiryNotificationSendCornScheduler"].ToString();
        }
        private string getTaskExpiryNotificationDataCollectCornScheduler()
        {
            // Every Day @1:20AM
            return ConfigurationManager.AppSettings["taskexpiryNotificationDataCollectCornScheduler"].ToString();
        }

        private string getTaskExpiryNotificationSendCornScheduler()
        {
            // Every Day @1:30AM
            return ConfigurationManager.AppSettings["taskexpiryNotificationSendCornScheduler"].ToString();
        }
    }
}
