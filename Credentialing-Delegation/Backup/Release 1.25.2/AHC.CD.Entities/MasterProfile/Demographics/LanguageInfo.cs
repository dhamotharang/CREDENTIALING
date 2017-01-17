using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class LanguageInfo
    {
        public LanguageInfo()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int LanguageInfoID { get; set; }

        #region CanSpeakAmericanSignLanguage
        
        [Required]
        public string CanSpeakAmericanSignLanguage { get; set; }

        [NotMapped]
        public YesNoOption? CanSpeakAmericanSignYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.CanSpeakAmericanSignLanguage))
                    return null;

                if (this.CanSpeakAmericanSignLanguage.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CanSpeakAmericanSignLanguage);
            }
            set
            {
                this.CanSpeakAmericanSignLanguage = value.ToString();
            }
        }

        #endregion        

        public virtual ICollection<KnownLanguage> KnownLanguages { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
