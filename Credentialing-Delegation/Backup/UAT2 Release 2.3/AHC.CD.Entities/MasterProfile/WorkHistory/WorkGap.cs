using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class WorkGap
    {
        public WorkGap()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int WorkGapID { get; set; }

        [Column(TypeName="datetime2")]
        [Required]
        public DateTime StartDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }
        
        public string Description { get; set; }

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

        public ICollection<WorkGapHistory> WorkGapHistory { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
