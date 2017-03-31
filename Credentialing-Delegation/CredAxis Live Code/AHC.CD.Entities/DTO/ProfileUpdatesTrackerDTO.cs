using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.DTO
{
    public class ProfileUpdatesTrackerDTO
    {
        public int ProfileUpdatesTrackerId { get; set; }

        public string NPINumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Salutation { get; set; }

        public string oldData { get; set; }

        public string NewData { get; set; }

        public string NewConvertedData { get; set; }

        public string Section { get; set; }

        public string SubSection { get; set; }

        public string Url { get; set; }

        public int? RespectiveObjectId { get; set; }

        public string ApprovalStatus { get; set; }

        public string Modification { get; set; }       

        public string RejectionReason { get; set; }

        public int ProfileId { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
