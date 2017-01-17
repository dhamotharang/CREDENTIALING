using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Language
{
    public class FacilityLanguage
    {
        public FacilityLanguage()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FacilityLanguageID { get; set; }

        #region AmericanSignLanguage

        [Required]
        public string AmericanSignLanguage { get; private set; }

        [NotMapped]
        public YesNoOption? AmericanSignLanguageYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.AmericanSignLanguage))
                    return null;

                if (this.AmericanSignLanguage.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AmericanSignLanguage);
            }
            set
            {
                this.AmericanSignLanguage = value.ToString();
            }
        }

        #endregion

        public ICollection<NonEnglishLanguage> NonEnglishLanguages { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
