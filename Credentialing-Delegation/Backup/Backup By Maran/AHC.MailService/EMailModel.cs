using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.MailService
{
    public class EMailModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Tuple<Stream,string>> Attachments { get; set; }

    }
}
