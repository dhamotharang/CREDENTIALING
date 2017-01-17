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
    public class EmailDetail
    {
        public EmailDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int EmailDetailID { get; set; }
        
        [Required]
        [MaxLength(100)]    
        [Index(IsUnique = true)]
        public string EmailAddress { get; set; }

        #region Status

        [Required]
        public string Status { get; private set; }

        [NotMapped]
        public virtual StatusType StatusType
        {
            get
            {
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

        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
    }
}
