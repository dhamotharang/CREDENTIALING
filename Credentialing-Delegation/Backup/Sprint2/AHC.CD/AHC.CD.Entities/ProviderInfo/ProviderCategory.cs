using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class ProviderCategory
    {
        public virtual int ProviderCategoryID
        {
            get;
            set;
        }

        public virtual string Title
        {
            get;
            set;
        }

        public virtual string Code
        {
            get;
            set;
        }

        public virtual ICollection<ProviderType> ProviderTypes
        {
            get;
            set;
        }

    }
}
