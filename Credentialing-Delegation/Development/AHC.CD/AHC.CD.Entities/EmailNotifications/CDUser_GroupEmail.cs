using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.EmailNotifications
{
    public class CDUser_GroupEmail
    {
        public CDUser_GroupEmail()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        [Key]
        public int CDUserGroupEmailID { get; set; }

        public int? CDUserId { get; set; }
        [ForeignKey("CDUserId")]
        public CDUser CDUser { get; set; }

        public int EmailGroupID { get; set; }
        [ForeignKey("EmailGroupID")]
        public EmailGroup EmailGroup { get; set; }

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

        public int? LastUpdatedBy { get; set; }
        [ForeignKey("LastUpdatedBy")]
        public CDUser LastUpdatedUser { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
