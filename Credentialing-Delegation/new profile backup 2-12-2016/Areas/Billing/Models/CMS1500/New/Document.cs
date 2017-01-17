using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Document
    {
        [Key]
        public long Document_PK_Id { get; set; }
        public string DocumentKey { get; set; }
        public string NameOfDocument { get; set; }
        public long DocumentType_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string MemberID { get; set; }
        [ForeignKey("DocumentType_FK_Id")]
        public virtual DocumentType DocumentType { get; set; }
        //public virtual ICollection<EncounterDocument> EncounterDocuments { get; set; }
    }
}