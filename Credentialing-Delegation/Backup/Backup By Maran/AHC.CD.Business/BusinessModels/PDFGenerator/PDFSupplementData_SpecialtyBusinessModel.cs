using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.PDFGenerator
{
    public class PDFSupplementData_SpecialtyBusinessModel
    {
        #region Supplement Specialty1
        public string SupplementSpecialtyCode1 { get; set; }
        public string SupplementSpecialtyInitialCertificationDate1 { get; set; }
        public string SupplementSpecialtyReCertificationDate1 { get; set; }
        public string SupplementSpecialtyExpirationDate1 { get; set; }
        public string SupplementSpecialtyBoardCertified1 { get; set; }
        public string SupplementSpecialtyCertifyingBoardCode1 { get; set; }
        public string SupplementSpecialtyHMO1 { get; set; }
        public string SupplementSpecialtyPPO1 { get; set; }
        public string SupplementSpecialtyPOS1 { get; set; }

        #region If not Board Certified1
        public string SupplementSpecialtyExamStatus1 { get; set; }
        public string SupplementSpecialtyDate1 { get; set; }       //No label given for this
        public string SupplementSpecialtyReasonForNotTakingExam1 { get; set; }
        #endregion

        #endregion

        #region Supplement Specialty2
        public string SupplementSpecialtyCode2 { get; set; }
        public string SupplementSpecialtyInitialCertificationDate2 { get; set; }
        public string SupplementSpecialtyReCertificationDate2 { get; set; }
        public string SupplementSpecialtyExpirationDate2 { get; set; }
        public string SupplementSpecialtyBoardCertified2 { get; set; }
        public string SupplementSpecialtyCertifyingBoardCode2 { get; set; }
        public string SupplementSpecialtyHMO2 { get; set; }
        public string SupplementSpecialtyPPO2 { get; set; }
        public string SupplementSpecialtyPOS2 { get; set; }

        #region If not Board Certified2
        public string SupplementSpecialtyExamStatus2 { get; set; }
        public string SupplementSpecialtyDate2 { get; set; }       //No label given for this
        public string SupplementSpecialtyReasonForNotTakingExam2 { get; set; }
        #endregion

        #endregion
    }
}
