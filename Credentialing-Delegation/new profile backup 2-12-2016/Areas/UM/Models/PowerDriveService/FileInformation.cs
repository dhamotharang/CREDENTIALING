using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.Models.PowerDriveService
{
    public class FileInformation
    {
        public string FileKey { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
