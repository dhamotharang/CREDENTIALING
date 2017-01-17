using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class OtherConditionInformation
    {
        public OtherConditionInformation()
        {
            this.PatientCondition = new HashSet<PatientCondition>();
        }
        [Key]
        public int OtherConditionInformation_PK_Id { get; set; }
        public string StateID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }

        public virtual ICollection<PatientCondition> PatientCondition { get; set; }
    }
}