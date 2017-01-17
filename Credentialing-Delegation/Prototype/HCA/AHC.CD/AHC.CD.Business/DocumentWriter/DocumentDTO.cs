using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.DocumentWriter
{
    public class DocumentDTO
    {
        //public int IndividualProviderID { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
        public string DocumentFolder { get; set; }
    }
}
