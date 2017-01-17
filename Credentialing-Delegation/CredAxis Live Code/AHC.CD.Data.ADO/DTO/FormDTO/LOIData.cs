using AHC.CD.Entities.MasterData.Enums;
using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.DTO.FormDTO
{
    public class LOIData
    {
        public int ProfileID { get; set; }
        public string IsPrimary { get; set; }
        public YesNoOption? PrimaryYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsPrimary))
                    return null;

                if (this.IsPrimary.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsPrimary);
            }
            set
            {
                this.IsPrimary = value.ToString();
            }
        }
        public string Practicelocationstatus { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string ProviderTitleCode { get; set; }
        public string ProviderTitle { get; set; }
        public string Country { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public DateTime JoiningDate { get; set; }

        public string SpecialityName { get; set; }

        public string SpecialityPreference { get; set; }

        
        public PreferenceType? PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.SpecialityPreference))
                    return null;

                if (this.SpecialityPreference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.SpecialityPreference);
            }
            set
            {
                this.SpecialityPreference = value.ToString();
            }
        }
        public string SpecialityStatus { get; set; }
        
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.SpecialityStatus))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.SpecialityStatus);
            }
            set
            {
                this.SpecialityStatus = value.ToString();
            }
        }
        public string NPINumber { get; set; }
        public string CAQHNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
       
        
        public string NationalIDNumberNumberStored { get; set; }
       
        public string NationalIDNumber
        {
            get { return EncryptorDecryptor.Decrypt(this.NationalIDNumberNumberStored); }
            set { this.NationalIDNumberNumberStored = EncryptorDecryptor.Encrypt(value); }
        }
        
    }
}
