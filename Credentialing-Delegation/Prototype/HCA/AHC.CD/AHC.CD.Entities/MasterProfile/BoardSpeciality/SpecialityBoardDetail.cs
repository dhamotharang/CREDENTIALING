using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpeciality
{
    public class SpecialityBoardDetail
    {
        public SpecialityBoardDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int SpecialityBoardDetailID { get; set; }

        #region BoardCertified Enum Mapping

        [Required]
        public string IsBoardCertified { get; private set; }

        [NotMapped]
        public YesNoOption YesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsBoardCertified);
            }
            set
            {
                this.IsBoardCertified = value.ToString();
            }
        }
        
        #endregion        

        public SpecialityBoardCetifiedDetail SpecialityBoardCetifiedDetail { get; set; }

        public SpecialityBoardNotCertifiedDetail SpecialityBoardNotCertifiedDetail { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
