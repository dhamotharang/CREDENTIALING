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
        public YesNoOption? IsResidentOfUSAYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsResidentOfUSA))
                    return null;

                if (this.IsResidentOfUSA.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsResidentOfUSA);
            }
            set
            {
                this.IsResidentOfUSA = value.ToString();
            }
        }
        
        #endregion               

        #region IsAuthorizedToWorkInUS

        [Required]
        public string IsAuthorizedToWorkInUS { get; private set; }

        [NotMapped]
        public YesNoOption? IsAuthorizedToWorkInUSYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsAuthorizedToWorkInUS))
                    return null;

                if (this.IsAuthorizedToWorkInUS.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsAuthorizedToWorkInUS);
            }
            set
            {
                this.IsAuthorizedToWorkInUS = value.ToString();
            }
        }

        #endregion

        public virtual VisaInfo VisaInfo { get; set; }

        public ICollection<VisaDetailHistory> VisaDetailHistory { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }    
}
