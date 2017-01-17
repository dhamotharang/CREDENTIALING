using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class NDCData
    {
        public NDCData()
        {
            this.Procedure = new HashSet<Procedure>();
        }
        [Key]
        public long NDCData_PK_Id { get; set; }
        public string LineNote { get; set; }
        public Nullable<int> LineNoteId { get; set; }
        public Nullable<System.TimeSpan> AnesthesiaStartTime { get; set; }
        public Nullable<System.TimeSpan> AnesthesiaEndTime { get; set; }
        public Nullable<int> NDCQlfrId { get; set; }
        public string NDCCode { get; set; }

        public Nullable<decimal> NDCUnitPrice { get; set; }
        public Nullable<double> NDCQuantity { get; set; }
        public Nullable<int> NDCQuantityQlfrID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string NDCQuantityQlfrCode { get; set; }
        public string NDCQlfrCode { get; set; }

        public virtual ICollection<Procedure> Procedure { get; set; }
    }
}
