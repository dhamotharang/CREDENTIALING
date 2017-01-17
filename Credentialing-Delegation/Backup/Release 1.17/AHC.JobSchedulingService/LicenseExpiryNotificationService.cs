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
        private IJobDetail emailNotificationJobDetail = null;
        private ITrigger cornTrigger = null;
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

                emailNotificationJobDetail = JobBuilder.Create<EmailNotificationJob>()
                        .WithIdentity("email-job-notification", "email-group")
                        .Build();

                cornTrigger = TriggerBuilder.Create()
                        .WithIdentity("corntrigger", "email-group")
                        .WithCronSchedule(GetCornSchedule())
                        .ForJob("email-job-notification", "email-group")
                        .Build();

                scheduler.ScheduleJob(emailNotificationJobDetail, cornTrigger);
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

        private string GetCornSchedule()
        {
            return ConfigurationManager.AppSettings["expiryNotificationCornScheduler"].ToString();
        }
    }
}
