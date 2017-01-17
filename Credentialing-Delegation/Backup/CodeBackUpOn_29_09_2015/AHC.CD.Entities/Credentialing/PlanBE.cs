using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
    public class PlanBE
    {
        public PlanBE()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanBEID { get; set; }

        public int PlanID { get; set; }
        [ForeignKey("PlanID")]
        public Plan Plan { get; set; }

        public int OrganizationGroupID { get; set; }
        [ForeignKey("OrganizationGroupID")]
        public OrganizationGroup Group { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
