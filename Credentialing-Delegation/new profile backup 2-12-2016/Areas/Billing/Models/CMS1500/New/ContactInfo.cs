using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ContactInfo
    {
        [Key]
        public long ContactInfo_PK_ID { get; set; }
        public string Email { get; set; }

        [Display(Name = "Telephone")]
        public string Phone { get; set; }
        public Nullable<long> Address_FK_Id { get; set; }

        [ForeignKey("Address_FK_Id")]
        public virtual Address Address { get; set; }
    }
}