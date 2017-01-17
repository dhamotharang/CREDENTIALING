using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile.Demographics;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailGroup
    {
        public EmailGroup()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        public int EmailGroupID { get; set; }

        public string EmailGroupName { get; set; }

        public ICollection<EmailDetail> GroupEmailDetails { get; set; }

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
