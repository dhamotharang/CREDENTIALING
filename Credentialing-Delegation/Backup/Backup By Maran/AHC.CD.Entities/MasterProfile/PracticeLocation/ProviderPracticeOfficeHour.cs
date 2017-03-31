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
    public class ProviderPracticeOfficeHour:PracticeOfficeHour
    {
        public ProviderPracticeOfficeHour()
        {
            LastModifiedDate = DateTime.Now;
        }

        #region AnyTimePhoneCoverage

        [Required]
        public string AnyTimePhoneCoverage { get; set; }

        [NotMapped]
        public YesNoOption? AnyTimePhoneCoverageYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.AnyTimePhoneCoverage))
                    return null;

                if (this.AnyTimePhoneCoverage.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AnyTimePhoneCoverage);
            }
            set
            {
                this.AnyTimePhoneCoverage = value.ToString();
            }
        }

        #endregion

        #region Answering Service

        //[Required]
        public string AnsweringService { get; private set; }

        [NotMapped]
        public YesNoOption? AnsweringServiceYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.AnsweringService))
                    return null;

                if (this.AnsweringService.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.AnsweringService);
            }
            set
            {
                this.AnsweringService = value.ToString();
            }
        }

        #endregion

        #region Voice Mail To Answering Service

        //[Required]
        public string VoiceMailToAnsweringService { get; private set; }

        [NotMapped]
        public YesNoOption? VoiceMailToAnsweringServiceYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.VoiceMailToAnsweringService))
                    return null;

                if (this.VoiceMailToAnsweringService.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.VoiceMailToAnsweringService);
            }
            set
            {
                this.VoiceMailToAnsweringService = value.ToString();
            }
        }

        #endregion

        #region Voice Mail Other

        //[Required]
        public string VoiceMailOther { get; private set; }

        [NotMapped]
        public YesNoOption? VoiceMailOtherYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.VoiceMailOther))
                    return null;

                if (this.VoiceMailOther.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.VoiceMailOther);
            }
            set
            {
                this.VoiceMailOther = value.ToString();
            }
        }

        #endregion

        #region Phone Number

        public string AfterHoursTelephoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCode))
                    return this.Number;
                else if (!String.IsNullOrEmpty(this.Number))
                    return this.CountryCode + "-" + this.Number;

                return null;
            }
            set
            {
                //Number = value;
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Number = numbers[0];
                    else
                    {
                        this.CountryCode = numbers[0];
                        this.Number = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string Number { get; set; }

        [NotMapped]
        public string CountryCode { get; set; }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
