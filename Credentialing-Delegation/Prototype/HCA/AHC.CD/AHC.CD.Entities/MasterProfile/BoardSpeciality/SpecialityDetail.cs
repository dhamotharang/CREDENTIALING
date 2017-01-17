using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.BoardSpeciality
{
    public class SpecialityDetail
    {
        public SpecialityDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int SpecialityDetailID { get; set; }

        #region SpecialityPreference

        [Required]
        public string SpecialityPreference { get; private set; }

        [NotMapped]
        public PreferenceType YesNoOption
        {
            get
            {
                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.SpecialityPreference);
            }
            set
            {
                this.SpecialityPreference = value.ToString();
            }
        }

        #endregion        

        #region Speciality

        [Required]
        public int SpecialityID { get; set; }
        [ForeignKey("SpecialityID")]
        public Speciality Speciality { get; set; }

        #endregion

        [Required]
        public Double PercentageOfTime { get; set; }

        #region ListedInHMO

        [Required]
        public string ListedInHMO { get; private set; }

        [NotMapped]
        public YesNoOption ListedInHMOYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ListedInHMO);
            }
            set
            {
                this.ListedInHMO = value.ToString();
            }
        }

        #endregion

        #region ListedInPPO

        [Required]
        public string ListedInPPO { get; private set; }

        [NotMapped]
        public YesNoOption ListedInPPOYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ListedInPPO);
            }
            set
            {
                this.ListedInPPO = value.ToString();
            }
        }
        
        #endregion

        #region ListedInPOS

        [Required]
        public string ListedInPOS { get; set; }

        [NotMapped]
        public YesNoOption ListedInPOSYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ListedInPOS);
            }
            set
            {
                this.ListedInPOS = value.ToString();
            }
        }

        #endregion        

        public string CertificatePath { get; set; }

        #region SpecialityBoardDetail

        [Required]
        public int SpecialityBoardDetailID { get; set; }
        [ForeignKey("SpecialityBoardDetailID")]
        public SpecialityBoardDetail SpecialityBoardDetail { get; set; }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
