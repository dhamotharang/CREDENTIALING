using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PDFSupplementData_TrainingBusinessModel
    {
        #region Training
        public string SupplementTrainingInstitutionOrHospitalName { get; set; }
        public string SupplementTrainingSchoolCode { get; set; }
        public string SupplementTrainingNumber { get; set; }
        public string SupplementTrainingStreet { get; set; }
        public string SupplementTrainingSuiteBuilding { get; set; }
        public string SupplementTrainingCity { get; set; }
        public string SupplementTrainingState { get; set; }
        public string SupplementTrainingZip { get; set; }
        public string SupplementTrainingCountryCode { get; set; }
        public string SupplementTrainingTelephone { get; set; }
        public string SupplementTrainingFax { get; set; }
        public string SupplementTrainingCompleteInSchool { get; set; }
        public string SupplementTrainingIfNotCompleteExplain { get; set; }

        #region Internship/Residency1
        public string SupplementType1 { get; set; }
        public string SupplementStartDate1 { get; set; }
        public string SupplementEndDate1 { get; set; }
        public string SupplementDepartmentSpecialty1 { get; set; }
        public string SupplementNameOfDirector1 { get; set; }
        #endregion

        #region Internship/Residency2
        public string SupplementType2 { get; set; }
        public string SupplementStartDate2 { get; set; }
        public string SupplementEndDate2 { get; set; }
        public string SupplementDepartmentSpecialty2 { get; set; }
        public string SupplementNameOfDirector2 { get; set; }
        #endregion

        #region Internship/Residency3
        public string SupplementType3 { get; set; }
        public string SupplementStartDate3 { get; set; }
        public string SupplementEndDate3 { get; set; }
        public string SupplementDepartmentSpecialty3 { get; set; }
        public string SupplementNameOfDirector3 { get; set; }
        #endregion

        #endregion       
    }
}
