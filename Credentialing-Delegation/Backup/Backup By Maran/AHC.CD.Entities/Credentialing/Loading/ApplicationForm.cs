using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.Loading
{
    public class ApplicationForm
    {
        public ApplicationForm()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ApplicationFormID { get; set; }

        public string ApplicationFormPath { get; set; }

        public int? CreatedByID { get; set; }
        [ForeignKey("CreatedByID")]
        public CDUser CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

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
    }
}
