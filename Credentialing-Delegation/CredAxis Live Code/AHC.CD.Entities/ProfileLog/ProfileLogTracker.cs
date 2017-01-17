using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileLog
{
    public class ProfileLogTracker
    {

        public ProfileLogTracker()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfileLogTrackerId { get; set; }

        public int ProfileId { get; set; }

        public string LogedByFullName { get; set; }

        public string LogedByUserName { get; set; }
       
        public string IsLocked { get; set; }

        [NotMapped]
        public YesNoOption? IsLockedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsLocked))
                    return null;

                if (this.IsLocked.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsLocked);
            }
            set
            {
                this.IsLocked = value.ToString();
            }
        }   

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
