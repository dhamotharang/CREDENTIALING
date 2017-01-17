using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class BillingContact
    {
        public BillingContact()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int BillingContactID { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string TaxId { get; set; }

        public PracticeAddress PracticeAddress { get; set; }

        [Required]
        public string POBoxAddress { get; set; }
        
        [Required]
        public string Telephone { get; set; }
        
        public string Fax { get; set; }
        
        public string EmailID { get; set; }

        #region ElectronicBillingCapability

        [Required]
        public string ElectronicBillingCapability { get; private set; }

        [NotMapped]
        public YesNoOption ElectronicBillingCapabilityYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ElectronicBillingCapability);
            }
            set
            {
                this.ElectronicBillingCapability = value.ToString();
            }
        }

        #endregion

        public string BillingDepartment { get; set; }
        
        [Required]
        public string CheckPayableTo { get; set; }
        
        [Required]
        public string Office { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
