using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.UM.Models.PowerDriveService
{
    public class Document
    {
        public string FileName { get; set; }
        public byte[] bytes { get; set; }
        public TransferType TransferType { get; set; }
        public string Extension { get; set; }
        public bool IsPresent { get; set; }
    }
}
