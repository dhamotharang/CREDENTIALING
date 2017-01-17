using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.CustomField
{
    public class CustomField
    {
        public CustomField()
        {
            LastModifiedDate = DateTime.Now;
        }
        public int CustomFieldID { get; set; }

        public string CustomFieldTitle { get; set; }

        #region CustomFieldCategory

        public string CustomFieldCategory { get; private set; }

        [NotMapped]
        public CustomFieldCategoryType? customFieldCategoryType
        {
            get
            {
                if (String.IsNullOrEmpty(this.CustomFieldCategory))
                    return null;

                return (CustomFieldCategoryType)Enum.Parse(typeof(CustomFieldCategoryType), this.CustomFieldCategory);
            }
            set
            {
                this.CustomFieldCategory = value.ToString();
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

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
