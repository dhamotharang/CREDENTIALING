using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.CCMPortal
{
   public class CCMQuickActionDTO
    {
        public CCMQuickActionDTO()
        {
            QuickActionSet = new List<QuickActionIdSetDTO>();
        }
        public List<QuickActionIdSetDTO> QuickActionSet { get; set; }

        public string SignaturePath { get; set; }

        public DateTime? SignedDate { get; set; }

        public string SignedByID { get; set; }

        public string RemarksForAppointments { get; set; }

        public CCMApprovalStatusType AppointmentsStatus { get; set; }

    }
 
}
