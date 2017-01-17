using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class PatientCondition
    {
        [Key]
        public long PatientCondition_PK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        [Display(Name="Auto Accident?")]
        public Nullable<bool> IsAutAccident { get; set; }

        [Display(Name = "Other Accidents?")]
        public Nullable<bool> IsOtherAccident { get; set; }

        [Display(Name="Employment? (Current Or Previous)")]
        public Nullable<bool> IsEmployeement { get; set; }

        [Display(Name="State")]
        public string AutoAccidentState { get; set; }
        public Nullable<long> Encounter_FK_Id { get; set; }

        [ForeignKey("Encounter_FK_Id")]
        public virtual Encounter Encounter { get; set; }
        //[ForeignKey("OtherInfo_FK_Id")]
        //public virtual OtherConditionInformation OtherConditionInformation { get; set; }
        //[ForeignKey("PatientState_FK_Id")]
        //public virtual PatientConditionFactor PatientConditionFactor { get; set; }
    }
}