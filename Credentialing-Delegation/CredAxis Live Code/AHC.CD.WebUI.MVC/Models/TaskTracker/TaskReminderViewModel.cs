using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.TaskTracker
{
    public class TaskReminderViewModel
    {
        public TaskReminderViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int TaskReminderID { get; set; }

        public string ReminderInfo { get; set; }

        public int ScheduledByID { get; set; }

        public DateTime CreatedDate { get; set; }        
        
        public string ScheduledDateTime { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public StatusType StatusType { get; set; }
        //Snooze time in minutes
        public int SnoozeTime { get; set; }
    }
}