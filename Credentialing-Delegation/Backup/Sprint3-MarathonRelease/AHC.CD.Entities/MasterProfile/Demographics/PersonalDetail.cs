using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class PersonalDetail
    {
        public PersonalDetail()
        {
            LastUpdatedDateTime = DateTime.Now;
        }

        public int PersonalDetailID { get; set; }

        #region Title

        public string Salutation { get;  set; }

        [NotMapped]
        public SalutationType? SalutationType
        {
            get
            {
                if(String.IsNullOrEmpty(this.Salutation))
                    return null;

                if (this.Salutation.Equals("Not Available"))
                    return null;

                return (SalutationType)Enum.Parse(typeof(SalutationType), this.Salutation);
            }
            set
            {
                this.Salutation = value.ToString();
            }
        }

        #endregion    

        #region Provider Title

        public virtual ICollection<ProviderTitle> ProviderTitles { get; set; }

        #endregion

        [Required]
        public virtual string FirstName { get; set; }

        public virtual string MiddleName { get; set; }

        [Required]
        public virtual string LastName { get; set; }

        public string Suffix { get; set; }

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
                this.Gender = value.ToString();
            }
        }

        #endregion        

        public virtual string MaidenName { get; set; }

        #region MaritalStatus

        public string MaritalStatus { get; set; }

        [NotMapped]
        public MaritalStatusType? MaritalStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.MaritalStatus))
                    return null;

                if (this.MaritalStatus.Equals("Not Available"))
                    return null;

                return (MaritalStatusType)Enum.Parse(typeof(MaritalStatusType), this.MaritalStatus);
            }
            set
            {
                this.MaritalStatus = value.ToString();
            }
        }

        #endregion        

        public string SpouseName { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime LastUpdatedDateTime { get; private set; }
    }
}
