using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class DocumentType
    {
        [Key]
        public long DocumentType_PK_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<long> DocumentCategory_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        //  public virtual ICollection<Document> Documents { get; set; }
        [ForeignKey("DocumentCategory_FK_Id")]
        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}