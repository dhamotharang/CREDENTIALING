using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailTemplate
    {
        public EmailTemplate()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmailTemplateID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        #region Email Detail

        public string Subject { get; set; }

        public string Body { get; set; }

        #endregion

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
