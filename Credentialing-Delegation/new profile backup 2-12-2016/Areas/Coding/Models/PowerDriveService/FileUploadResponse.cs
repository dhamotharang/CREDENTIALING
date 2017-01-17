using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.PowerDriveService
{
    public class FileUploadResponse
    {
        public FileUploadResponse()
        {
            FileInfomations = new List<SavedFilesInformation>();
        }
        public List<SavedFilesInformation> FileInfomations { get; set; }
        public string UserName { get; set; }
    } 
}