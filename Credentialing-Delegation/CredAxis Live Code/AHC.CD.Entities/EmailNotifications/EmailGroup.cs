using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile.Demographics;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterData.Enums;
using System.ComponentModel.DataAnnotations;

namespace AHC.CD.Entities.EmailNotifications
{
    public class EmailGroup
    {
        public EmailGroup()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        public int EmailGroupID { get; set; }
        
        [Required]
        public string EmailGroupName { get; set; }

        [Required]
        public string Description { get; set; }

        public int? LastUpdatedBy { get; set; }
        [ForeignKey("LastUpdatedBy")]
        public CDUser LastUpdateByUser { get; set; }

        public int? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public CDUser CreatedByUser { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedOn { get; set; }

        public List<CDUser_GroupEmail> CDUserGroupMails { get; set; }

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
