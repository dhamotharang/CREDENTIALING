using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    /// <summary>
    /// DL
    /// Passport
    /// Resume
    /// </summary>
    public class DocumentType
    {
        public int DocumentTypeID { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        [Required]
        [ForeignKey("DocumentCategory")]
        public int DocumentCategoryID { get; set; }

        public virtual DocumentCategory DocumentCategory { get; set; }
    }
}
