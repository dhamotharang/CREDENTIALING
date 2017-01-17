using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class HCCCode
    {
        [Key]
        public int HCCCode_PK_Id { get; set; }

        public string Code { get; set; }

        public string HCCWeight { get; set; }

        public string Version { get; set; }
    }
}