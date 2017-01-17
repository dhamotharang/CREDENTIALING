using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProviderInfo
{
    public class ProviderType
    {
        public virtual string Code
        {
            get;
            set;
        }

        public virtual int ProviderTypeID
        {
            get;
            set;
        }

        public virtual string Title
        {
            get;
            set;
        }

        [ForeignKey("ProviderCategory")]
        public int? ProviderCategoryID { get; set; }

        public virtual ProviderCategory ProviderCategory
        {
            get;
            set;
        }

        public virtual ICollection<Provider> Providers
        {
            get;
            set;
        }

    }
}
