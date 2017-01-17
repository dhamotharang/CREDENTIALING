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

        [Required]
        public string Title { get; private set; }

        [NotMapped]
        public TitleType TitleType
        {
            get
            {
                return (TitleType)Enum.Parse(typeof(TitleType), this.Gender);
            }
            set
            {
                this.Gender = value.ToString();
            }
        }

        #endregion    

        [Required]
        public virtual string FirstName { get; set; }

        public virtual string MiddleName { get; set; }

        [Required]
        public virtual string LastName { get; set; }

        public string Suffix { get; set; }

        #region Gender

        [Required]
        public string Gender { get; private set; }

        [NotMapped]
        public GenderType GenderType
        {
            get
            {
                return (GenderType)Enum.Parse(typeof(GenderType), this.Gender);
            }
            set
            {
                this.Gender = value.ToString();
            }
        }

        #endregion        

        public virtual string MaidenName { get; private set; }

        #region MaritalStatus

        [Required]
        public string MaritalStatus { get; private set; }

        [NotMapped]
        public MaritalStatusType MaritalStatusType
        {
            get
            {
                return (MaritalStatusType)Enum.Parse(typeof(MaritalStatusType), this.MaritalStatus);
            }
            set
            {
                this.MaritalStatus = value.ToString();
            }
        }

        #endregion        

        public string SpouseName { get; set; }

        public string ProfilePhotoPath { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime LastUpdatedDateTime { get; private set; }
    }
}
