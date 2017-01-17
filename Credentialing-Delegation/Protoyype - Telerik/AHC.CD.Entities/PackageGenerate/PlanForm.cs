using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.PackageGenerate
{
    public class PlanForm
    {
        public PlanForm()
        {
            LastModifiedDate = DateTime.Now;
        }
        public int PlanFormID { get; set; }

        public string PlanFormName { get; set; }

        public string FileName { get; set; }

        public string PlanFormBelongsTo { get; set; }

        public string PlanFormPath { get; set; }

        public string PlanFormXmlPath { get; set; }

        #region Xml Generation Status

        public string IsXmlGenerated { get; set; }

        [NotMapped]
        public YesNoOption? IsXmlGeneratedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsXmlGenerated))
                    return null;

                if (this.IsXmlGenerated.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsXmlGenerated);
            }
            set
            {
                this.IsXmlGenerated = value.ToString();
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
