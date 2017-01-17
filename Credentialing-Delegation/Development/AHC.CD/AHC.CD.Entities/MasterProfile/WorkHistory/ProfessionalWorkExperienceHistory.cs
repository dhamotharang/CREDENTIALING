using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class ProfessionalWorkExperienceHistory
    {
        public ProfessionalWorkExperienceHistory()
        {
            LastModifiedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now.ToUniversalTime();

        }

        public int ProfessionalWorkExperienceHistoryID { get; set; }

        //[Required]
        public string EmployerName { get; set; }

        #region Address

        public string Building { get; set; }

        public string Street { get; set; }

        //[Required]
        public string Country { get; set; }

        //[Required]
        public string State { get; set; }

        public string County { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion        

        #region Mobile Number

        public string MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCodeMobile))
                    return this.EmployerMobile;
                else if (!String.IsNullOrEmpty(this.EmployerMobile))
                    return this.CountryCodeMobile + "-" + this.EmployerMobile;

                return null;
            }
            set
            {
                if(value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.EmployerMobile = numbers[0];
                    else
                    {
                        this.CountryCodeMobile = numbers[0];
                        this.EmployerMobile = numbers[1];
                    }
                    
                }
            }
        }

        [NotMapped]
        public string EmployerMobile { get; set; }

        [NotMapped]
        public string CountryCodeMobile { get; set; }

        #endregion

        #region Fax Number

        public string FaxNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCodeFax))
                    return this.EmployerFax;
                else if (!String.IsNullOrEmpty(this.EmployerFax))
                    return this.CountryCodeFax + "-" + this.EmployerFax;

                return null;
            }
            set
            {
                if(value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.EmployerFax = numbers[0];
                    else
                    {
                        this.CountryCodeFax = numbers[0];
                        this.EmployerFax = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string EmployerFax { get; set; }

        [NotMapped]
        public string CountryCodeFax { get; set; }

        #endregion        

        public string JobTitle { get; set; }

        public string SupervisorName { get; set; }

        public string Department { get; set; }

        public string EmployerEmail { get; set; }

        #region ProviderType

        //[Required]
        public int? ProviderTypeID { get; set; }
        [ForeignKey("ProviderTypeID")]
        public ProviderType ProviderType { get; set; }

        #endregion        

        #region CanContactEmployer

        //[Required]
        public string CanContactEmployer { get; set; }

        [NotMapped]
        public YesNoOption? CanContactEmployerOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.CanContactEmployer))
                    return null;

                if (this.CanContactEmployer.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CanContactEmployer);
            }
            set
            {
                this.CanContactEmployer = value.ToString();
            }
        }

        #endregion

        #region Currently Working

        //[Required]
        public string CurrentlyWorking { get; set; }

        [NotMapped]
        public YesNoOption? CurrentlyWorkingOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.CurrentlyWorking))
                    return null;

                if (this.CurrentlyWorking.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CurrentlyWorking);
            }
            set
            {
                this.CurrentlyWorking = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "datetime2")]        
        public DateTime? EndDate { get; set; }

        public string DepartureReason { get; set; }

        public string WorkExperienceDocPath { get; set; }

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
