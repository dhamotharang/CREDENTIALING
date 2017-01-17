using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class WorkersCompensationInformation
    {
        public WorkersCompensationInformation()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int WorkersCompensationInformationID { get; set; }
        
        public string WorkersCompensationNumber { get; set; }

        #region CertificationStatus

        public string CertificationStatus { get; private set; }

        [NotMapped]
        public StatusType StatusType
        {
            get
            {
                return (StatusType)Enum.Parse(typeof(StatusType), this.CertificationStatus);
            }
            set
            {
                this.CertificationStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpirationDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
