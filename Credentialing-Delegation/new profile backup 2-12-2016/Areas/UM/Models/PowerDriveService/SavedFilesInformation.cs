using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.Models.PowerDriveService
{
    public class SavedFilesInformation
    {
        public string FileName { get; set; }
        public string FileKey { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
