using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Enums
{
    public enum PreferredContactType
    {
        [Description("Home Phone")]
        HomePhone=1,
        [Description("Fax")]
        Fax,
        [Description("Mobile")]
        Mobile,
        [Description("Email")]
        Email,
        [Description("Pager")]
        Pager
    }
}
