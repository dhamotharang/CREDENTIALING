using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfileReviewSection
{
    public class ProfileReviewSection
    {
        public ProfileReviewSection()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfileReviewSectionID { get; set; }

        public int? ProfileSectionID { get; set; }
        [ForeignKey("ProfileSectionID")]
        public ProfileSection ProfileSection { get; set; }

        public int? ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public Profile Profile { get; set; }    

        #region Display

        public string Display { get; set; }

        [NotMapped] 
        public DisplayType? DisplayType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Display))
                    return null;

                return (DisplayType)Enum.Parse(typeof(DisplayType), this.Display);
            }
            set
            {
                this.Display = value.ToString();
            }
        }

        #endregion        

        #region Status

        public string Status { get; set; }

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
