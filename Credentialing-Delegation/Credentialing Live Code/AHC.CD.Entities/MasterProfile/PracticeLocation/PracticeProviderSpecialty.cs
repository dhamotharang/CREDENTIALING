using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeProviderSpecialty
    {
        public int PracticeProviderSpecialtyId { get; set; }

        public int SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public virtual Specialty Specialty { get; set; }

        public string Status { get; set; }

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

    }
}
