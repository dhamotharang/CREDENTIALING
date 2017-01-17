using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeService
    {
        public PracticeService()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PracticeServiceID { get; set; }

        public ICollection<PracticeServiceQuestionAnswer> PracticeServiceQuestionAnswers { get; set; }

        #region PracticeType

        [Required]
        public int PracticeTypeID { get; set; }
        [ForeignKey("PracticeTypeID")]
        public PracticeType PracticeType { get; set; }

        #endregion

        public string AdditionalOfficeProcedures  { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
