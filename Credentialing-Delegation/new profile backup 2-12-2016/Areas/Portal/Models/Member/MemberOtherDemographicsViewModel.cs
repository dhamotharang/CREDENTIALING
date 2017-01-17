using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberOtherDemographicsViewModel
    {
        [Display(Name = "EthinicityDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Ethinicity { get; set; }

        [Display(Name = "RaceDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Race { get; set; }

        [Display(Name = "ReligionDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Religion { get; set; }

        [Display(Name = "MaritalStatusDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string MaritalStatus { get; set; }

        [Display(Name = "FamilyStatusDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string FamilyStatus { get; set; }

        #region Contact

        [Display(Name = "HomePhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string HomePhone { get; set; }


        [Display(Name = "EmailDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "AlternatePhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string AlternatePhone { get; set; }

        [Display(Name = "ContactPreferencePhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactPreference { get; set; }

        [Display(Name = "DoNotCallPhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string DoNotCall { get; set; }

        [Display(Name = "ResidentialStatusPhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ResidentialStatus { get; set; }

          [Display(Name = "PreferableCallDaysPhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string PreferableCallDays { get; set; }

        [Display(Name = "ContactAlertDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
         [DisplayFormat(NullDisplayText = "-")]
         public string ContactAlert { get; set; }

        [Display(Name = "ParentalConsentThruPhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ParentalConsentThru { get; set; }

        [Display(Name = "ParentalConsentFromPhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ParentalConsentFrom { get; set; }

        [Display(Name = "PrefCallTimeFromPhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? PrefCallTimeFrom { get; set; }

        [Display(Name = "PrefCallTimeThruPhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-", DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? PrefCallTimeThru { get; set; }
        #endregion
        #region Language
        [Display(Name = "Spoken1PhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Spoken1 { get; set; }

        [Display(Name = "Spoken2PhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Spoken2 { get; set; }

        [Display(Name = "Written1PhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Written1 { get; set; }

        [Display(Name = "Written2PhoneDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string Written2 { get; set; }
        #endregion
        #region Labels
        /*Just to maintain localization in labels*/
        [Display(Name = "CultureTitleDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string CultureTitle { get; set; }

        [Display(Name = "ContactTitleDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactTitle { get; set; }

        [Display(Name = "LanguageTitleDemoGraphics", ResourceType = typeof(App_LocalResources.Content))]
        [DisplayFormat(NullDisplayText = "-")]
        public string LanguageTitle { get; set; }
        #endregion
    }
}
