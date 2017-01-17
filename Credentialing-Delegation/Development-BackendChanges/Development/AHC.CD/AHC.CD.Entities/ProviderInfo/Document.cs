using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class Document
    {

        public Document()
        {
            CreatedDate = DateTime.Now;
            DocumentStatus = ProviderInfo.DocumentStatus.Active;
        }
        public int DocumentID { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime? CreatedDate { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public DocumentStatus DocumentStatus { get; set; }

        [Required]
        [ForeignKey("DocumentType")]
        public int DocumentTypeID { get; set; }

        public virtual DocumentType DocumentType { get; set; }
    }


    public enum DocumentStatus : int
    {
        Active,
        Inactive
    }
}
