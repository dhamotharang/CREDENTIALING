using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class OtherLegalName
    {
        public OtherLegalName()
        {
            LastModifiedDate = DateTime.Now;
            this.StatusType = MasterData.Enums.StatusType.Active;
        }

        public int OtherLegalNameID { get; set; }

        [Required]
        public string OtherFirstName { get; set; }

        public string OtherMiddleName { get; set; }

        [Required]
        public string OtherLastName { get; set; }

        public string Suffix { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        [NotMapped]
        public long DocumentSize { get; set; }

        public string DocumentPath { get; set; }

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

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public ICollection<OtherLegalNameHistory> OtherLegalNameHistory { get; set; }
    }
}
