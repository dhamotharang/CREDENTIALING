using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Models.PowerDriveService
{
    public class DocumentAndStream
    {
        public Stream InputStream { get; set; }

        public Document Document { get; set; }
    }
}
