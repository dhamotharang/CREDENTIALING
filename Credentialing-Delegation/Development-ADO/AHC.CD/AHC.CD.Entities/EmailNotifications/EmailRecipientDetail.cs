using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailRecipientDetail
    {
        public EmailRecipientDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmailRecipientDetailID { get; set; }

        public string Recipient { get; set; }

        #region Recipient Type

        public string RecipientType { get; set; }

        [NotMapped]
        public RecipientType? RecipientTypeCategory
        {
            get
            {
                if (String.IsNullOrEmpty(this.RecipientType))
                    return null;

                return (RecipientType)Enum.Parse(typeof(RecipientType), this.RecipientType);
            }
            set
            {
                this.RecipientType = value.ToString();
            }
        }

        #endregion

        public ICollection<EmailTracker> EmailTracker { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
