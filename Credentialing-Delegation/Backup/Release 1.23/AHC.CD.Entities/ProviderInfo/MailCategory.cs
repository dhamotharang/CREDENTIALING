using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class MailCategory
    {
        public int MailCategoryID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public virtual ICollection<MailTemplate> MailTemplates
        {
            get;
            set;
        }
    }
}
