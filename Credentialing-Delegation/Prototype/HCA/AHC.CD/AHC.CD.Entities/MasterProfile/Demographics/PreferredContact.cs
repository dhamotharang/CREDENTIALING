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
        public string ContactType { get; private set; }

        [NotMapped]
        public PreferredContactType PreferredWrittenContactType
        {
            get
            {
                return (PreferredContactType)Enum.Parse(typeof(PreferredContactType), this.ContactType);
            }
            set
            {
                this.ContactType = value.ToString();
            }
        }

        #endregion        

        [Required]
        public int PreferredIndex { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
