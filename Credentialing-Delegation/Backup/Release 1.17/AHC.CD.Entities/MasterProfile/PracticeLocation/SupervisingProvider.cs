using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class SupervisingProvider
    {

        public SupervisingProvider()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int SupervisingProviderID { get; set; }

        public int ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public virtual Profile Profile { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        public DateTime? ActivationDate { get; set; }

        public DateTime? DeactivationDate { get; set; }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
