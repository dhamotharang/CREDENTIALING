using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class DateQlfr
    {
        public DateQlfr()
        {
         //   this.ClaimDate = new HashSet<ClaimDate>();
        }
        [Key]
        public int DateQlfr_PK_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }

     //   public virtual ICollection<ClaimDate> ClaimDate { get; set; }
    }
}