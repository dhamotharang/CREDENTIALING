using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class EmployerDetail
    {
        public EmployerDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EmployerDetailID { get; set; }

        [Required]
        public string EmployerName { get; set; }

        [Required]
        public string SupervisorName  { get; set; }

        #region ProviderType

        [Required]
        public int ProviderTypeID { get; set; }
        [ForeignKey("ProviderTypeID")]
        public virtual ProviderType ProviderType { get; set; }

        #endregion

        [Required]
        public string EmployerMobile { get; set; }
        
        [Required]
        public string EmployerFax { get; set; }

        #region CanContactEmployer

        [Required]
        public string CanContactEmployer { get; private set; }

        [NotMapped]
        public YesNoOption CanContactEmployerOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CanContactEmployer);
            }
            set
            {
                this.CanContactEmployer = value.ToString();
            }
        }

        #endregion

        [Required]
        public string Department { get; set; }        
        
        public string WorkExperienceDocPath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
