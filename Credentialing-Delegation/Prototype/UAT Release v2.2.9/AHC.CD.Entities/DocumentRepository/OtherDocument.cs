using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.DocumentRepository
{
    public class OtherDocument
    {
        public OtherDocument()
        {
            LastModifiedDate = DateTime.Now;
            this.StatusType = MasterData.Enums.StatusType.Active;
        }

        public int OtherDocumentID { get; set; }

        public string Title { get; set; }

        public bool IsPrivate { get; set; }

        public string DocumentPath { get; set; }

        public string ModifiedBy { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
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

        public ICollection<OtherDocumentHistory> OtherDocumentHistory { get; set; }

    }
}
