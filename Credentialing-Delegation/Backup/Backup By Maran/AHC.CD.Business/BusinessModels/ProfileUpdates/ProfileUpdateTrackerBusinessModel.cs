using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.ProfileUpdates
{
    public class ProfileUpdateTrackerBusinessModel
    {
        public string Section { get; set; }

        public string SubSection { get; set; }
       
        public string ModificationType { get; set; }
       
        public int ProfileId { get; set; }        

        public string userAuthId { get; set; }

        public int objId { get; set; }

        public string url { get; set; }

        public string IncludeProperties { get; set; }

        public string UniqueData { get; set; }
    }
}
