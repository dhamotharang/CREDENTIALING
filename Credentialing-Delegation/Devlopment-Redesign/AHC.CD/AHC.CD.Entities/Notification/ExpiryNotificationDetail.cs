using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class ExpiryNotificationDetail
    {
        public ExpiryNotificationDetail()
        {
            this.StateLicenseExpiries = new List<StateLicenseExpiry>();
            this.DEALicenseExpiries = new List<DEALicenseExpiry>();
            this.CDSCInfoExpiries = new List<CDSCInfoExpiry>();
            this.SpecialtyDetailExpiries = new List<SpecialtyDetailExpiry>();
            this.HospitalPrivilegeExpiries = new List<HospitalPrivilegeExpiry>();
            this.ProfessionalLiabilityExpiries = new List<ProfessionalLiabilityExpiry>();
            this.WorkerCompensationExpiries = new List<WorkerCompensationExpiry>();
            this.LastModifiedDate = DateTime.Now;
        }

        public int ExpiryNotificationDetailID { get; set; }
        
        public int ProfileId { get; set; }
        public string NPINumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ProviderLevel { get; set; }
        public string ProviderTypes 
        {
            get
            {
                if (ProviderTitles != null)
                    return String.Join(",", this.ProviderTitles);

                return null;
            }
            set
            {
                if(value != null)
                    this.ProviderTitles = value.Split(',').ToList();
            }
        }

        [NotMapped]
        public List<string> ProviderTitles { get; set; }

        public virtual ICollection<StateLicenseExpiry> StateLicenseExpiries { get; set; }
        public virtual ICollection<DEALicenseExpiry> DEALicenseExpiries { get; set; }
        public virtual ICollection<CDSCInfoExpiry> CDSCInfoExpiries { get; set; }
        public virtual ICollection<SpecialtyDetailExpiry> SpecialtyDetailExpiries { get; set; }
        public virtual ICollection<HospitalPrivilegeExpiry> HospitalPrivilegeExpiries { get; set; }
        public virtual ICollection<ProfessionalLiabilityExpiry> ProfessionalLiabilityExpiries { get; set; }
        public virtual ICollection<WorkerCompensationExpiry> WorkerCompensationExpiries { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
