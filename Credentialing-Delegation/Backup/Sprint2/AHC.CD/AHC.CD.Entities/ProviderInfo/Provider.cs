using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.ProviderInfo
{
    public abstract class Provider
    {
        

        public Provider()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        public int ProviderID
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }

        public string UniqueProviderCode
        {
            get;
            set;
        }

        [Column(TypeName = "datetime2")]
        public DateTime? LastUpdatedDateTime
        {
            get;
            set;
        }
        [ForeignKey("ProviderType")]
        public int ProviderTypeID { get; set; }
        
        public virtual ProviderType ProviderType
        {
            get;
            set;
        }

        [Required]
        public virtual ProviderRelation Relation
        {
            get;
            set;
        }

        public virtual ProviderStatus ProviderStatus
        {
            get;
            set;
        }

        public virtual ContractInfo ContractInfo
        {
            get;
            set;
        }

        [Required]
        public virtual bool IsPartOfGroup
        {
            get;
            set;
        }

        [ForeignKey("Group")]
        public int? GroupID { get; set; }
        
        public virtual Group Group { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
