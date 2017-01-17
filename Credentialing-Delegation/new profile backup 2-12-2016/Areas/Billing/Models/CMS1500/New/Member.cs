using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Member
    {
        public Member()
        {
            this.Schedule = new List<Schedule>();
            this.Subscriber = new List<Subscriber>();
        }

        [Key]
        public long Member_PK_Id { get; set; }

        [Display(Name = "First")]
        public string FirstName { get; set; }


        [Display(Name = "Last")]
        public string LastName { get; set; }


        [Display(Name = "MI")]
        public string MiddleName { get; set; }

         [Display(Name = "Patient Birth Date")]
        public DateTime DateOfBirth { get; set; }

         [Display(Name = "Sex")]
         public string Gender { get; set; }

        public string MemberUniqueId { get; set; }
        public Nullable<long> ContactInfo_FK_Id { get; set; }

          [Display(Name = "Patient Relationship to Insured")]
        public string PatientRelationShipCode { get; set; }

        [ForeignKey("ContactInfo_FK_Id")]
        public virtual ContactInfo ContactInfo { get; set; }
        public virtual IList<Schedule> Schedule { get; set; }
        public virtual IList<Subscriber> Subscriber { get; set; }
    }
}