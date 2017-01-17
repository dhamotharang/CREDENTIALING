using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class PersonalInfo
    {
        public PersonalInfo()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        
        public int PersonalInfoID { get; set; }

        [Required]
        public virtual string LastName
        {
            get;
            set;
        }

        [Required]
        public virtual string FirstName
        {
            get;
            set;
        }

        public virtual string MiddleName
        {
            get;
            set;
        }

        [Required]
        public virtual string Title
        {
            get;
            set;
        }

        public virtual string MaidenName
        {
            get;
            set;
        }

        public virtual char Sex
        {
            get;
            set;
        }

        public virtual string OtherNames
        {
            get;
            set;
        }

        [Column(TypeName = "datetime2")]
        public virtual DateTime? LastUpdatedDateTime
        {
            get;
            set;
        }

        [Required]
        public virtual string Email
        {
            get;
            set;
        }

        public virtual MaritalStatus MaritalStatus
        {
            get;
            set;
        }

        public virtual FamilyInfo FamilyInfo
        {
            get;
            set;
        }

       
        public virtual ICollection<PersonalIdentificationInfo> PersonalIdentificationInfos
        {
            get;
            set;
        }

       

    }
}
