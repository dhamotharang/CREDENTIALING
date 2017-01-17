using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Service
{
    public class FacilityService
    {
        public FacilityService()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FacilityServiceID { get; set; }

        #region Laboratory Services

        public string LaboratoryServices { get; private set; }

        public YesNoOption? LaboratoryServicesYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.LaboratoryServices))
                    return null;

                if (this.LaboratoryServices.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.LaboratoryServices);
            }
            set
            {
                this.LaboratoryServices = value.ToString();
            }
        }

        public string LaboratoryAccreditingCertifyingProgram { get; set; }

        #endregion

        #region Radiology Services

        public string RadiologyServices { get; private set; }

        public YesNoOption? RadiologyServicesYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.RadiologyServices))
                    return null;

                if (this.RadiologyServices.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.RadiologyServices);
            }
            set
            {
                this.RadiologyServices = value.ToString();
            }
        }

        public string RadiologyXRayCertificateType { get; set; }

        #endregion

        public virtual ICollection<FacilityServiceQuestionAnswer> FacilityServiceQuestionAnswers { get; set; }

        #region Anesthesia Administration

        public string IsAnesthesiaAdministered { get; private set; }

        public YesNoOption? IsAnesthesiaAdministeredYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsAnesthesiaAdministered))
                    return null;

                if (this.IsAnesthesiaAdministered.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsAnesthesiaAdministered);
            }
            set
            {
                this.IsAnesthesiaAdministered = value.ToString();
            }
        }

        public string AnesthesiaCategory { get; set; }

        public string AnesthesiaAdminFirstName { get; set; }

        public string AnesthesiaAdminLastName { get; set; }

        #endregion

        #region PracticeType

        public int? PracticeTypeID { get; set; }
        [ForeignKey("PracticeTypeID")]
        public FacilityPracticeType PracticeType { get; set; }

        #endregion

        public string AdditionalOfficeProcedures  { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
