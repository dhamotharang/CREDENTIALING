using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class MailTemplate
    {
        public int MailTemplateID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public DateTime? MailDate
        {
            get;
            set;
        }

        public virtual MailCategory MailCategory
        {
            get;
            set;
        }
    }
}
