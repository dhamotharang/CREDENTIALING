using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfessionalReference
{
    public class ProfessionalReferenceInfoHistory
    {
        public ProfessionalReferenceInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now.ToUniversalTime();
        }

        public int ProfessionalReferenceInfoHistoryID { get; set; }

        #region ProviderType

        public int? ProviderTypeID { get; set; }
        [ForeignKey("ProviderTypeID")]
        public ProviderType ProviderType { get; set; }

        #endregion

        //[Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        //[Required]
        public string LastName { get; set; }

        public string Degree { get; set; }

        #region Specialty

        //[Required] do not uncomment
        public int? SpecialtyID { get; set; }
        [ForeignKey("SpecialtyID")]
        public Specialty Specialty { get; set; }

        #endregion
        
        public string Relationship { get; set; }

        #region IsBoardCerified

        //[Required]
        public string IsBoardCerified { get; set; }

        [NotMapped]
        public YesNoOption? BoardCerifiedOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsBoardCerified))
                    return null;

                if (this.IsBoardCerified.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsBoardCerified);
            }
            set
            {
                this.IsBoardCerified = value.ToString();
            }
        }

        #endregion
     
        public string Email { get; set; }        
       
        public string Building { get; set; }
        
        public string Street { get; set; }
        
        public string State { get; set; }
        
        public string County { get; set; }
        
        public string City { get; set; }

        public string Country { get; set; }
        
        public string Zipcode { get; set; }

        #region Phone Number

        //[Required]
        public string PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.PhoneCountryCode))
                    return this.Telephone;
                else if (!String.IsNullOrEmpty(this.Telephone))
                    return this.PhoneCountryCode + "-" + this.Telephone;

                return null;
            }
            set
            {
                if(value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Telephone = numbers[0];
                    else
                    {
                        this.PhoneCountryCode = numbers[0];
                        this.Telephone = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string Telephone { get; set; }

        [NotMapped]
        public string PhoneCountryCode { get; set; }


        #endregion

        #region Fax Number

        public string FaxNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.FaxCountryCode))
                    return this.Fax;
                else if (!String.IsNullOrEmpty(this.Fax))
                    return this.FaxCountryCode + "-" + this.Fax;

                return null;
            }
            set
            {
                if(value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Fax = numbers[0];
                    else
                    {
                        this.FaxCountryCode = numbers[0];
                        this.Fax = numbers[1];
                    }
                    
                }
            }
        }

        [NotMapped]
        public string Fax { get; set; }

        [NotMapped]
        public string FaxCountryCode { get; set; }

        #endregion
        
        #region History Status

        public string HistoryStatus { get; private set; }

        [NotMapped]
        public HistoryStatusType? HistoryStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.HistoryStatus))
                    return null;

                if (this.HistoryStatus.Equals("Not Available"))
                    return null;

                return (HistoryStatusType)Enum.Parse(typeof(HistoryStatusType), this.HistoryStatus);
            }
            set
            {
                this.HistoryStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public int? DeletedById { get; set; }
        [ForeignKey("DeletedById")]
        public CDUser DeletedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedDate { get; set; }

    }
}
