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

                scheduler.ScheduleJob(changeEmailNotificationJobDetail, changeNotificationCornTrigger);
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
            string trigger = ConfigurationManager.AppSettings["changeNotificationCornScheduler"].ToString();
            return trigger;
        }

    }
}
