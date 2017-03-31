using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.DTO
{
    public class FollowUpMailDTO
    {
        public int EmailInfoID { get; set; }

        public string Subject { get; set; }

        public string SendingDate { get; set; }

        public string Recepients { get; set; }

        public string NextFollowUpDate { get; set; }
    }
}
