using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Coding.Models.PowerDriveService
{
    public class UploadFileDTO
    {
       // public List<DocumentAndStream> DocumentAndStreams { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
       // public TransferType TransferType { get; set; }
    }
}
