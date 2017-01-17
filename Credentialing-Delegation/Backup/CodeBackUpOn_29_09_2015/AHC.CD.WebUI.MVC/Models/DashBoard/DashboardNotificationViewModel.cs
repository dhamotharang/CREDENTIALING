using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.DashBoard
{
    public class DashboardNotificationViewModel
    {
        public int UserDashboardNotificationID { get; set; }

        public string Action { get; set; }

        public string ActionPerformedByUser { get; set; }

        public string ActionPerformed { get; set; }

        public string Description { get; set; }

        #region Acknowledgement Status

        public string AcknowledgementStatus { get; private set; }

        [NotMapped]
        public AcknowledgementStatusType? AcknowledgementStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.AcknowledgementStatus))
                    return null;

                return (AcknowledgementStatusType)Enum.Parse(typeof(AcknowledgementStatusType), this.AcknowledgementStatus);
            }
            set
            {
                this.AcknowledgementStatus = value.ToString();
            }
        }

        #endregion
    }
}