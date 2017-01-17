using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeNonEnglishLanguage
    {
        public PracticeNonEnglishLanguage()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PracticeNonEnglishLanguageID { get; set; }

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

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AmericanSignLanguage);
            }
            set
            {
                this.AmericanSignLanguage = value.ToString();
            }
        }

        #endregion

        #region InterpretersAvailable

        [Required]
        public string InterpretersAvailable { get; private set; }

        [NotMapped]
        public YesNoOption? InterpretersAvailableYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.InterpretersAvailable))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.InterpretersAvailable);
            }
            set
            {
                this.InterpretersAvailable = value.ToString();
            }
        }

        #endregion

        public ICollection<NonEnglishLanguage> NonEnglishLanguages { get; set; }
        public ICollection<InterpretedLanguage> InterpretedLanguages { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }

    public class NonEnglishLanguage
    {
        public NonEnglishLanguage()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int NonEnglishLanguageID { get; set; }
        public string Language { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }

    public class InterpretedLanguage
    {
        public InterpretedLanguage()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int InterpretedLanguageID { get; set; }
        public string Language { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
