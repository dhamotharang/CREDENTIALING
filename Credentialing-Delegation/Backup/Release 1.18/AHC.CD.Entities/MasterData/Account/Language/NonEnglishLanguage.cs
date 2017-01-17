using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Language
{
    public class NonEnglishLanguage
    {
        public NonEnglishLanguage()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int NonEnglishLanguageID { get; set; }
        public string Language { get; set; }

        #region InterpretersAvailable

        public string InterpretersAvailable { get; private set; }

        [NotMapped]
        public YesNoOption? InterpretersAvailableYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.InterpretersAvailable))
                    return null;

                if (this.InterpretersAvailable.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.InterpretersAvailable);
            }
            set
            {
                this.InterpretersAvailable = value.ToString();
            }
        }

        #endregion

        #region Status

        public string Status { get; set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
