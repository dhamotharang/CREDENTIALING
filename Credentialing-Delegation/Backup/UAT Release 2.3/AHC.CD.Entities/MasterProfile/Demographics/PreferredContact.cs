using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class PreferredContact
    {
        public PreferredContact()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PreferredContactID { get; set; }

        #region ContactType

        [Required]
        public string ContactType { get; set; }

        [NotMapped]
        public PreferredContactType? PreferredWrittenContactType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ContactType))
                    return null;

                if (this.ContactType.Equals("Not Available"))
                    return null;

                return (PreferredContactType)Enum.Parse(typeof(PreferredContactType), this.ContactType);
            }
            set
            {
                this.ContactType = value.ToString();
            }
        }

        #endregion        

        //[Required]
        public int PreferredIndex { get; set; }

        #region Status

        //[Required]
        public string Status { get; set; }

        [NotMapped]
        public virtual StatusType? StatusType
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

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
