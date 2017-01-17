using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeProviderHistory
    {
        public PracticeProviderHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PracticeProviderHistoryID { get; set; }

        public string NPINumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        #region Practice Type

        public string Practice { get; private set; }

        [NotMapped]
        public PracticeType? PracticeType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Practice))
                    return null;

                if (this.Practice.Equals("Not Available"))
                    return null;

                return (PracticeType)Enum.Parse(typeof(PracticeType), this.Practice);
            }
            set
            {
                this.Practice = value.ToString();
            }
        }

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

        #region Relation

        public string Relation { get; private set; }

        [NotMapped]
        public RelationType? RelationType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Relation))
                    return null;

                if (this.Relation.Equals("Not Available"))
                    return null;

                return (RelationType)Enum.Parse(typeof(RelationType), this.Relation);
            }
            set
            {
                this.Relation = value.ToString();
            }
        }
        

        #endregion

        #region Address

        public string Building { get; set; }

        public string Street { get; set; }

        public string Country { get; set; }

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
                if (String.IsNullOrEmpty(this.CountryCodeTelephone))
                    return this.Telephone;
                else if (!String.IsNullOrEmpty(this.Telephone))
                    return this.CountryCodeTelephone + "-" + this.Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Telephone = numbers[0];
                    else
                    {
                        this.CountryCodeTelephone = numbers[0];
                        this.Telephone = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string Telephone { get; set; }

        [NotMapped]
        public string CountryCodeTelephone { get; set; }


        #endregion

        public virtual ICollection<PracticeProviderType> PracticeProviderTypes { get; set; }

        public virtual ICollection<PracticeProviderSpecialty> PracticeProviderSpecialties { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ActivationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeactivationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
