using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.UserInfo
{
    public class ProfileUser
    {
        public ProfileUser()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfileUserID { get; set; }

        public int? CDUserID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string RoleCode { get; set; }

        #region Phone Number

        //[Required]
        public string MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.PhoneCountryCode))
                    return this.Phone;
                else if (!String.IsNullOrEmpty(this.Phone))
                    return this.PhoneCountryCode + "-" + this.Phone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Phone = numbers[0];
                    else
                    {
                        this.PhoneCountryCode = numbers[0];
                        this.Phone = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string Phone { get; set; }

        [NotMapped]
        public string PhoneCountryCode { get; set; }


        #endregion

        #region Gender

        [Required]
        public string Gender { get; set; }

        [NotMapped]
        public GenderType? GenderType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Gender))
                    return null;

                if (this.Gender.Equals("Not Available"))
                    return null;

                return (GenderType)Enum.Parse(typeof(GenderType), this.Gender);
            }
            set
            {
                if (value == 0)
                    this.Gender = "Not Available";
                else
                    this.Gender = value.ToString();
            }
        }

        #endregion

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        public virtual ICollection<ProviderUser> ProvidersUser { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ActivationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
