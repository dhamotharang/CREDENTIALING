using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHC.CD.Entities.ProviderInfo
{
    //[Table("Groups")]
    public class Group// : Provider
    {
        public int GroupID { get; set; }

        public string GroupName { get; set; }

        //[ForeignKey("ProviderID")]
        public virtual ICollection<Provider> Providers
        {
            get;
            set;
        }


    }
}
