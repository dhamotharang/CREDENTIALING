using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities
{
    public class CDUser
    {
        public CDUser()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int CDUserID { get; set; }

        public string AuthenicateUserId { get; set; }

        public int? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public virtual CDUserRelation UserRelation { get; set; }
        public virtual ICollection<CDUserRole> CDRoles { get; set; }

        #region Dashboard Notifications

        public ICollection<UserDashboardNotification> DashboardNotifications { get; set; }

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
