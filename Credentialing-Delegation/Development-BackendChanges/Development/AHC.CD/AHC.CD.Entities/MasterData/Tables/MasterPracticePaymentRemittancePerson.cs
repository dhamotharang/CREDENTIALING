using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class MasterPracticePaymentRemittancePerson
    {
        public MasterPracticePaymentRemittancePerson()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int MasterPracticePaymentRemittancePersonID { get; set; }

        public MasterEmployee PaymentAndRemittancePerson { get; set; }

        #region ElectronicBillingCapability

        // [Required]
        public string ElectronicBillingCapability { get; set; }

        [NotMapped]
        public YesNoOption? ElectronicBillingCapabilityYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ElectronicBillingCapability))
                    return null;

                if (this.ElectronicBillingCapability.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.ElectronicBillingCapability);
            }
            set
            {
                this.ElectronicBillingCapability = value.ToString();
            }
        }

        #endregion

        public string BillingDepartment { get; set; }

        //[Required]
        public string CheckPayableTo { get; set; }

        //[Required]
        public string Office { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
