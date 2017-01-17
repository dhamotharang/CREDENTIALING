using AHC.CD.Entities.MasterData.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
    public class BusinessEntityMapper
    {
        public int BusinessEntityMapperID { get; set; }

        public int GroupID { get; set; }
        [ForeignKey("GroupID")]
        public Group BusinessEntity { get; set; }

        public int PlanID { get; set; }
        [ForeignKey("PlanID")]
        public Plan Plan { get; set; }
    }
}
