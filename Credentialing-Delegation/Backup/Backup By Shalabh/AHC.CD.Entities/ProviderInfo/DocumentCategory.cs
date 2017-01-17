using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    /// <summary>
    /// Personal
    /// License
    /// Miscellaneous
    /// </summary>
    public class DocumentCategory
    {
        public int DocumentCategoryID { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<DocumentType> DocumentTypes { get; set; }
    }
}
