using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class VisaDetail
    {

        public VisaDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int VisaDetailID { get; set; }

        #region IsResidentOfUSA

        [Required]
        public string IsResidentOfUSA { get; private set; }

        [NotMapped]
        public YesNoOption IsResidentOfUSAYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsResidentOfUSA);
            }
            set
            {
                this.IsResidentOfUSA = value.ToString();
            }
        }
        
        #endregion       

        public VisaInfo VisaInfo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }    
}
