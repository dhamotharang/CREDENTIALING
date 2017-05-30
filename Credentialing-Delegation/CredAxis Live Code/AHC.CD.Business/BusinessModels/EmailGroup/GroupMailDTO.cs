using AHC.CD.Business.Search;
using AHC.CD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.EmailGroup
{
    public class GroupMailDTO
    {
        public int EmailGroupId { get; set; }
        public string EmailGroupName { get; set; }
        public string Description { get; set; }
        public int Cduser_GrpMailId { get; set; }
        //public List<string> Emails { get; set; }
        public CDUser CreatedBy { get; set; }
        public CDUser LastUpdatedBy { get; set; }
        public Dictionary<int?, string> Emails { get; set; }
        public string Status { get; set; }
        public int CurrentCDuserId { get; set; }
        public List<SearchUserforGroupMailDTO> GroupMailUserDetails { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
