using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.PackageGenerate
{
    public class PackageGenerator
    {
        public PackageGenerator()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PackageGeneratorID { set; get; }

        public string PackageName { set; get; }

        public string PackageFilePath { get; set; }

        public int? InitiatedByID { get; set; }
        [ForeignKey("InitiatedByID")]
        public CDUser InitiatedBy { get; set; }

        public int? PlanID { get; set; }
        [ForeignKey("PlanID")]
        public Plan Plan { get; set; }

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
