using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.CustomField.CustomFieldTransaction
{
    public class CustomFieldTransactionData
    {
        public CustomFieldTransactionData()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CustomFieldTransactionDataID { get; set; }

        public int CustomFieldID { get; set; }
        [ForeignKey("CustomFieldID")]
        public CustomField CustomField { get; set; }

        public string CustomFieldTransactionDataValue { get; set; }

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

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
