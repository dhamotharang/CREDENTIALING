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
    public class HomeAddressHistory
    {
        public HomeAddressHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int HomeAddressHistoryID { get; set; }

        #region Address

        //[Required]
        public string UnitNumber { get; set; }

        //[Required]
        public string Street { get; set; }

        //[Required]
        public string Country { get; set; }

        //[Required]
        public string State { get; set; }

        public string County { get; set; }

        //[Required]
        public string City { get; set; }

        //[Required]
        public string ZipCode { get; set; }

        #endregion        

        //[Required]
        [Column(TypeName = "datetime2")]        
        public DateTime? LivingFromDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime? LivingEndDate { get; set; }

        //[Required]
        public int StartYear { get; set; }

        public int StartMonth { get; set; }

        //[Required]
        public int EndYear { get; set; }

        public int EndMonth { get; set; }

        #region IsPresentlyStaying

        public string IsPresentlyStaying { get; private set; }

        [NotMapped]
        public YesNoOption? IsPresentlyStayingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsPresentlyStaying))
                    return null;

                if (this.IsPresentlyStaying.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsPresentlyStaying);
            }
            set
            {
                this.IsPresentlyStaying = value.ToString();
            }
        }

        #endregion

        #region AddressPreference

        public string AddressPreference { get; private set; }

        [NotMapped]
        public PreferenceType? AddressPreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.AddressPreference))
                    return null;

                if (this.AddressPreference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.AddressPreference);
            }
            set
            {
                this.AddressPreference = value.ToString();
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

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
