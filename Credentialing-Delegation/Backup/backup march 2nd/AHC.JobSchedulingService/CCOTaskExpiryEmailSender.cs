﻿using AHC.CD.Business.Notification;
using AHC.CD.Data.EFRepository;
using AHC.CD.Data.Repository;
using AHC.MailService;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.JobSchedulingService
{
    /// <summary>
    /// Author: Pavan Jorige
    /// </summary>
    public class CCOTaskExpiryEmailSender : IJob
    {
        private IExpiryNotificationManager expiryNotificationManager = null;

        public CCOTaskExpiryEmailSender()
        {
            expiryNotificationManager = new ExpiryNotificationManager(new EFUnitOfWork(), new EmailSender());
        }

        public void Execute(IJobExecutionContext context)
        {
            expiryNotificationManager.NotifyExpiriesForCCO();
        }
    }
}
