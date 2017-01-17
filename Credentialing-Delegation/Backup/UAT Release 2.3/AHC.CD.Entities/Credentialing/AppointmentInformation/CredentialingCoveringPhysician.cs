using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.AppointmentInformation
{
    public class CredentialingCoveringPhysician
    {
        public CredentialingCoveringPhysician()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingCoveringPhysicianID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<PracticeProviderType> PracticeProviderTypes { get; set; }

        public virtual ICollection<PracticeProviderSpecialty> PracticeProviderSpecialties { get; set; }

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
