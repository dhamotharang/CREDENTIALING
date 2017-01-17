using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DTO
{
    public class DocumentDTO
    {
        public int ProviderID { get; set; }
        public int DocumentTypeID { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
        public string ApplicationRootFolder { get; set; }
    }
}
