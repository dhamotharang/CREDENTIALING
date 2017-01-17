using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account
{
    public class Organization
    {
        public Organization()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int OrganizationID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public int OrganizationTypeId { get; set; }
        [ForeignKey("OrganizationTypeId")]
        public OrganizationType OrganizationType { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<PracticingGroup> PracticingGroups { get; set; }
        public virtual ICollection<AHC.CD.Entities.MasterData.Account.Branch.Facility> Facilities { get; set; }

        #region Status

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

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
