using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AHC.CD.Entities.MasterData.Account;
namespace AHC.CD.Entities.Credentialing
{
   public class PlanContractBEMapping
    {
        public int PlanContractBEMappingId { get; set; }

       public int LOBID { get; set; }
       [ForeignKey("LOBID")]
       public LOB LOB { get; set;}

       public int GroupID { get; set; }
       [ForeignKey("GroupID")]
       public Group BusinessEntity { get; set;}
    }
}
